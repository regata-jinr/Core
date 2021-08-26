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

namespace Regata.Core.Hardware
{
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


    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

