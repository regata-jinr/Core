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
    /// - [1000-2000) - Success codes
    ///    -[1000-1099) - DataBase           success codes
    ///    -[1100-1199) - Cloud              success codes
    ///    -[1200-1299) - Detector           success codes
    ///    -[1300-1399) - Logger             success codes
    ///    -[1400-1499) - Settings           success codes
    ///    -[1500-1599) - Export:Excel       success codes
    ///    -[1600-1699) - Export:GoogleSheet success codes
    ///    -[1700-1799) - Export:CSV         success codes
    ///    -[1800-1899) - UI:WinForms        success codes
    /// </summary>
    public partial  struct Codes
    {
        #region DataBase
        
        public const int SUCC_DB_CONN = 1000;

        #endregion

        #region Cloud

        public const int SUCC_CLD_TRGT      = 1100;
        public const int SUCC_CLD_GOOD_RSPN = 1101;
        public const int SUCC_CLD_UPL_FILE  = 1102;

        #endregion

        #region Detector

        public const int SUCC_DET_RST        = 1200; //$"Success reseting of the detector"
        public const int SUCC_DET_CON        = 1201; //$"Successful connection to the detector"
        public const int SUCC_DET_RECON      = 1202; //$"Reconnection successful"
        public const int SUCC_DET_DCON       = 1203; //$"Disconnection successful"
        public const int SUCC_DET_ACQ_STOP   = 1204;
        public const int SUCC_DET_ACQ_PAUSE  = 1205;
        public const int SUCC_DET_FILE_SAVED = 1206;
        public const int SUCC_DET_CLR_SMPL_INFO = 1207;

        #endregion


        #region UI:Winforms
        public const int SUCC_UI_WF_PIN_SAVED = 1800;
        public const int SUCC_UI_WF_ACQ_DONE = 1801;
        public const int SUCC_UI_WF_ACQ_START = 1802;
        
        #endregion

    }
}