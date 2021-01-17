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
    /// - [3000-4000) - Error codes
    ///    -[3000-3099) - DataBase           error codes
    ///    -[3100-3199) - Cloud              error codes
    ///    -[3200-3299) - Detector           error codes
    ///    -[3300-3399) - Logger             error codes
    ///    -[3400-3499) - Settings           error codes
    ///    -[3500-3599) - Export:Excel       error codes
    ///    -[3600-3699) - Export:GoogleSheet error codes
    ///    -[3700-3799) - Export:CSV         error codes
    ///    -[3800-3899) - UI:WinForms        error codes
    /// </summary>
    public partial struct Codes
    {

        #region DataBase 
        // 3000 - 3099

        public const ushort ERR_DB_CON = 3000;
        public const ushort ERR_DB_INS = 3001;
        public const ushort ERR_DB_UPD = 3002;
        public const ushort ERR_DB_EXEC = 3003;
        public const ushort ERR_DB_REMOVE = 3004;
        public const ushort ERR_DB_PRIV = 3005;
        public const ushort ERR_DB_EMPTY_CS = 3006;


        #endregion

        #region Cloud Errors
        // 3100 - 3199

        public const ushort ERR_CLD_CON = 3100;
        public const ushort ERR_CLD_UPLD = 3101;
        public const ushort ERR_CLD_TKN = 3102;
        public const ushort ERR_CLD_FND = 3103;
        public const ushort ERR_CLD_DWLD = 3104;
        public const ushort ERR_CLD_TRGT_NFND = 3105;
        public const ushort ERR_CLD_HC_NULL = 3106;
        public const ushort ERR_CLD_UPL_FILE_NFND = 3107;
        public const ushort ERR_CLD_FL_SHRNG_FNFND = 3108;
        public const ushort ERR_CLD_UPL_UNREG = 3109;
        public const ushort ERR_CLD_GEN_TKN = 3110;
        public const ushort ERR_CLD_UPL_FILE = 3111;
        public const ushort ERR_CLD_CON_UNREG = 3112;
        public const ushort ERR_CLD_RMV_FILE_UNREG = 3113;
        public const ushort ERR_CLD_UPL_FILE_UNREG = 3114;
        public const ushort ERR_CLD_FL_SHRNG_UNREG = 3115;
        public const ushort ERR_CLD_IS_EXST_UNREG = 3116;
        public const ushort ERR_CLD_CRT_DIR_UNREG = 3117;
        public const ushort ERR_CLD_BAD_RSPN_UNREG = 3118;
        public const ushort ERR_CLD_SEND_REQ_UNREG = 3119;
        public const ushort ERR_CLD_DWNLD_FILE_UNREG = 3120;
        public const ushort ERR_CLD_DWLD_UNREG = 3121;
        public const ushort ERR_CLD_DWLD_FNFND = 3122;


        #endregion

        #region Detector Errors
        // 3200 - 3299

        public const ushort ERR_DET_NAME_EXSTS = 3200; //$"Detector with name '{name}' wasn't find in the MID wizard 
        public const ushort ERR_DET_NAME_EXSTS_UNREG = 3201;
        public const ushort ERR_DET_CTOR_UNREG = 3202;
        public const ushort ERR_DET_RST_UNREG = 3203;
        public const ushort ERR_DET_AVAIL_UNREG = 3204;
        public const ushort ERR_DET_INTR_CONN_UNREG = 3205;
        public const ushort ERR_DET_CONN_UNREG = 3206;
        public const ushort ERR_DET_DCON_UNREG = 3207;
        public const ushort ERR_DET_ACQ_START_UNREG = 3208;
        public const ushort ERR_DET_ACQ_STOP_UNREG = 3209;
        public const ushort ERR_DET_MEAS_ZERO_DUR = 3210;
        public const ushort ERR_DET_ACQ_PAUSE_UNREG = 3211;
        public const ushort ERR_DET_ACQ_CLR_UNREG = 3212;
        public const ushort ERR_DET_ACQ_HRDW = 3213;
        public const ushort ERR_DET_MSG_UNREG = 3214;
        public const ushort ERR_DET_EFF_H_FILE_NF = 3215;
        public const ushort ERR_DET_EFF_H_FILE_UNREG = 3216;
        public const ushort ERR_DET_EFF_ENG_FILE_NF = 3217;
        public const ushort ERR_DET_EFF_ENG_FILE_UNREG = 3218;
        public const ushort ERR_DET_CHCK_MEAS_UNREG = 3219;
        public const ushort ERR_DET_FSAVE_DCON = 3220;
        public const ushort ERR_DET_MEAS_WRONG_DET = 3221;
        public const ushort ERR_DET_MEAS_EMPTY_FLDS = 3222;
        public const ushort ERR_DET_MEAS_EMPTY = 3223;
        public const ushort ERR_DET_CHCK_IRR_UNREG = 3224;
        public const ushort ERR_DET_IRR_EMPTY_FLDS = 3225;
        public const ushort ERR_DET_IRR_EMPTY = 3226;
        public const ushort ERR_DET_FILE_SAVE_UNREG = 3227;
        public const ushort ERR_DET_FILE_NOT_SAVED = 3228;
        public const ushort ERR_DET_FSAVE_DUPL = 3229;
        public const ushort ERR_DET_GET_DEADT_UNREG = 3230;
        public const ushort ERR_DET_LOAD_SMPL_INFO_UNREG = 3231;
        public const ushort ERR_DET_NAME_N_EXST = 3232;
        public const ushort ERR_DET_RST = 3233;
        public const ushort ERR_DET_NOT_READY = 3235;
        public const ushort ERR_DET_GET_PARAM_UNREG = 3236;
        public const ushort ERR_DET_SET_NULL_PARAM = 3237;
        public const ushort ERR_DET_SET_PARAM_UNREG = 3238;

        #endregion

        #region Logger Errors
        // 3300 - 3399

        #endregion

        #region Settings Errors
        // 3400 - 3499
        public const ushort ERR_SET_MSG_ARR_NULL = 3400;
        public const ushort ERR_SET_LBL_ARR_NULL = 3401;
        public const ushort ERR_SET_LBL_FILE_NOT_EXST = 3402;
        public const ushort ERR_SET_CODE_FILE_NOT_EXST = 3403;
        public const ushort ERR_SET_GET_MSG_UNREG = 3404;
        public const ushort ERR_SET_GET_LBL_UNREG = 3405;
        public const ushort ERR_SET_CODE_FILE_UNREG = 3406;
        public const ushort ERR_SET_LBL_FILE_UNREG = 3407;
        public const ushort ERR_SET_SAVE_UNREG = 3408;
        public const ushort ERR_SET_SAVE_EMPT_ASMBL = 3409;
        public const ushort ERR_SET_RST_UNREG = 3410;
        public const ushort ERR_SET_RST_EMPT_ASMBL = 3411;
        public const ushort ERR_SET_LOAD_UNREG = 3412;
        public const ushort ERR_SET_LOAD_EMPT_ASMBL = 3413;
        public const ushort ERR_SET_GET_FILE_SET_EMPT_ASMBL = 3414;
        public const ushort ERR_SET_SET_ASMBL_NAME_EMPTY = 3415;

        #endregion



        #region Export Errors

        #region Excel Errors
        // 3500 - 3599

        #endregion

        #region Google sheets Errors
        // 3600 - 3699

        #endregion

        #region CSV Errors
        // 3700 - 3799

        #endregion

        #endregion


        #region UI:Winforms

        public const ushort ERR_UI_WF_RDGV_Null_Data                        = 3800;
        public const ushort ERR_UI_WINFORMS_LOGIN_UNREG                     = 3801;
        public const ushort ERR_UI_WINFORMS_LOGIN_ENTER_WRONG_PIN_OR_USER   = 3802;
        public const ushort ERR_UI_WINFORMS_LOGIN_ENTER_WRONG_LOGIN_OR_PASS = 3803;
        public const ushort ERR_UI_WINFORMS_LOGIN_ENTER_UNREG               = 3804;
        public const ushort ERR_UI_WINFORMS_LOGIN_ENTER_PIN_NOT_FOUND       = 3805;
        public const ushort ERR_UI_WINFORMS_LOGIN_APP_ALREADY_OPENED        = 3806;
        public const ushort ERR_UI_WINFORMS_CRT_PIN_UNREG                   = 3807;
        public const ushort ERR_UI_WINFORMS_CRT_PIN_FIELD_UNREG             = 3808;
        public const ushort ERR_UI_WINFORMS_CHCK_PSWD_UNREG                 = 3809;
        public const ushort ERR_UI_WINFORMS_CHCK_PIN_UNREG                  = 3810;
        public const ushort ERR_UI_WINFORMS_ADD_PIN_UNREG                   = 3811;
        


        #endregion

    }
}