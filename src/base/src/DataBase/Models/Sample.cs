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

using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("table_Sample")]
    public class Sample : ISample, IWeightedSample
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

        [Column("P_Weighting_LLI")]
        public float? LLIWeight    { get; set; }
        [Column("P_Weighting_SLI")]
        public float? SLIWeight { get; set; }

        [NotMapped]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";
        [NotMapped]
        public string SampleKey => $"{SetKey}-{SampleNumber}";

        public override string ToString()
        {
            return $"{SampleNumber}-{ClientSampleId}";
        }

        // I can not use overloading constructors here because EF Core will use it

        public static Sample CastSRM(Standard srm) => new Sample()
        {
            CountryCode = "s",
            ClientNumber = "s",
            Year = "s",
            SetNumber = srm.SetName,
            SetIndex = srm.SetNumber,
            SampleNumber = srm.Number,
            SLIWeight = srm.SLIWeight,
            LLIWeight = srm.LLIWeight
        };

        public static Sample CastMonitor(Monitor mon) => new Sample()
        {
            CountryCode = "m",
            ClientNumber = "m",
            Year = "m",
            SetNumber = mon.SetName,
            SetIndex = mon.SetNumber,
            SampleNumber = mon.Number,
            SLIWeight = mon.SLIWeight,
            LLIWeight = mon.LLIWeight
        };


    } // public class Sample : ISample, IWeightedSample
}     // namespace Regata.Core.DataBase.Models
