using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForms.Models
{
    [Table("table_SRM_Set")]
    public class StandardSetInfo
    {
        public string SRM_Set_Name              { get; set; }
        public string SRM_Set_Number            { get; set; }
        public string SRM_Set_Type              { get; set; }
        public Single SRM_Set_Weight            { get; set; }
        public DateTime SRM_Set_Purchasing_Date { get; set; }

        public override string ToString()
        {
            return $"s-s-s-{SRM_Set_Name}-{SRM_Set_Number}";
        }

    }
}
