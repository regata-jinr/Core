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

        private Task TrackPositionAsync()
        {
            var ct = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            return Task.Run(() =>
            {
                while (DeviceIsMoving)
                {
                    if (TargetPosition == CurrentPosition)
                    {
                        PositionReached?.Invoke();
                        break;
                    }
                }
            }, ct.Token);
        }

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
}     // namespace Measurements.Core.Hardware

