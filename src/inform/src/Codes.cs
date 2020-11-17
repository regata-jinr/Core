/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/
/// <summary>
/// This file contains definition of basic codes. The idea of such usage is support general code for any application and also allow user to extend code by his own codes. One of the disadvanteges is hot to guarantee unique code identifier in case of user will extend it? Perhaps attributes?
/// </summary>
namespace Regata.Core.Inform.Codes
{


    public class InfoCodes
    {

    }

    public class SuccessCodes
    {

    }

    public class WarningCodes
    {

    }

    public enum ErrorCodes : uint
    {
        UNREGSTR      = 0,

        #region DataBase  Errors
        ERR_DB_CON    = 1,
        ERR_DB_INS    = 2,
        ERR_DB_UPD    = 3,
        ERR_DB_EXEC   = 4,
        ERR_DB_REMOVE = 5,
        ERR_DB_PRIV   = 6,
        #endregion

        #region Logger Errors
        #endregion

        #region Settings Errors
        #endregion

        #region Cloud Errors
        ERR_CLOUD_CON  = 7,
        ERR_CLOUD_UPLD = 8,
        ERR_CLOUD_TKN  = 9,
        ERR_CLOUD_FND  = 10,
        ERR_CLOUD_DWLD = 11

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