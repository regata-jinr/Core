using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForms.Models
{
    [Table("table_Monitor_Set")]
    public class MonitorSetInfo
    {
        public string Monitor_Set_Name { get; set; }
        public string Monitor_Set_Number { get; set; }
        public string Monitor_Set_Type { get; set; }
        public Single Monitor_Set_Weight { get; set; }
        public DateTime Monitor_Set_Purchasing_Date { get; set; }

        public override string ToString()
        {
            return $"m-m-m-{Monitor_Set_Name}-{Monitor_Set_Number}";
        }
    }
}
