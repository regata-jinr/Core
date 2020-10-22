using System;
using System.ComponentModel.DataAnnotations;

namespace WebMonitorBlazor.Data
{
  public class Log
  {
    [Key]
    public int id { get; set; }
    public DateTime date_time { get; set; }
    public string level { get; set; }
    public string userid { get; set; }
    public string callingsite { get; set; }
    public string iname { get; set; }
    public string message { get; set; }

  }
}