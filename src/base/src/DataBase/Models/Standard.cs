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

using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("table_SRM")]
    public class Standard : ISrmAndMonitor
    {
        [Column("SRM_Set_Name")]
        public string SetName   { get; set; }

        [Column("SRM_Set_Number")]
        public string SetNumber { get; set; }

        [Column("SRM_Number")]
        public string Number     { get; set; }

        [Column("SRM_SLI_Weight")]
        public float? SLIWeight { get; set; }

        [Column("SRM_LLI_Weight")]
        public float? LLIWeight { get; set; }

        public override string ToString() => $"s-s-s-{SetName}-{SetNumber}-{Number}";
    }

}
