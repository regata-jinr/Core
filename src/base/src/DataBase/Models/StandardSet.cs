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
        public string   SRM_Set_Name              { get; set; }
        public string   SRM_Set_Number            { get; set; }
        public string   SRM_Set_Type              { get; set; }
        public float    SRM_Set_Weight            { get; set; }
        public DateTime SRM_Set_Purchasing_Date   { get; set; }

        public override string ToString()
        {
            return $"s-s-s-{SRM_Set_Name}-{SRM_Set_Number}";
        }

    }
}
