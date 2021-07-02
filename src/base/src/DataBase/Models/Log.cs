/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("logs")]
    public class Log
    {
        public long      Id            { get; set; }
        public DateTime  DateTime      { get; set; }
        public string    Level         { get; set; }
        public string    Assistant     { get; set; }
        public string    Frominstance  { get; set; }
        public int       Code          { get; set; }
        public string    Message       { get; set; }
    }
}
