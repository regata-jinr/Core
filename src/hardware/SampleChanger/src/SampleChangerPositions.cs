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
                    X = GetAxisPosition(Axes.X),
                    Y = GetAxisPosition(Axes.Y),
                    C = GetAxisPosition(Axes.C),
                    SerialNumber = this.SerialNumber,
                    Detector = PairedDetector
                };
            }
            set
            {
                SetAxisPosition(Axes.X, value.X);
                SetAxisPosition(Axes.Y, value.Y);
                if (value.C.HasValue)
                    SetAxisPosition(Axes.C, value.C.Value);
            }
        }

        public event Action<SampleChanger> PositionReached;

        private int TracksCount = 0;

        public async Task TrackPositionAsync(CancellationToken ct)
        {
            SelectCurrentComPort();
            try
            {
                if (TracksCount > 1)
                {
                    TracksCount = 0;
                    return;
                }
                //if (ct == CancellationToken.None)
                //ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;
                // FIXME: due to MB_Delay after operatoin DeviceIsMoving is false
                //        when axis is switching
                //        DeviceIsMoving
                await Task.Delay(TimeSpan.FromSeconds(2), ct).ConfigureAwait(false);

                while (DeviceIsMoving && TracksCount <= 1)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), ct).ConfigureAwait(false);

                    if (CurrentPosition == TargetPosition)
                    {
                        PositionReached?.Invoke(this);
                        break;
                    }
                }

                TracksCount++;
                await TrackPositionAsync(ct);

            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS) { DetailedText = "The async tracking task was cancelled by timemout." });
                throw;
                //Stop();
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS_UNREG) { DetailedText = ex.ToString() });

            }

        }

        private async Task TrackToHomePositionAsync(CancellationToken ct)
        {
            try
            {
                while (DeviceIsMoving)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), ct).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS) { DetailedText = "The async tracking task was cancelled by timemout." });
                throw;
                //Stop();
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_TRCK_POS_UNREG) { DetailedText = ex.ToString() });
            }
        }


        public int GetAxisPosition(Axes ax) => GetAxisParameter(ax, XemoConst.APos);

        public void SetAxisPosition(Axes ax, int val) => SetAxisParameter(ax, XemoConst.APos, val);



    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

