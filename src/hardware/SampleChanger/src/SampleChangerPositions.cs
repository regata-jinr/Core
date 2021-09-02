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
    public enum PinnedPositions { Unknown, Home, InsideDetShield, AboveDisk, NearDisk, HomeX, HomeY }

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
                    SerialNumber = this.SerialNumber
                };
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.APos, value.X);
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.APos, value.Y);
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.APos, value.C.HasValue ? value.C.Value : 0);
            }
        }

        public event Action PositionReached;

        private async Task TrackPositionAsync()
        {
            try
            {
                var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2));
                while (DeviceIsMoving)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1000), ct.Token);

                    if (TargetPosition == CurrentPosition)
                    {
                        PositionReached?.Invoke();
                        break;
                    }
                }
                ct.Dispose();
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS) { DetailedText = "The async tracking task was cancelled by timemout." });
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS_UNREG) { DetailedText = ex.Message });

            }

        }

        public int GetAxisPosition(Axes ax) => XemoDLL.MB_AGet((short)ax, XemoConst.APos);

        public void SetAxisPosition(Axes ax, int val) => XemoDLL.MB_ASet((short)ax, XemoConst.APos, val);



    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

