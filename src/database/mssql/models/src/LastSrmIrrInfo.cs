using System;

namespace NewForms.Models
{
    public class LastSrmIrrInfo
    {
        //public string SRM_Set_Key               { get; set; }
        public string SRM_Number { get; set; }
        //public string SRM_Set_Number               { get; set; }
        //public float? SRM_SLI_Weight               { get; set; }
        //public float? SRM_LLI_Weight               { get; set; }
        public DateTime? LastDateOfIrradiation     { get; set; }
        public int NumberOfIrradiations { get; set; }

        //public override string ToString() => $"s-s-s-{SRM_Set_Key}-{SRM_Number}";
    }
}
