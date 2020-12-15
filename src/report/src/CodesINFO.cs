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
    /// - [1000-2000) - Success codes
    /// - [2000-3000) - Warning codes
    /// - [3000-4000) - Error codes
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

        #endregion

        #region Detector

        public const ushort INFO_DET_NAME_EXSTS = 7;  //.Info($"Detector with name '{name}' was found in the MID wizard list and will be used");
        public const ushort INFO_DET_CHNG_STATUS = 8; // detector status has changed
        public const ushort INFO_DET_CLN = 9; //$"Cleaning of the detector {Name}"
        public const ushort INFO_DET_RST = 10; //$"Reseting of the detector {Name}"
        public const ushort INFO_DET_RECON = 11; //        $"Attempt to reconnect to the detector."
        public const ushort INFO_DET_DCON = 12; // $"Disconnecting from the detector.");
        public const ushort INFO_DET_ACQ_START = 13; // acqusition has started
        public const ushort INFO_DET_ACQ_STOP        = 14;
        public const ushort INFO_DET_ACQ_PAUSE       = 15;
        public const ushort INFO_DET_ACQ_CLR         = 16;
        public const ushort INFO_DET_ACQ_DONE        = 17;
        public const ushort INFO_DET_ACQ_COUNTS_CHNG = 18;
        public const ushort INFO_DET_ACQ_MODE_CHNG   = 19;

        public const ushort INFO_DET_EFF_H_FILE_ADD = 20; //$"Efficiency file {effFileName} will add to the detector");
        public const ushort INFO_DET_EFF_ENG_FILE_ADD = 21; //$"Efficiency file {effFileName} will add to the detector");

        public const ushort INFO_DET_SAVED = 22;
        public const ushort INFO_DET_LOAD_SMPL_INFO = 23;

        #endregion

    }
}
