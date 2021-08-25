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

        private void Move(Axes axis, int? coordinate = null, int? velocityScalingFactor = null, Direction? dir = null)
        {
            _activeAxis = axis;

            if (coordinate == null && velocityScalingFactor == null)
            {
                Home(axis);
                return;
            }

            if (coordinate.HasValue)
            {
                XemoDLL.MB_Amove((short)axis, coordinate.Value);
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
            HomeY();
            HomeX();
            HomeC();
        }

        private void Home(Axes ax)
        {
            XemoDLL.MB_Home((short)ax);
        }
        #endregion

        public int GetAxisPosition(Axes ax)
        {
            return ax switch
            {
                Axes.X => CurrentPosition.X,
                Axes.Y => CurrentPosition.Y,
                Axes.C => CurrentPosition.C,
                _ => -444
            };
        }


    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware

