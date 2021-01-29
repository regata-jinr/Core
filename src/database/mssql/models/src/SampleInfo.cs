using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForms.Models
{
    [Table("table_Sample")]
    public class SampleInfo
    {
        public string F_Country_Code { get; set; }
        public string F_Client_Id { get; set; }
        public string F_Year { get; set; }
        public string F_Sample_Set_Id { get; set; }
        public string F_Sample_Set_Index { get; set; }
        public string A_Sample_ID { get; set; }
        public string A_Client_Sample_ID {get; set; }
        public string A_Sample_Type { get; set; }
        public Single? P_Weighting_LLI { get; set; }
        public Single? P_Weighting_SLI { get; set; }

        [NotMapped]
        public string SetKey => $"{F_Country_Code}-{F_Client_Id}-{F_Year}-{F_Sample_Set_Id}-{F_Sample_Set_Index}";
        [NotMapped]
        public string SampleKey => $"{SetKey}-{A_Sample_ID}";

        public override string ToString()
        {
            return $"{A_Sample_ID}-{A_Client_Sample_ID}";
        }


    }
}
