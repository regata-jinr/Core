using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForms.Models
{
    [Table("table_Monitor")]
    public class MonitorInfo
    {
        public string Monitor_Set_Name { get; set; }
        public string Monitor_Set_Number { get; set; }
        public Single Monitor_Set_Weight { get; set; }
        public string Monitor_Number { get; set; }
        public Single? Monitor_SLI_Weight { get; set; }
        public Single? Monitor_LLI_Weight { get; set; }

        public override string ToString()
        {
            return $"m-m-m-{Monitor_Set_Name}-{Monitor_Set_Number}-{Monitor_Number}";
        }


    }
}
