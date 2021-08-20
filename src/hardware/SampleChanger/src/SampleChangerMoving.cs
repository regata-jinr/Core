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

using System;
using Regata.Core.Hardware.Xemo;

namespace Regata.Core.Hardware
{
    enum Direction { Negative = -1, Positive = 1 }
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

        public void MoveToPosition(Position pos)
        {
            MoveToC(pos.C);
            MoveToY(pos.Y);
            XemoDLL.MB_Still((short)Axes.Y);
            MoveToX(pos.X);
        }

        public void MoveRight()
        {
            Move(Axes.X, velocity: Settings.XVelocity, dir: Direction.Positive);
        }

        public void MoveLeft()
        {
            Move(Axes.X, velocity: Settings.XVelocity, dir: Direction.Negative);
        }

        public void MoveUp()
        {
            Move(Axes.Y, velocity: Settings.YVelocity, dir: Direction.Positive);
        }

        public void MoveDown()
        {
            Move(Axes.Y, velocity: Settings.YVelocity, dir: Direction.Negative);
        }

        public void MoveClockwise()
        {
            Move(Axes.C, velocity: Settings.CVelocity, dir: Direction.Positive);
        }

        public void MoveСounterclockwise()
        {
            Move(Axes.C, velocity: Settings.CVelocity, dir: Direction.Negative);
        }

        public void MoveToX(int x_coord)
        {
            Move(axis: Axes.X, coordinate: x_coord);
        }
        public void MoveToY(int y_coord)
        {
            Move(axis: Axes.Y, coordinate: y_coord);

        }
        public void MoveToC(int c_coord)
        {
            Move(axis: Axes.C, coordinate:c_coord);
        }

        private void Move(Axes axis, int? coordinate = null, int? velocity = null, Direction? dir = null)
        {
            if (coordinate == null && velocity == null)
            {
                Home(axis);
                return;
            }

            if (coordinate.HasValue)
            {
                XemoDLL.MB_Amove((short)axis, coordinate.Value);
                return;
            }

            if (velocity.HasValue && dir.HasValue)
            {
                XemoDLL.MB_Jog((short)axis, (int)dir.Value * velocity.Value);
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
            HomeY();
            HomeX();
            HomeC();
        }

        private void Home(Axes ax)
        {
            XemoDLL.MB_Home((short)ax);
        }
        #endregion



    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

