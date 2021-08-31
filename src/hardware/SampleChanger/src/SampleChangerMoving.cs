/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.EntityFrameworkCore;
using Regata.Core.Messages;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware.Xemo;
using System;
using System.Linq;

namespace Regata.Core.Hardware
{
    public enum Direction { Negative = -1, Positive = 1 }
    public enum Heights { h2p5, h5, h10, h20 }
     public partial class SampleChanger
    {

        public void MoveToPosition(Position pos, Axes moveAlongAxisFirst)
        {
            if (pos.C.HasValue)
                MoveToC(pos.C.Value);

            if (moveAlongAxisFirst == Axes.X)
            {
                MoveToX(pos.X);
                MoveToY(pos.Y);
            }
            else
            {
                MoveToY(pos.Y);
                MoveToX(pos.X);
            }
        }

        private (Position Above, Position Near) GetAboveAndNearPositions(string diskName)
        {

            Position posAbove = null;
            Position posNear = null;
            using (var r = new RegataContext())
            {
                posAbove = r.Positions.AsNoTracking().Where(p => p.Detector == PairedDetector && p.SerialNumber == p.SerialNumber && p.Name == $"AboveCell{diskName}Disk").First();

                posNear = r.Positions.AsNoTracking().Where(p => p.Detector == PairedDetector && p.SerialNumber == p.SerialNumber && p.Name == $"NearAndAbove{diskName}Disk").First();


                if (posAbove == null || posNear == null || !posAbove.C.HasValue)
                {
                    Report.Notify(new Message(Codes.ERR_XM_WRONG_POS));
                    return (null,null);
                }

                return (posAbove, posNear);
            }
        }


        public void PutSampleToTheDisk(short cellNum)
        {
            var _dp = new DiskParams(cellNum);

            var pos = GetAboveAndNearPositions(_dp.DiskName);

            pos.Above.C = pos.Above.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;
            MoveToPosition(pos.Above, Axes.X);
            MoveToX(pos.Near.X);

            
        }

        public void TakeSampleFromTheCell(short cellNum)
        {
            var _dp = new DiskParams(cellNum);
            var pos = GetAboveAndNearPositions(_dp.DiskName);
            pos.Near.C = pos.Near.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;
            MoveToPosition(pos.Near, Axes.X);
            MoveToX(pos.Above.X);
        }

        public void PutSampleAboveDetectorWithHeight(Heights h)
        {
            
        }


        public void MoveRight(int speed)
        {
            Move(Axes.X, speed: speed, dir: Direction.Positive);
        }

        public void MoveLeft(int speed)
        {
            Move(Axes.X, speed: speed, dir: Direction.Negative);
        }

        public void MoveUp(int speed)
        {
            Move(Axes.Y, speed: speed, dir: Direction.Positive);
        }

        public void MoveDown(int speed)
        {
            Move(Axes.Y, speed: speed, dir: Direction.Negative);
        }

        public void MoveClockwise(int speed)
        {
            Move(Axes.C, speed: speed, dir: Direction.Positive);
        }

        public void MoveСounterclockwise(int speed)
        {
            Move(Axes.C, speed: speed, dir: Direction.Negative);
        }

        public void MoveToX(int? x_coord)
        {
            Move(axis: Axes.X, coordinate: x_coord);
        }
        public void MoveToY(int? y_coord)
        {
            Move(axis: Axes.Y, coordinate: y_coord);

        }
        public void MoveToC(int? c_coord)
        {
            Move(axis: Axes.C, coordinate:c_coord);
        }

        private void Move(Axes axis, int? coordinate = null, int? speed = null, Direction? dir = null)
        {
            _activeAxis = axis;

            if (coordinate == null && speed == null)
                return;

            if (coordinate.HasValue)
            {
                var _speed = XemoDLL.MB_AGet((short)axis, XemoConst.Speed);
                if (speed.HasValue)
                    XemoDLL.MB_ASet((short)axis, XemoConst.Speed, speed.Value);

                XemoDLL.MB_Amove((short)axis, coordinate.Value);
                XemoDLL.MB_Still((short)axis);

                XemoDLL.MB_ASet((short)axis, XemoConst.Speed, _speed);

                return;
            }

            if (speed.HasValue && dir.HasValue)
            {
                
                XemoDLL.MB_Jog((short)axis, (int)dir.Value * speed.Value);
                return;
            }
        }

        #region Stop
        public void StopX() => Stop(Axes.X);
        public void StopY() => Stop(Axes.Y);

        public void StopC() => Stop(Axes.C);

        public void Stop()
        {
            StopX();
            StopY();
            StopC();
        }

        private void Stop(Axes ax)
        {
            XemoDLL.MB_Stop((short)ax);
        }
        #endregion Stop

        #region Home
        public void HomeX() => Home(Axes.X);
        public void HomeY() => Home(Axes.Y);

        public void HomeC() => Home(Axes.C);

        public void Home()
        {
            Connect();
            InitializeAxes();
            ResetAllSoftwareLimits();

            HomeY();
            Settings.LYDecel = Settings.AxesParams.L_DECEL[0];
            HomeX();
            Settings.LXDecel = Settings.AxesParams.L_DECEL[1];
            HomeC();
            Settings.LCDecel = Settings.AxesParams.L_DECEL[2];

            CurrentPosition = HomePosition;


            Settings.SoftwareYUpLimit = MaxY + 1000;
            //Settings.SoftwareYDownLimit = 0;

            //Settings.SoftwareXLeftLimit = 1500;
            Settings.SoftwareXRightLimit = MaxX + 1000;

            Settings.SoftwareCLeftLimit = -MaxC;
            Settings.SoftwareCRightLimit = MaxC;

        }

        private void Home(Axes ax)
        {
            XemoDLL.MB_Home((short)ax);
            XemoDLL.MB_Still((short)ax);
        }
        #endregion

        public int GetAxisPosition(Axes ax)
        {
            return ax switch
            {
                Axes.X => CurrentPosition.X,
                Axes.Y => CurrentPosition.Y,
                Axes.C => CurrentPosition.C.HasValue ? CurrentPosition.C.Value : 0,
                _ => -444
            };
        }


    } // public partial class SampleChanger

    public struct DiskParams
    {
        public int    CellNum;
        public int    Gap;
        public string DiskName;
        public int    InitCellNum;

        public DiskParams(int cellNum)
        {
            DiskName = cellNum switch
            {
                > 30 => "Internal",
                <= 30 => "External"
            };

            this.CellNum = cellNum switch
            {
                > 45 => 1,
                <= 0 => 1,
                _ => cellNum
            };

            Gap = cellNum switch
            {
                > 30 => 2400,
                <= 30 => 1200
            };

            InitCellNum = cellNum switch
            {
                > 30 => 31,
                <= 30 => 1
            };
        }

    }

}     // namespace Regata.Core.Hardware

