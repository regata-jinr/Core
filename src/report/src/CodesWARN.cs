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
    /// - [2000-3000) - Warning codes
    ///    -[2000-2099) - DataBase           warning codes
    ///    -[2100-2199) - Cloud              warning codes
    ///    -[2200-2299) - Detector           warning codes
    ///    -[2300-2399) - Logger             warning codes
    ///    -[2400-2499) - Settings           warning codes
    ///    -[2500-2599) - Export:Excel       warning codes
    ///    -[2600-2699) - Export:GoogleSheet warning codes
    ///    -[2700-2799) - Export:CSV         warning codes
    ///    -[2800-2899) - UI:WinForms        warning codes
    /// </summary>
    public partial struct Codes
    {

        #region Cloud

        public const ushort WRN_CLD_BAD_RSPN = 2100;

        #endregion

        #region Detector

        public const ushort WARN_DET_BUSY = 2200; // Un
        public const ushort WARN_DET_CONN_TIMEOUT = 2201;  // "Can not to disconnect from detector {_name}. Exceeded timeout limit.");
        public const ushort WARN_DET_RST = 2202;

        #endregion

        #region

        public const ushort WARN_SET_FILE_NOT_EXST = 2400;

        #endregion


        #region UI:WinForms
        public const ushort WARN_UI_WF_RDGV_Empty_Data = 2800;
        public const ushort WARN_UI_WF_RDGV_Wrong_Value_Type = 2801;

        #endregion

    }
}