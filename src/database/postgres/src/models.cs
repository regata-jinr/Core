/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/


using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Postgres.Models
{
    [Table("logs")]
    public class Log
    {
        public int      id            { get; set; }
        public DateTime date_time     { get; set; }
        public string   level         { get; set; }
        public string   assistant     { get; set; }
        public string   frominstance { get; set; }
        public string   message       { get; set; }
    }
}
