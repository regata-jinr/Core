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

        #region DataBase

        public const ushort ERR_DB_CON = 3000;
        public const ushort ERR_DB_INS = 3001;
        public const ushort ERR_DB_UPD = 3002;
        public const ushort ERR_DB_EXEC = 3003;
        public const ushort ERR_DB_REMOVE = 3004;
        public const ushort ERR_DB_PRIV = 3005;

        #endregion

        #region Cloud Errors

        public const ushort ERR_CLOUD_CON = 3006;
        public const ushort ERR_CLOUD_UPLD = 3007;
        public const ushort ERR_CLOUD_TKN = 3008;
        public const ushort ERR_CLOUD_FND = 3009;
        public const ushort ERR_CLOUD_DWLD = 3010;

        #endregion

        #region Logger Errors
        #endregion

        #region Settings Errors
        #endregion

        #region Detector Errors

        public const ushort ERR_DET_NAME_EXSTS = 3011; //$"Detector with name '{name}' wasn't find in the MID wizard list. Status will change to 'error'";
        public const ushort ERR_DET_NAME_EXSTS_UNREG = 3012; // Un
        public const ushort ERR_DET_CTOR_UNREG = 3013; // Un
        public const ushort ERR_DET_RST_UNREG = 3014; // Un
        public const ushort ERR_DET_AVAIL_UNREG = 3015; // Un
        public const ushort ERR_DET_INTR_CONN_UNREG = 3016; // Un
        public const ushort ERR_DET_CONN_UNREG = 3017; // Un
        public const ushort ERR_DET_DCON_UNREG = 3018; // Un
        public const ushort ERR_DET_ACQ_START_UNREG = 3019;
        public const ushort ERR_DET_ACQ_STOP_UNREG = 3020;
        public const ushort ERR_DET_ACQ_PAUSE_UNREG = 3021;
        public const ushort ERR_DET_ACQ_CLR_UNREG = 3022;
        public const ushort ERR_DET_ACQ_HRDW = 3023;
        public const ushort ERR_DET_MSG_UNREG = 3024;

        public const ushort ERR_DET_EFF_H_FILE_NF = 3025;
        public const ushort ERR_DET_EFF_H_FILE_UNREG = 3026;
        public const ushort ERR_DET_EFF_ENG_FILE_NF = 3027;
        public const ushort ERR_DET_EFF_ENG_FILE_UNREG = 3028;

        public const ushort ERR_DET_CHCK_MEAS_UNREG = 3029;
        public const ushort ERR_DET_MEAS_ZERO_DUR = 3030;
        public const ushort ERR_DET_MEAS_WRONG_DET = 3031;
        public const ushort ERR_DET_MEAS_EMPTY_FLDS = 3032;
        public const ushort ERR_DET_MEAS_EMPTY = 3033;
        public const ushort ERR_DET_CHCK_IRR_UNREG = 3034;
        public const ushort ERR_DET_IRR_EMPTY_FLDS = 3035;
        public const ushort ERR_DET_IRR_EMPTY = 3036;
        public const ushort ERR_DET_FILE_SAVE_UNREG = 3037;
        public const ushort ERR_DET_FILE_NOT_SAVED = 3038;
        public const ushort ERR_DET_FSAVE_DUPL = 3039;
        public const ushort ERR_DET_FSAVE_DCON = 3040;
        public const ushort ERR_DET_LOAD_SMPL_INFO_UNREG = 3041;
        public const ushort ERR_DET_NAME_N_EXST = 3042;
        public const ushort ERR_DET_RST = 3043;
        public const ushort ERR_DET_NOT_READY = 3045;

        public const ushort ERR_DET_GET_PARAM_UNREG = 3046;
        public const ushort ERR_DET_SET_NULL_PARAM = 3047;
        public const ushort ERR_DET_SET_PARAM_UNREG = 3048;
        public const ushort ERR_DET_GET_DEADT_UNREG = 3049;

        #endregion

        #region Export Errors

        #region Excel Errors
        #endregion

        #region Google sheets Errors
        #endregion

        #region CSV Errors
        #endregion

        #endregion



    }
}