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

using Regata.Core.Messages;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware.Xemo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Regata.Core.Hardware
{
    public enum PinnedPositions { Unknown, Home, AboveDet2p5, AboveDet5, AboveDet10, AboveDet20, AboveDisk, NearDisk, HomeX, HomeY, Moving }

    public partial class SampleChanger
    {
        public Position TargetPosition { get; set; }
        public Position CurrentPosition 
        {
            get
            {
                return new Position()
                {
                    X = XemoDLL.MB_AGet((short)Axes.X, XemoConst.APos),
                    Y = XemoDLL.MB_AGet((short)Axes.Y, XemoConst.APos),
                    C = XemoDLL.MB_AGet((short)Axes.C, XemoConst.APos),
                    SerialNumber = this.SerialNumber,
                    Detector = PairedDetector
                };
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.APos, value.X);
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.APos, value.Y);
                if (value.C.HasValue)
                    XemoDLL.MB_ASet((short)Axes.C, XemoConst.APos, value.C.Value);
            }
        }

        public event Action<SampleChanger> PositionReached;

        private async Task TrackPositionAsync()
        {
            try
            {
                using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)))
                {
                    // FIXME: due to MB_Delay after operatoin DeviceIsMoving is false
                    //        when axis is switching
                    //        DeviceIsMoving
                    while (true)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), ct.Token);

                        if (CurrentPosition == TargetPosition)
                        {
                            PositionReached?.Invoke(this);
                            break;
                        }
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

        public int GetAxisPosition(Axes ax) => XemoDLL.MB_AGet((short)ax, XemoConst.APos);

        public void SetAxisPosition(Axes ax, int val) => XemoDLL.MB_ASet((short)ax, XemoConst.APos, val);



    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

