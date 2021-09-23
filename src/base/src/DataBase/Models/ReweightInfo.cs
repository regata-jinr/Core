/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("reweightInfo")]
    public class ReweightInfo : IReWeightedSample, IId
    {
        [NotMapped]
        public int Id { get; set; }

        [Column("loadNumber")]
        public int LoadNumber { get; set; }

        [Column("Country_Code")]
        public string CountryCode { get; set; } // "RU"

        [Column("Client_ID")]
        public string ClientNumber { get; set; } // 1

        [Column("Year")]
        public string Year { get; set; } // 18

        [Column("Sample_Set_ID")]
        public string SetNumber { get; set; } // 55

        [Column("Sample_Set_Index")]
        public string SetIndex { get; set; } // j

        [Column("Sample_ID")]
        public string SampleNumber { get; set; } // 1

        [Column("Container_Number")]
        public short? ContainerNumber { get; set; }

        [Column("Position_Number")]
        public short? PositionNumber { get; set; }

        [Column("InitWght")]
        public float? InitWght { get; set; }

        [Column("EmptyContWght")]
        public float? EmptyContWght { get; set; }
        [Column("ContWithSampleWght")]
        public float? ContWithSampleWght { get; set; }

        [Column("ARepackWght")]
        public float? ARepackWght { get; set; }

        [Column("Diff")]
        public float? Diff { get; set; }

    } // public class ReweightInfo
}     // namespace Regata.Core.DataBase.Models