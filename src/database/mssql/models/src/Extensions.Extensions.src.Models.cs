using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Extensions
{
    [Table("SharingFilesErrors")]
    public class SharingFilesErrors
    {
        public string fileS        { get; set; }
        public string fileSPath    { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString() => Path.Combine(fileSPath,$"{fileS}.cnf");
        
    }

    [Table("sharedspectra")]
    public class SharedSpectra
    {
        public string fileS     { get; set; }
        public string token     { get; set; }
    }
}
