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

            MoveC(Target.C);
            MoveY(Target.Y);
            XemoDLL.MB_Still((short)Axes.Y);
            MoveX(Target.X);
        }

        public void MoveToPosition(Position pos)
        {
            MoveC(pos.C);
            MoveY(pos.Y);
            XemoDLL.MB_Still((short)Axes.Y);
            MoveX(pos.X);
        }

        public void MoveRight()
        {
            Move(Axes.X, Settings.XVelocity, Direction.Positive);
        }

        public void MoveLeft()
        {
            Move(Axes.X, Settings.XVelocity, Direction.Negative);
        }

        public void MoveUp()
        {
            Move(Axes.Y, Settings.YVelocity, Direction.Positive);
        }

        public void MoveDown()
        {
            Move(Axes.Y, Settings.YVelocity, Direction.Negative);
        }

        public void MoveClockwise()
        {
            Move(Axes.C, Settings.CVelocity, Direction.Positive);
        }

        public void MoveСounterclockwise()
        {
            Move(Axes.C, Settings.CVelocity, Direction.Negative);
        }

        public void MoveX(int x_coord)
        {
            MoveAxisToCoordinate(Axes.X, x_coord);
        }
        public void MoveY(int y_coord)
        {
            MoveAxisToCoordinate(Axes.Y, y_coord);
        }
        public void MoveC(int c_coord)
        {
            MoveAxisToCoordinate(Axes.C, c_coord);
        }

        private void MoveAxisToCoordinate(Axes axis, int coord)
        {
            XemoDLL.MB_Amove((short)axis, coord);
        }

        private void Move(Axes axis, int velocity, Direction dir)
        {
            XemoDLL.MB_Jog((short)axis, (int)dir * velocity);
        }

        private void Move(Axes axis, int? coordinate = null, int? velocity = null, Direction? dir = null)
        {
            if (coordinate == null && velocity == null)
            {
                Home(axis);
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

