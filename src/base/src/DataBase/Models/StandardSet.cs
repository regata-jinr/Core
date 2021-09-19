/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2018-2021, REGATA Experiment at FLNP|JINR                  *
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
    [Table("table_SRM_Set")]
    public class StandardSet
    {
        [Column("SRM_Set_Name")]
        public string   SetName              { get; set; }
        [Column("SRM_Set_Number")]
        public string   SetNumber            { get; set; }
        [Column("SRM_Set_Type")]
        public string   SetType              { get; set; }
        [Column("SRM_Set_Weight")]
        public float    SetWeight            { get; set; }
        [Column("SRM_Set_Purchasing_Date")]
        public DateTime SetPurchasingDate   { get; set; }

        public override string ToString()
        {
            return $"s-s-s-{SetName}-{SetNumber}";
        }

    }
}
