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
    ///    -[3900-3999) - UI:WinForms        error codes
    /// </summary>
    public partial struct Codes
    {

        #region DataBase 
        // 3000 - 3099

        public const int ERR_DB_CON = 3000;
        public const int ERR_DB_INS = 3001;
        public const int ERR_DB_UPD = 3002;
        public const int ERR_DB_EXEC = 3003;
        public const int ERR_DB_REMOVE = 3004;
        public const int ERR_DB_PRIV = 3005;
        public const int ERR_DB_EMPTY_CS = 3006;


        #endregion

        #region Cloud Errors
        // 3100 - 3199

        public const int ERR_CLD_CON = 3100;
        public const int ERR_CLD_UPLD = 3101;
        public const int ERR_CLD_TKN = 3102;
        public const int ERR_CLD_FND = 3103;
        public const int ERR_CLD_DWLD = 3104;
        public const int ERR_CLD_TRGT_NFND = 3105;
        public const int ERR_CLD_HC_NULL = 3106;
        public const int ERR_CLD_UPL_FILE_NFND = 3107;
        public const int ERR_CLD_FL_SHRNG_FNFND = 3108;
        public const int ERR_CLD_UPL_UNREG = 3109;
        public const int ERR_CLD_GEN_TKN = 3110;
        public const int ERR_CLD_UPL_FILE = 3111;
        public const int ERR_CLD_CON_UNREG = 3112;
        public const int ERR_CLD_RMV_FILE_UNREG = 3113;
        public const int ERR_CLD_UPL_FILE_UNREG = 3114;
        public const int ERR_CLD_FL_SHRNG_UNREG = 3115;
        public const int ERR_CLD_IS_EXST_UNREG = 3116;
        public const int ERR_CLD_CRT_DIR_UNREG = 3117;
        public const int ERR_CLD_BAD_RSPN_UNREG = 3118;
        public const int ERR_CLD_SEND_REQ_UNREG = 3119;
        public const int ERR_CLD_DWNLD_FILE_UNREG = 3120;
        public const int ERR_CLD_DWLD_UNREG = 3121;
        public const int ERR_CLD_DWLD_FNFND = 3122;
        public const int ERR_CLD_DEL_FIL_DIR_UNREG = 3123;

        #endregion

        #region Detector Errors
        // 3200 - 3299

        public const int ERR_DET_NAME_EXSTS = 3200; //$"Detector with name '{name}' wasn't find in the MID wizard 
        public const int ERR_DET_NAME_EXSTS_UNREG = 3201;
        public const int ERR_DET_CTOR_UNREG = 3202;
        public const int ERR_DET_RST_UNREG = 3203;
        public const int ERR_DET_AVAIL_UNREG = 3204;
        public const int ERR_DET_INTR_CONN_UNREG = 3205;
        public const int ERR_DET_CONN_UNREG = 3206;
        public const int ERR_DET_DCON_UNREG = 3207;
        public const int ERR_DET_ACQ_START_UNREG = 3208;
        public const int ERR_DET_ACQ_STOP_UNREG = 3209;
        public const int ERR_DET_MEAS_ZERO_DUR = 3210;
        public const int ERR_DET_ACQ_PAUSE_UNREG = 3211;
        public const int ERR_DET_ACQ_CLR_UNREG = 3212;
        public const int ERR_DET_ACQ_HRDW = 3213;
        public const int ERR_DET_MSG_UNREG = 3214;
        public const int ERR_DET_EFF_H_FILE_NF = 3215;
        public const int ERR_DET_EFF_H_FILE_UNREG = 3216;
        public const int ERR_DET_EFF_ENG_FILE_NF = 3217;
        public const int ERR_DET_EFF_ENG_FILE_UNREG = 3218;
        public const int ERR_DET_CHCK_MEAS_UNREG = 3219;
        public const int ERR_DET_FSAVE_DCON = 3220;
        public const int ERR_DET_MEAS_WRONG_DET = 3221;
        public const int ERR_DET_MEAS_EMPTY_FLDS = 3222;
        public const int ERR_DET_MEAS_EMPTY = 3223;
        public const int ERR_DET_CHCK_IRR_UNREG = 3224;
        public const int ERR_DET_IRR_EMPTY_FLDS = 3225;
        public const int ERR_DET_IRR_EMPTY = 3226;
        public const int ERR_DET_FILE_SAVE_UNREG = 3227;
        public const int ERR_DET_FILE_NOT_SAVED = 3228;
        public const int ERR_DET_GET_DEADT_UNREG = 3230;
        public const int ERR_DET_LOAD_SMPL_INFO_UNREG = 3231;
        public const int ERR_DET_NAME_N_EXST = 3232;
        public const int ERR_DET_RST = 3233;
        public const int ERR_DET_NOT_READY = 3235;
        public const int ERR_DET_GET_PARAM_UNREG = 3236;
        public const int ERR_DET_SET_NULL_PARAM = 3237;
        public const int ERR_DET_SET_PARAM_UNREG = 3238;
        public const int ERR_DET_SMPL_CNVTR_UNREG = 3239;
        public const int ERR_DET_SMPL_CNVTR = 3240;
        public const int ERR_DET_EFF_DIR_EMPTY = 3241;

        #endregion

        #region Logger Errors
        // 3300 - 3399
        public const int ERR_REP_SEND_MAIL = 3300;


        #endregion

        #region Settings Errors
        // 3400 - 3499
        public const int ERR_SET_MSG_ARR_NULL = 3400;
        public const int ERR_SET_LBL_ARR_NULL = 3401;
        public const int ERR_SET_LBL_FILE_NOT_EXST = 3402;
        public const int ERR_SET_CODE_FILE_NOT_EXST = 3403;
        public const int ERR_SET_GET_MSG_UNREG = 3404;
        public const int ERR_SET_GET_LBL_UNREG = 3405;
        public const int ERR_SET_CODE_FILE_UNREG = 3406;
        public const int ERR_SET_LBL_FILE_UNREG = 3407;
        public const int ERR_SET_SAVE_UNREG = 3408;
        public const int ERR_SET_SAVE_EMPT_ASMBL = 3409;
        public const int ERR_SET_RST_UNREG = 3410;
        public const int ERR_SET_RST_EMPT_ASMBL = 3411;
        public const int ERR_SET_LOAD_UNREG = 3412;
        public const int ERR_SET_LOAD_EMPT_ASMBL = 3413;
        public const int ERR_SET_GET_FILE_SET_EMPT_ASMBL = 3414;
        public const int ERR_SET_SET_ASMBL_NAME_EMPTY = 3415;

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


        #region UI:Winforms Errors

        public const int ERR_UI_WF_RDGV_Null_Data                  = 3800;
        public const int ERR_UI_WF_LOGIN_UNREG                     = 3801;
        public const int ERR_UI_WF_LOGIN_ENTER_WRONG_PIN_OR_USER   = 3802;
        public const int ERR_UI_WF_LOGIN_ENTER_WRONG_LOGIN_OR_PASS = 3803;
        public const int ERR_UI_WF_LOGIN_ENTER_UNREG               = 3804;
        public const int ERR_UI_WF_LOGIN_ENTER_PIN_NOT_FOUND       = 3805;
        public const int ERR_UI_WF_LOGIN_APP_ALREADY_OPENED        = 3806;
        public const int ERR_UI_WF_CRT_PIN_UNREG                   = 3807;
        public const int ERR_UI_WF_CRT_PIN_FIELD_UNREG             = 3808;
        public const int ERR_UI_WF_CHCK_PSWD_UNREG                 = 3809;
        public const int ERR_UI_WF_CHCK_PIN_UNREG                  = 3810;
        public const int ERR_UI_WF_ADD_PIN_UNREG                   = 3811;
        public const int ERR_UI_WF_CRT_PIN_WRONG_PASS_OR_USER      = 3812;
        public const int ERR_UI_WF_TIME_CHNG_UNREG                 = 3813;
        public const int ERR_UI_WF_DET_SAVE_UNREG                  = 3814;
        public const int ERR_UI_WF_DET_STOP_UNREG                  = 3815;
        public const int ERR_UI_WF_DET_HGHT_CHNG_UNREG             = 3816;
        public const int ERR_UI_WF_DET_STAT_CHNG_UNREG             = 3817;
        public const int ERR_UI_WF_DCP_PAUSE_UNREG                 = 3818;
        public const int ERR_UI_WF_DCP_CLR_UNREG                   = 3819;
        public const int ERR_UI_WF_DCP_REFR_TIME_UNREG             = 3820;
        public const int ERR_UI_WF_DCP_INIT_UNREG                  = 3821;


        #endregion

        #region Scales Errors

        public const int ERR_SCL_EMPT_COM = 3900;
        public const int ERR_SCL_GET_WGHT = 3901;
        public const int ERR_SCL_UNREG    = 3902;

        #endregion


    } // public partial struct Codes
}     // namespace Regata.Core
