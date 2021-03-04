/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DB.MSSQL.Models
{
    public enum MeasurementsType { sli, lli1, lli2, bckg };

    [AutoMap(typeof(Irradiation))]
    public class Measurement
    {
        [Key]
        [Ignore]
        public int        Id                   { get; set; }
        [Required]
        [SourceMember(nameof(Irradiation.Id))]
        public int        IrradiationId        { get; set; }
        public int        RegId               { get; set; }
        [Required]
        public string     CountryCode          { get; set; }
        [Required]
        public string     ClientNumber         { get; set; }
        [Required]
        public string     Year                 { get; set; }
        [Required]
        public string     SetNumber            { get; set; }
        [Required]
        public string     SetIndex             { get; set; }
        [Required]
        public string     SampleNumber         { get; set; }
        [Required]
        public int        Type                 { get; set; }
        [Ignore]
        public string     AcqMode              { get; set; }
        [Ignore]
        public uint?      DiskPosition         { get; set; }
        [Ignore]
        public DateTime?  DateTimeStart        { get; set; }
        [Ignore]
        public uint?      Duration             { get; set; }
        [Ignore]
        public DateTime?  DateTimeFinish       { get; set; }
        [Ignore]
        public float?     DeadTime             { get; set; }
        [Ignore]
        public float?     Height               { get; set; }
        [Ignore]
        public string     FileSpectra          { get; set; }
        [Ignore]
        public string     Detector             { get; set; }
        [Ignore]
        public int        Assistant            { get; set; }
        [Ignore]
        public string     Note                 { get; set; }

        [NotMapped]
        [Ignore]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";

        [NotMapped]
        [Ignore]
        public string SampleKey => $"{SetIndex}-{SampleNumber}";
        public override string ToString() => $"{SetKey}-{SampleNumber}";

        [NotMapped]
        [Ignore]
        public static readonly IReadOnlyDictionary<MeasurementsType, string> TypeToString = new Dictionary<MeasurementsType, string> { { MeasurementsType.sli, "SLI" }, { MeasurementsType.lli1,"LLI-1" }, { MeasurementsType.lli2, "LLI-2" }, { MeasurementsType.bckg, "BCKG" } };

    } // public class Measurement

}     // namespace Regata.Core.DB.MSSQL.Models
