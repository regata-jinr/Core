/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2018-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


namespace Regata.Core.DataBase.Models
{
    public interface ISample
    {
        string CountryCode         { get; set; } // "RU"
        string ClientNumber        { get; set; } // 1
        string Year                { get; set; } // 18
        string SetNumber           { get; set; } // 55
        string SetIndex            { get; set; } // j
        string SampleNumber        { get; set; } // 1

        //public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";
        //public string SampleKey => $"{SetKey}-{SampleNumber}";

    }
}
