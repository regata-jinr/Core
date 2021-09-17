/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("table_Monitor_Set")]
    public class MonitorSet
    {
        [Column("Monitor_Set_Name")]
        public string SetName { get; set; }
        [Column("Monitor_Set_Number")]
        public string SetNumber { get; set; }
        [Column("Monitor_Set_Type")]
        public string SetType { get; set; }
        [Column("Monitor_Set_Weight")]
        public Single SetWeight { get; set; }
        [Column("Monitor_Set_Purchasing_Date")]
        public DateTime SetPurchasingDate { get; set; }

        public override string ToString()
        {
            return $"m-m-m-{SetName}-{SetNumber}";
        }
    }
}
