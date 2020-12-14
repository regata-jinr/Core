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

namespace SWeight.Models
{
    [Table("table_Sample")]
    public class Sample
    {
        [Column("F_Country_Code")]
        public string Country_Code { get; set; }
        [Column("F_Client_Id")]
        public string Client_Id { get; set; }
        [Column("F_Year")]
        public string Year { get; set; }
        [Column("F_Sample_Set_Id")]
        public string Sample_Set_Id { get; set; }
        [Column("F_Sample_Set_Index")]
        public string Sample_Set_Index { get; set; }
        public string A_Sample_ID { get; set; }
        public string A_Client_Sample_ID { get; set; }
        public float? P_Weighting_SLI { get; set; }
        public float? P_Weighting_LLI { get; set; }
    }

    [Table("table_Sample_Set")]
    public class SamplesSet
    {
        public string Country_Code { get; set; }
        public string Client_Id { get; set; }
        public string Year { get; set; }
        public string Sample_Set_Id { get; set; }
        public string Sample_Set_Index { get; set; }
    }
}



