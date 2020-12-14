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

        public const ushort ERR_DB_CON    = 3000;
        public const ushort ERR_DB_INS    = 3001;
        public const ushort ERR_DB_UPD    = 3002;
        public const ushort ERR_DB_EXEC   = 3003;
        public const ushort ERR_DB_REMOVE = 3004;
        public const ushort ERR_DB_PRIV   = 3005;

        #endregion

        #region Cloud Errors

        public const ushort ERR_CLOUD_CON  = 3006;
        public const ushort ERR_CLOUD_UPLD = 3007;
        public const ushort ERR_CLOUD_TKN  = 3008;
        public const ushort ERR_CLOUD_FND  = 3009;
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
        public const ushort ERR_DET_INTR_CONN = 3016; // Un
        public const ushort ERR_DET_CONN = 3017; // Un
        public const ushort ERR_DET_DCON = 3018; // Un
        public const ushort ERR_DET_ACQ_START= 3019;
        public const ushort ERR_DET_ACQ_STOP = 3020;
        public const ushort ERR_DET_ACQ_PAUSE = 3021;
        public const ushort ERR_DET_ACQ_CLR  = 3022;
        public const ushort ERR_DET_ACQ_HRDW = 3023;
        public const ushort ERR_DET_MSG = 3024;



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