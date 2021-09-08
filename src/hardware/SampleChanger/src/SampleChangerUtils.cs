﻿/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Diagnostics;

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {
        public static void ShowDevicesCams()
        {
            Process.Start(@"http://159.93.105.78/");
            Process.Start(@"http://159.93.105.75/");
            Process.Start(@"http://159.93.105.79/");
        }

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware