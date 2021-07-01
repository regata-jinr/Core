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
    [Table("table_Sample_Set")]
    public class SamplesSet
    {
        public string         Country_Code            { get; set; } // "RU"
        public string         Client_Id               { get; set; } // 1
        public string         Year                    { get; set; } // 18
        public string         Sample_Set_Id           { get; set; } // 55
        public string         Sample_Set_Index        { get; set; } // j
        public DateTime       Sample_Set_Receipt_Date { get; set; } // 1
        public DateTime       Sample_Set_Report_Date  { get; set; } // SLI
        public string         Received_By             { get; set; }
        public string         Notes_1                 { get; set; }
        public string         Notes_2                 { get; set; }
        public string         Notes_3                 { get; set; }
        public string         Note                    { get; set; }
        public string         Loggs                   { get; set; }
        public bool           PrepCompl               { get; set; }
        public bool           SLICompl                { get; set; }
        public bool           LLICompl                { get; set; }
        public bool           ResCompl                { get; set; }

        public override string ToString() => $"{Country_Code}-{Client_Id}-{Year}-{Sample_Set_Id}-{Sample_Set_Index}";

    }
}
