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
using Regata.Core.Hardware.Xemo;
using Regata.Core.Messages;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Regata.Core.Hardware
{
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

        public async Task MoveToPositionAsync(Position pos, Axes moveAlongAxisFirst)
        {
            TargetPosition = pos;

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

            await TrackPositionAsync();

        }

        private (Position Above, Position Near) GetAboveAndNearPositions(string diskName)
        {
            try
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
                        return (null, null);
                    }

                    return (posAbove, posNear);
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GET_PIN_POS_UNREG) { DetailedText = ex.Message });
                return (null, null);
            }
        }

        public void PutSampleToTheDisk(short cellNum)
        {
            var _dp = new DiskParams(cellNum);

            var pos = GetAboveAndNearPositions(_dp.DiskName);

            pos.Above.C = pos.Above.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;

            //if (PinnedPosition == PinnedPositions.InsideDetShield)
            MoveToY(MaxY);

            MoveToPosition(pos.Above, Axes.X);
            MoveToX(pos.Near.X);

            IsSampleCaptured = false;
            PinnedPosition = PinnedPositions.NearDisk;

        }

        public async Task PutSampleToTheDiskAsync(short cellNum)
        {
            var _dp = new DiskParams(cellNum);

            var pos = GetAboveAndNearPositions(_dp.DiskName);
            // 
            pos.Above.C = pos.Above.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;

            //if (PinnedPosition == PinnedPositions.InsideDetShield)
            MoveToY(MaxY);

            await MoveToPositionAsync(pos.Above, Axes.X);

            MoveToX(pos.Near.X);

            IsSampleCaptured = false;
            PinnedPosition = PinnedPositions.NearDisk;

        }

        public void TakeSampleFromTheCell(short cellNum)
        {
            var _dp = new DiskParams(cellNum);
            var pos = GetAboveAndNearPositions(_dp.DiskName);
            pos.Near.C = pos.Near.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;
            if(PinnedPosition != PinnedPositions.NearDisk)
                MoveToY(MaxY);
            MoveToPosition(pos.Near, Axes.X);
            MoveToX(pos.Above.X);
            IsSampleCaptured = true;
            PinnedPosition = PinnedPositions.AboveDisk;
        }

        public async Task TakeSampleFromTheCellAsync(short cellNum)
        {
            var _dp = new DiskParams(cellNum);
            var pos = GetAboveAndNearPositions(_dp.DiskName);
            pos.Near.C = pos.Near.C.Value + (_dp.CellNum - _dp.InitCellNum) * _dp.Gap;
            if(PinnedPosition != PinnedPositions.NearDisk)
                MoveToY(MaxY);

            await MoveToPositionAsync(pos.Near, Axes.X);

            MoveToX(pos.Above.X);
            IsSampleCaptured = true;
            PinnedPosition = PinnedPositions.AboveDisk;
        }


        public void PutSampleAboveDetectorWithHeight(Heights h)
        {
            try
            {
                var hstr = h switch
                {
                    Heights.h2p5 => "H2p5",
                    Heights.h5 => "H5",
                    Heights.h10 => "H10",
                    Heights.h20 => "H20",
                    _ => "H2p5" // ?
                };

                Position posAbove = null;
                using (var r = new RegataContext())
                {
                    posAbove = r.Positions.AsNoTracking().Where(p => p.Detector == PairedDetector && p.SerialNumber == p.SerialNumber && p.Name == $"AboveDetector{hstr}").First();


                    if (posAbove == null)
                    {
                        Report.Notify(new Message(Codes.ERR_XM_WRONG_POS));
                        return;
                    }

                }

                MoveToY(MaxY);
                MoveToPosition(posAbove, Axes.X);

                PinnedPosition = PinnedPositions.AboveDisk;
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_PUT_ABV_DET_UNREG) { DetailedText = ex.Message });
            }

        }

        public async Task PutSampleAboveDetectorWithHeightAsync(Heights h)
        {
            try
            {
                var hstr = h switch
                {
                    Heights.h2p5 => "H2p5",
                    Heights.h5 => "H5",
                    Heights.h10 => "H10",
                    Heights.h20 => "H20",
                    _ => "H2p5" // ?
                };

                Position posAbove = null;
                using (var r = new RegataContext())
                {
                    posAbove = r.Positions.AsNoTracking().Where(p => p.Detector == PairedDetector && p.SerialNumber == p.SerialNumber && p.Name == $"AboveDetector{hstr}").First();


                    if (posAbove == null)
                    {
                        Report.Notify(new Message(Codes.ERR_XM_WRONG_POS));
                        return;
                    }

                }

                MoveToY(MaxY);

                await MoveToPositionAsync(posAbove, Axes.X);

                PinnedPosition = h switch
                {
                    Heights.h2p5 => PinnedPositions.AboveDet2p5,
                    Heights.h5   => PinnedPositions.AboveDet5,
                    Heights.h10  => PinnedPositions.AboveDet10,
                    Heights.h20  => PinnedPositions.AboveDet20
                };

            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_PUT_ABV_DET_ASYNC_UNREG) { DetailedText = ex.Message });
            }

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
            Move(axis: Axes.C, coordinate: c_coord);
        }


        #region Stop
        public void StopX() => Stop(Axes.X);
        public void StopY() => Stop(Axes.Y);

        public void StopC() => Stop(Axes.C);

        #endregion Stop

        #region Home
        public void HomeX() 
        {
            Home(Axes.X);
            PinnedPosition = PinnedPositions.HomeX;
        }

        public void HomeY()
        {
            Home(Axes.Y);
            PinnedPosition = PinnedPositions.HomeY;
        }

        public void HomeC() => Home(Axes.C);

        public void Home()
        {
            try
            {
                Connect();
                InitializeAxes();
                ResetAllSoftwareLimits();

                XemoDLL.MB_Delay(1000);

                HomeY();
                XemoDLL.MB_Delay(100);
                Settings.LYDecel = Settings.AxesParams.L_DECEL[0];
                HomeX();
                XemoDLL.MB_Delay(100);
                Settings.LXDecel = Settings.AxesParams.L_DECEL[1];
                HomeC();
                XemoDLL.MB_Delay(100);
                Settings.LCDecel = Settings.AxesParams.L_DECEL[2];

                XemoDLL.MB_Delay(100);
                MoveToC(HomePosition.C);

                HomePosition.C = 0;
                CurrentPosition = HomePosition;

                Settings.SoftwareYUpLimit = MaxY + 1000;
                //Settings.SoftwareYDownLimit = 0;

                //Settings.SoftwareXLeftLimit = 1500;
                Settings.SoftwareXRightLimit = MaxX + 1000;

                Settings.SoftwareCLeftLimit = -MaxC;
                Settings.SoftwareCRightLimit = MaxC;

                IsStopped = false;
                PinnedPosition = PinnedPositions.Home;


            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_HOME_UNREG) { DetailedText = ex.Message });
            }
        }

        public async Task HomeAsync()
        {
            Home();
            await TrackToHomePositionAsync();
            PinnedPosition = PinnedPositions.Home;
        }

        private async Task TrackToHomePositionAsync()
        {
            try
            {
                using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)))
                {
                    while (DeviceIsMoving)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), ct.Token);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS) { DetailedText = "The async tracking task was cancelled by timemout." });
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS_UNREG) { DetailedText = ex.ToString() });

            }

        }

        #endregion

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware
