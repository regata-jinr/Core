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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    public enum MeasurementsType { sli, lli1, lli2, bckg };

    public class Measurement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int        Id                   { get; set; }
        [Required]
        public int        IrradiationId        { get; set; }
        public int        RegId                { get; set; }
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
        public int?        AcqMode             { get; set; }
        public int?      DiskPosition          { get; set; }
        public DateTime?  DateTimeStart        { get; set; }
        public int?      Duration              { get; set; }
        public DateTime?  DateTimeFinish       { get; set; }
        public float?     Height               { get; set; }
        public float?     DeadTime             { get; set; }
        public string     FileSpectra          { get; set; }
        public string     Detector             { get; set; }
        public int?       Assistant            { get; set; }
        public string     Note                 { get; set; }

        [NotMapped]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";

        [NotMapped]
        public string SampleKey => $"{SetIndex}-{SampleNumber}";
        public override string ToString() => $"{SetKey}-{SampleNumber}";

        [NotMapped]
        public static readonly IReadOnlyDictionary<MeasurementsType, string> TypeToString = new Dictionary<MeasurementsType, string> { { MeasurementsType.sli, "SLI" }, { MeasurementsType.lli1,"LLI-1" }, { MeasurementsType.lli2, "LLI-2" }, { MeasurementsType.bckg, "BCKG" } };

        public Measurement()
        { }
        public Measurement(Irradiation ir)
        {
            Type          = ir.Type;
            Year          = ir.Year;
            SetIndex      = ir.SetIndex;
            SetNumber     = ir.SetNumber;
            CountryCode   = ir.CountryCode;
            ClientNumber  = ir.ClientNumber;
            SampleNumber  = ir.SetNumber;
            IrradiationId = ir.Id;
        }
    

    } // public class Measurement

}     // namespace Regata.Core.DataBase.Models
