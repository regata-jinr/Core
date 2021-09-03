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

using Regata.Core.Hardware.Xemo;
using Regata.Core.Messages;
using System;

namespace Regata.Core.Hardware
{
    public enum Direction { Negative = -1, Positive = 1 }

    public partial class SampleChanger
    {

        private void Move(Axes axis, int? coordinate = null, int? speed = null, Direction? dir = null)
        {
            try
            {
                if (IsStopped)
                    return;

                _activeAxis = axis;

                if (coordinate == null && speed == null)
                    return;

                PinnedPosition = PinnedPositions.Moving;

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
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_MOVE_UNREG) { DetailedText = ex.Message });

            }
        }

        private void Stop(Axes ax)
        {
            XemoDLL.MB_Stop((short)ax);
        }

        public void Stop()
        {
            BreakSystemProgram();
        }

        private void Home(Axes ax)
        {
            XemoDLL.MB_Home((short)ax);
            XemoDLL.MB_Still((short)ax);
        }
      

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware

