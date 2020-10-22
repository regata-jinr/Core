using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Extensions.Models
{
    public class ScRecord
    {
        [NotMapped]
        public short     RowNum           { get; set; }

        public string    SampleSet        { get; set; }
        public string    Sample_ID        { get; set; }
        public float?    Weight           { get; set; }
        public decimal?  Height           { get; set; }
        public string    Date_Start       { get; set; }
        public string    Time_Start       { get; set; }
        public string    Date_Finish      { get; set; }
        public string    Time_Finish      { get; set; }

        [Ignore]
        public short?      Container_Number { get; set; }
        [Ignore]
        public short?      Position_Number  { get; set; }
    }
}

