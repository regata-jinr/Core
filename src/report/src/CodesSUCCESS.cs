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


/// <summary>
/// This file contains definition of basic codes. The idea of such usage is support general code for any application and also allow user to extend code by his own codes. One of the disadvanteges is hot to guarantee unique code identifier in case of user will extend it? Perhaps attributes?
/// </summary>
namespace Regata.Core
{
    /// <summary>
    /// Codes class contains unique identificator of message. 
    /// - [0-1000) - Info codes
    /// - [1000-2000) - Success codes
    /// - [2000-3000) - Warning codes
    /// - [3000-4000) - Error codes
    /// </summary>
    public partial  struct Codes
    {

        #region Detector

        public const ushort SUCC_DET_RST = 1010; //$"Success reseting of the detector"
        public const ushort SUCC_DET_CON = 1011; //$"Successful connection to the detector"
        public const ushort SUCC_DET_RECON = 1012; //$"Reconnection successful"
        public const ushort SUCC_DET_DCON = 1013; //$"Disconnection successful"
        public const ushort SUCC_DET_ACQ_STOP = 1014;
        public const ushort SUCC_DET_ACQ_PAUSE = 1015;
        #endregion





    }
}