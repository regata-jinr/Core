/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
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
    ///    -[2700-2799) - XEMO SampleChanger warning codes
    ///    -[2800-2899) - UI:WinForms        warning codes
    /// </summary>
    public partial struct Codes
    {

        #region Cloud

        public const int WRN_CLD_BAD_RSPN = 2100;

        #endregion

        #region Detector

        public const int WARN_DET_BUSY = 2200; // Un
        public const int WARN_DET_CONN_TIMEOUT = 2201;  // "Can not to disconnect from detector {_name}. Exceeded timeout limit.");
        public const int WARN_DET_RST = 2202;
        public const int WARN_DET_FSAVE_DUPL = 2203;
        public const int WARN_DET_FSAVE_NOT_UNIQ_DB = 2204;
        public const int WARN_DET_FSAVE_NOT_UNIQ_LCL = 2205;


        #endregion

        #region Settings

        public const int WARN_SET_FILE_NOT_EXST = 2400;
        #endregion

        #region SampleChanger

        public const int WARN_XM_COM_NOT_FOUND = 2700;

        #endregion

        #region UI:WinForms
        public const int WARN_UI_WF_RDGV_Empty_Data = 2800;
        public const int WARN_UI_WF_RDGV_Wrong_Value_Type = 2801;
        public const int WARN_UI_WF_WRONG_PIN_FORMAT = 2802;
        public const int WARN_UI_WF_EMPTY_FIELD = 2803;
        public const int WARN_UI_WF_UTLT_NULL_CONTRL = 2804;
        public const int WARN_LBL_NOT_EXIST = 2805;
        public const int WARN_FORM_LBL_NOT_EXIST = 2806;
        public const int WARN_UI_WF_ACQ_START_ALL_MEAS = 2807;
        
        #endregion

    }
}