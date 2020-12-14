using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Runtime.CompilerServices;

namespace Extensions.Models
{
    [Table("table_Sample")]
    public class Sample : ICloneable
    {
        public override string ToString() => $"{F_Country_Code}-{F_Client_Id}-{F_Year}-{F_Sample_Set_Id}-{F_Sample_Set_Index}";
        public Sample() { }

        public Sample(string setKey)
        {
            var sk = setKey.Split('-');

            F_Country_Code = sk[0];
            F_Client_Id = sk[1];
            F_Year = sk[2];
            F_Sample_Set_Id = sk[3];
            F_Sample_Set_Index = sk[4];

        }

        public object Clone()
        {
            var newsmp = this.MemberwiseClone() as Sample;
            newsmp.P_Weighting_LLI = null;
            newsmp.P_Weighting_SLI = null;

            newsmp.A_Drying_Plan= false;
            newsmp.A_Freeze_Drying_Plan = false;
            newsmp.A_Homogenizing_Plan = false;
            newsmp.A_Pelletization_Plan = false;
            newsmp.I_SLI_Date_Start = null;

            return newsmp;
        }

        [Ignore]
        [Key]
        public string F_Country_Code        { get; set; }
        [Ignore]
        [Key]
        public string F_Client_Id           { get; set; }
        [Ignore]
        [Key]
        public string F_Year                { get; set; }
        [Ignore]
        [Key]
        public string F_Sample_Set_Id       { get; set; }
        [Ignore]
        [Key]
        public string F_Sample_Set_Index    { get; set; }
        [Ignore]
        [Key]
        public string A_Sample_ID           { get; set; }

        [Index(0)]
        public string    A_Client_Sample_ID      { get; set; }
        [Index(1)]
        public string    A_Sample_Type           { get; set; }
        [Index(2)]
        public string    A_Latitude              { get; set; }
        [Index(3)]
        public string    A_Longitude             { get; set; }
        [Index(4)]
        public string    A_Collection_Place      { get; set; }
        [Index(5)]
        public string    A_Notes                 { get; set; }
        [Index(6)]
        public string    A_Determined_Elements   { get; set; }

        [Ignore]
        public DateTime? I_SLI_Date_Start        { get; set; }
        [Ignore]
        public bool      A_Drying_Plan           { get; set; }
        [Ignore]
        public bool      A_Freeze_Drying_Plan    { get; set; }
        [Ignore]
        public bool      A_Homogenizing_Plan     { get; set; }
        [Ignore]
        public bool      A_Pelletization_Plan    { get; set; }
        [Ignore]
        public float?    P_Weighting_SLI         { get; set; }
        [Ignore]
        public float?    P_Weighting_LLI         { get; set; }

    }
}

