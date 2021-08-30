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
using System.Linq;

namespace Regata.Core.Hardware
{
    public enum Direction { Negative = -1, Positive = 1 }
    public enum Heights { h2p5, h5, h10, h20 }
     public partial class SampleChanger
    {

        public void MoveToTarget()
        {
            // NOTE: Y and C can move in parallel, but X only after Y!
            // https://github.com/regata-jinr/Core/issues/62
            MoveToC(TargetPosition.C);

            MoveToY(TargetPosition.Y);

            XemoDLL.MB_Still((short)Axes.Y);

            MoveToX(TargetPosition.X);
        }

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

        public void PutSampleToTheDisk(short cellNum)
        {
            var posName = cellNum switch
            {
                 > 30  => "AboveCellInternalDisk",
                <= 30 => "AboveCellExternalDisk"
            };

            int cell = cellNum switch
            {
                > 45 => 1,
                <= 0 => 1,
                _ => cellNum
            };

            Position pos = null;
            using (var r = new RegataContext())
            {
                pos = r.Positions.AsNoTracking().Where(p => p.Detector == PairedDetector && p.SerialNumber == p.SerialNumber && p.Name == posName).First();

                if (pos == null || !pos.C.HasValue)
                    Report.Notify(new Message(Codes.ERR_XM_WRONG_POS));
            }

            int c_coord = pos.C.Value + (cellNum - 1) * Settings.GapBetweenCellsExternalDisk;
        }

        public void TakeSampleFromTheCell(short cellNum)
        {

        }

        public void PutSampleAboveDetectorWithHeight(Heights h)
        {
            
        }


        public void MoveRight(int VelocityScalingFactor)
        {
            Move(Axes.X, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Positive);
        }

        public void MoveLeft(int VelocityScalingFactor)
        {
            Move(Axes.X, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Negative);
        }

        public void MoveUp(int VelocityScalingFactor)
        {
            Move(Axes.Y, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Positive);
        }

        public void MoveDown(int VelocityScalingFactor)
        {
            Move(Axes.Y, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Negative);
        }

        public void MoveClockwise(int VelocityScalingFactor)
        {
            Move(Axes.C, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Positive);
        }

        public void MoveСounterclockwise(int VelocityScalingFactor)
        {
            Move(Axes.C, velocityScalingFactor: VelocityScalingFactor, dir: Direction.Negative);
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

        private void Move(Axes axis, int? coordinate = null, int? velocityScalingFactor = null, Direction? dir = null)
        {
            _activeAxis = axis;

            if (coordinate == null && velocityScalingFactor == null)
                return;

            if (coordinate.HasValue)
            {
                XemoDLL.MB_Amove((short)axis, coordinate.Value);
                XemoDLL.MB_Still((short)axis);

                return;
            }

            if (velocityScalingFactor.HasValue && dir.HasValue)
            {
                var _speed = axis switch
                { 
                    Axes.X => Settings.XVelocity,
                    Axes.Y => Settings.YVelocity,
                    Axes.C => Settings.CVelocity,
                    _ => 0

                };
                XemoDLL.MB_Jog((short)axis, (int)dir.Value * velocityScalingFactor.Value * _speed);
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
}     // namespace Regata.Core.Hardware

