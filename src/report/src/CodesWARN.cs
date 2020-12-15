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
    public partial struct Codes
    {
        #region Detector

        public const ushort WARN_DET_BUSY = 2000; // Un
        public const ushort WARN_DET_CONN_TIMEOUT = 2001;  // "Can not to disconnect from detector {_name}. Exceeded timeout limit.");
        public const ushort WARN_DET_RST = 2002;
        

        #endregion

    }
}