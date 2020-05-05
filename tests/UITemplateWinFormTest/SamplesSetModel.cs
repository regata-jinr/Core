using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UITemplateWinFormTest
{
    [Table("table_Sample_Set")]
    public class SamplesSetModel
    {
        public string Country_Code { get; set; }
        public string Client_ID { get; set; }
        public string Year { get; set; }
        public string Sample_Set_ID { get; set; }
        public string Sample_Set_Index { get; set; }
        public string Notes_2 { get; set; }

    }
}
