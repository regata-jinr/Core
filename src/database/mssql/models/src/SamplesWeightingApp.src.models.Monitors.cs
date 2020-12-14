/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2020, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace SamplesWeighting
{
    [Table("table_Monitor")]
    public class Monitor
    {
        public string Monitor_Set_Name     { get; set; }
        public string Monitor_Set_Number   { get; set; }
        public string Monitor_Number       { get; set; }
        public float? Monitor_SLI_Weight { get; set; }
        public float? Monitor_LLI_Weight { get; set; }
    }

    [Table("table_Monitor_Set")]
    public class MonitorsSet
    {
        public string Monitor_Set_Name   { get; set; }
        public string Monitor_Set_Number { get; set; }
    }
}

