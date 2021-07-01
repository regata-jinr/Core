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

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("table_Sample")]
    public class Sample
    {
        [Column("F_Country_Code")]
        public string CountryCode         { get; set; } // "RU"
        [Column("F_Client_Id")]
        public string ClientNumber        { get; set; } // 1
        [Column("F_Year")]
        public string Year                { get; set; } // 18
        [Column("F_Sample_Set_Id")]
        public string SetNumber           { get; set; } // 55
        [Column("F_Sample_Set_Index")]
        public string SetIndex            { get; set; } // j
        [Column("A_Sample_ID")]
        public string SampleNumber        { get; set; } // 1
        [Column("A_Client_Sample_ID")]
        public string  ClientSampleId     { get; set; }

        public string  A_Sample_Type      { get; set; }
        public Single? P_Weighting_LLI    { get; set; }
        public Single? P_Weighting_SLI    { get; set; }

        [NotMapped]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";
        [NotMapped]
        public string SampleKey => $"{SetKey}-{SampleNumber}";

        public override string ToString()
        {
            return $"{SampleNumber}-{ClientSampleId}";
        }


    }
}
