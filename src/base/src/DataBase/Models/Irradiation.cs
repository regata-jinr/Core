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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Regata.Core.DataBase.Models
{
    public enum IrradiationType { sli, lli };

    public class Irradiation : ISample, IId, ICloneable
    {
        [Key]
        public int       Id             { get; set; }
        public string    CountryCode    { get; set; } // "RU"
        public string    ClientNumber   { get; set; } // 1
        public string    Year           { get; set; } // 18
        public string    SetNumber      { get; set; } // 55
        public string    SetIndex       { get; set; } // j
        public string    SampleNumber   { get; set; } // 1
        public int       Type           { get; set; }
        public DateTime? DateTimeStart  { get; set; }
        public int?      Duration       { get; set; }
        public DateTime? DateTimeFinish { get; set; }
        public short?    Container      { get; set; }
        public short?    Position       { get; set; }
        public short?    Channel        { get; set; }
        public int?      LoadNumber     { get; set; }
        public int?      Rehandler      { get; set; }
        public int?      Assistant      { get; set; }
        public string    Note           { get; set; }

        [NotMapped]
        public string SetKey
        {
            get
            {
                if (Year == "s" || Year == "m")
                    return $"{Year}-{SetNumber}-{SetIndex}-{SampleNumber}";
                return $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";
            }
        }

        [NotMapped]
        public string SampleKey
        {
            get
            {
                if (Year == "s" || Year == "m")
                    return SetNumber;
                return $"{SetIndex}-{SampleNumber}";
            }
        }
        public override string ToString() => $"{SetKey}-{SampleNumber}";

        public static readonly IReadOnlyDictionary<IrradiationType, string> TypeToString = new Dictionary<IrradiationType, string> { { IrradiationType.sli, "SLI" }, { IrradiationType.lli, "LLI" }};


        public void Swap(ref Irradiation ir2)
        {
            var tmpIr1 = this.Clone() as Irradiation;

            this.CountryCode         = ir2.CountryCode    ;
            this.ClientNumber        = ir2.ClientNumber   ;
            this.Year                = ir2.Year           ;
            this.SetNumber           = ir2.SetNumber      ;
            this.SetIndex            = ir2.SetIndex       ;
            this.SampleNumber        = ir2.SampleNumber   ;
            this.Type                = ir2.Type           ;
            this.Container           = ir2.Container      ;
            this.Position            = ir2.Position       ;
            this.Channel             = ir2.Channel        ;
            this.DateTimeStart       = ir2.DateTimeStart  ;
            this.Duration            = ir2.Duration       ;
            this.DateTimeFinish      = ir2.DateTimeFinish ;
            this.LoadNumber          = ir2.LoadNumber     ;
            this.Rehandler           = ir2.Rehandler      ;
            this.Assistant           = ir2.Assistant      ;
            this.Note = ir2.Note;

            ir2.CountryCode    = tmpIr1.CountryCode;
            ir2.ClientNumber   = tmpIr1.ClientNumber;
            ir2.Year           = tmpIr1.Year;
            ir2.SetNumber      = tmpIr1.SetNumber;
            ir2.SetIndex       = tmpIr1.SetIndex;
            ir2.SampleNumber   = tmpIr1.SampleNumber;
            ir2.Type           = tmpIr1.Type;
            ir2.DateTimeStart  = tmpIr1.DateTimeStart;
            ir2.Duration       = tmpIr1.Duration;
            ir2.DateTimeFinish = tmpIr1.DateTimeFinish;
            ir2.Container      = tmpIr1.Container;
            ir2.Position       = tmpIr1.Position;
            ir2.Channel        = tmpIr1.Channel;
            ir2.LoadNumber     = tmpIr1.LoadNumber;
            ir2.Rehandler      = tmpIr1.Rehandler;
            ir2.Assistant      = tmpIr1.Assistant;
            ir2.Note           = tmpIr1.Note;



        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //public Irradiation()
        //{ }

        //    public Irradiation(Sample smp, IrradiationType irType, int? loadNumber)
        //    {
        //        CountryCode  = smp.CountryCode;
        //        ClientNumber = smp.ClientNumber;
        //        Year         = smp.Year         ;
        //        SetNumber    = smp.SetNumber    ;
        //        SetIndex     = smp.SetIndex     ;
        //        SampleNumber = smp.SampleNumber ;
        //        Type         = (int)irType;
        //        LoadNumber   = loadNumber;
        //}

    } // public class Irradiation
}     // namespace Regata.Core.DataBase.Models
