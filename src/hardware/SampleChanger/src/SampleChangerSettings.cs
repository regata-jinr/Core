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
using System;

namespace Regata.Core.Hardware
{
    public class SampleChangerSettings
    {

        public int GapBetweenCellsExternalDisk { get; set; }
        public int GapBetweenCellsInternalDisk { get; set; }

        public SampleChanger.AxesParams AxesParams { get; set; } = new SampleChanger.AxesParams();
       
    } // public class SampleChangerSettings
}     // namespace Regata.Core.Hardware
