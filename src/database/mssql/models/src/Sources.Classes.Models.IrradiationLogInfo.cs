using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Measurements.UI.Models
{
    public class IrradiationLogInfo
    {
        public DateTime? DateTimeStart { get; set; }
        public int?      LoadNumber    { get; set; }
    }
}
