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
using System.Threading.Tasks;
using Regata.Core.Hardware.Xemo;

namespace Regata.Core.Hardware
{
    public struct Position
    {
        public Position(int x, int y, int c)
        {
            X = x;
            Y = y;
            C = c;
        }

        public int X;
        public int Y;
        public int C;

        public override string ToString() => $"X = {X}, Y = {Y}, C = {C}";

    }

    public partial class SampleChanger
    {
        public bool IsPositiveSwitcher => false;
        public bool IsNegativeSwitcher => false;
        public bool IsReferenceSwitcher => false;

        public Position Target { get; set; }
        public Position Current 
        {
            get
            {
                return new Position()
                {
                    X = XemoDLL.MB_AGet((short)Axes.X, XemoConst.APos),
                    Y = XemoDLL.MB_AGet((short)Axes.Y, XemoConst.APos),
                    C = XemoDLL.MB_AGet((short)Axes.C, XemoConst.APos)
                };
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.APos, value.X);
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.APos, value.Y);
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.APos, value.C);
            }
        }


    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

