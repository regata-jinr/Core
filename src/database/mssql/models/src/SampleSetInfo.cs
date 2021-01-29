using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForms.Models
{
    [Table("table_Sample_Set")]
    public class SampleSetInfo
    {
        public string Country_Code { get; set; } // "RU"
        public string Client_Id { get; set; } // 1
        public string Year { get; set; } // 18
        public string Sample_Set_Id { get; set; } // 55
        public string Sample_Set_Index { get; set; } // j
        public DateTime Sample_Set_Receipt_Date { get; set; } // 1
        public DateTime Sample_Set_Report_Date { get; set; } // SLI
        public string Received_By { get; set; }
        public string Notes_1 { get; set; }
        public string Notes_2 { get; set; }
        public string Notes_3 { get; set; }
        public string Note { get; set; }
        public string Loggs { get; set; }
        public bool PrepCompl { get; set; }
        public bool SLICompl { get; set; }
        public bool LLICompl { get; set; }
        public bool ResCompl { get; set; }

        public override string ToString() => $"{Country_Code}-{Client_Id}-{Year}-{Sample_Set_Id}-{Sample_Set_Index}";

    }
}
