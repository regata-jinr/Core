/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/


namespace Regata.Core.Settings.Codes
{
    public enum RErrorCodes 
    {
        Empty=0,
        Unknown = 0,
        #region DataBase Errors
        ERR_DB_CON,
        ERR_DB_INS,
        ERR_DB_UPD,
        ERR_DB_EXEC,
        ERR_DB_REMOVE,
        ERR_DB_PRIV,
        #endregion

        #region Hardware Errors

        #region Detector Errors
        ERR_DET_CON,
        ERR_ACQ,
        ERR_SPEC_SAVE,
        #endregion

        #region Sample Changer Errors
        #endregion

        #region Scales Errors
        #endregion

        #endregion

        #region Logger Errors
        #endregion

        #region Settings Errors
        #endregion

        #region Cloud Errors
        ERR_CLOUD_CON,
        ERR_CLOUD_UPLD,
        ERR_CLOUD_TKN,
        ERR_CLOUD_FND,
        ERR_CLOUD_DWLD

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