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
        [Column("Country_Code")]
        public string                         CountryCode             { get; set; } // "RU"
        [Column("Client_Id")]
        public string                         ClientNumber            { get; set; } // 1
        public string                         Year                    { get; set; } // 18
        [Column("Sample_Set_Id")]
        public string                         SetNumber               { get; set; } // 55
        [Column("Sample_Set_Index")]
        public string                         SetIndex                { get; set; } // j
        [Column("Sample_Set_Receipt_Date")]
        public DateTime                       SetReceiptDate { get; set; } // 1
        public DateTime                       Sample_Set_Report_Date  { get; set; } // SLI
        public string                         Received_By             { get; set; }
        public string                         Notes_1                 { get; set; }
        public string                         Notes_2                 { get; set; }
        public string                         Notes_3                 { get; set; }
        public string                         Note                    { get; set; }
        public string                         Loggs                   { get; set; }
        public bool                           PrepCompl               { get; set; }
        public bool                           SLICompl                { get; set; }
        public bool                           LLICompl                { get; set; }
        public bool                           ResCompl                { get; set; }

        public override string ToString() => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";

    }
}
