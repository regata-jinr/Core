//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace NewForms.Models
//{
//    [Table("table_SRM")]
//    public class StandardInfo
//    {
//        public string    SRM_Set_Name                     { get; set; }
//        public string    SRM_Set_Number                   { get; set; }
//        public string    SRM_Number                       { get; set; }
//        public float?    SRM_SLI_Weight                   { get; set; }
//        public float?    SRM_LLI_Weight                   { get; set; }
//        [NotMapped]
//        public DateTime? LastDateOfIrradiation            
//        {
//            get
//            {
//                List<DateTime> dateTimes = null;
//                using (var ic = new InfoContext())
//                {
//                    dateTimes = ic.Irradiations.Where(ir => ir.CountryCode == "s" && ir.ToString() == ToString() && ir.DateTimeStart.HasValue).Select(ir => ir.DateTimeStart.Value.Date).Distinct().ToList();
//                }
//                if (dateTimes == null) return null;
//                if (dateTimes.Any())
//                    return dateTimes.Max();
//                else
//                    return null;
//            } }
//        [NotMapped]
//        public int      NumberOfIrradiations             
//        {
//            get 
//            {
//                int num = 0;

//                using (var ic = new InfoContext())
//                {
//                    num = ic.Irradiations.Where(ir => ir.CountryCode == "s" && ir.ToString() == ToString() && ir.DateTimeStart.HasValue).Select(ir => ir.DateTimeStart.Value).Count();
//                }

//                return num;
//            }
//        }

//        public override string ToString() => $"s-s-s-{SRM_Set_Name}-{SRM_Set_Number}-{SRM_Number}";
//    }

//}
