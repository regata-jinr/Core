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


using System.Collections.Generic;
using System;


/// <summary>
/// This file contains definition of basic codes. The idea of such usage is support general code for any application and also allow user to extend code by his own codes. One of the disadvanteges is hot to guarantee unique code identifier in case of user will extend it? Perhaps attributes?
/// </summary>
namespace Regata.Core
{
    /// <summary>
    /// Codes class contains unique identificator of message. 
    /// - [0-1000) - Info codes
    ///    -[  0- 99) - DataBase           info codes
    ///    -[100-199) - Cloud              info codes
    ///    -[200-299) - Detector           info codes
    ///    -[300-399) - Logger             info codes
    ///    -[400-499) - Settings           info codes
    ///    -[500-599) - Export:Excel       info codes
    ///    -[600-699) - Export:GoogleSheet info codes
    ///    -[700-799) - Export:CSV         info codes
    ///    -[800-899) - UI:WinForms        info codes
    /// </summary>
    public partial struct Codes
    {

        public static bool Contains(ushort v)
        {
            var codes = new List<ushort>();
            foreach (var f in typeof(Codes).GetFields())
            {
                var c = f.GetRawConstantValue();
                if (!(c is ushort)) throw new InvalidOperationException("The type of code properties should be ushort");
                if (codes.Contains((ushort)c)) throw new InvalidOperationException($"The code {(ushort)c} already exist. Give the new code for the property {f.Name}");
                codes.Add((ushort)f.GetValue(null));
            }

            return codes.Contains(v);
        }


        public const ushort UNREG = 0;


        #region DataBase

        public const ushort INFO_DB_CON    = 1;
        public const ushort INFO_DB_INS    = 2;
        public const ushort INFO_DB_UPD    = 3;
        public const ushort INFO_DB_EXEC   = 4;
        public const ushort INFO_DB_REMOVE = 5;
        public const ushort INFO_DB_PRIV   = 6;
        public const ushort INFO_DB_TEST   = 7;

        #endregion


        #region Cloud

        public const ushort INFO_CLD_UPL_FILE = 100;
        public const ushort INFO_CLD_RMV_FILE = 101;
        public const ushort INFO_CLD_IS_EXST  = 102;
        public const ushort INFO_CLD_FL_SHRNG = 103;
        public const ushort INFO_CLD_CRT_DIR  = 104;
        public const ushort INFO_CLD_DEL_FL_DIR = 105;

        #endregion

        #region Detector

        public const ushort INFO_DET_NAME_EXSTS       = 200;  //.Info($"Detector with name '{name}' was found in the MID wizard
        public const ushort INFO_DET_CHNG_STATUS      = 201; // detector status has changed
        public const ushort INFO_DET_CLN              = 202; //$"Cleaning of the detector {Name}"
        public const ushort INFO_DET_RST              = 203; //$"Reseting of the detector {Name}"
        public const ushort INFO_DET_RECON            = 204; //        $"Attempt to reconnect to the detector."
        public const ushort INFO_DET_DCON             = 205; // $"Disconnecting from the detector.");
        public const ushort INFO_DET_ACQ_START        = 206; // acqusition has started
        public const ushort INFO_DET_ACQ_STOP         = 207;
        public const ushort INFO_DET_ACQ_PAUSE        = 208;
        public const ushort INFO_DET_ACQ_CLR          = 209;
        public const ushort INFO_DET_ACQ_DONE         = 210;
        public const ushort INFO_DET_ACQ_COUNTS_CHNG  = 211;
        public const ushort INFO_DET_ACQ_MODE_CHNG    = 212;
        public const ushort INFO_DET_EFF_H_FILE_ADD   = 213; //$"Efficiency file {effFileName} will add to the detector");
        public const ushort INFO_DET_EFF_ENG_FILE_ADD = 214; //$"Efficiency file {effFileName} will add to the detector");
        public const ushort INFO_DET_SAVED            = 215;
        public const ushort INFO_DET_LOAD_SMPL_INFO   = 216;

        #endregion

        #region Settings

        public const ushort INFO_SET_SET_ASMBL_NAME = 400;

        #endregion

    }
}
