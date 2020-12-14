/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


using CanberraDeviceAccessLib;
using System;

namespace Regata.Core.Hardware
{
   public class DetectorSettings
    {
        public string           Name                  { get; set; }
        public ConnectOptions   ConnectOption         { get; set; }
        public uint             CountNumber           { get; set; }
        public TimeSpan         ConnectionTimeOut     { get; set; }
        public AcquisitionModes AcquisitionMode       { get; set; }
        public string           EffCalFolder          { get; set; }
    }
}
