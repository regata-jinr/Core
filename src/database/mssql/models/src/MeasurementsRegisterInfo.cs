/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


using System;
using System.ComponentModel.DataAnnotations;

namespace Regata.Core.Models.MSSQL
{
    public class MeasurementsRegisterInfo
    {
        [Key]
        public int       Id             { get; set; }
        [Required]
        public string    Name           { get; set; }
        public int       LoadNumber     { get; set; }
        [Required]
        public string    Type           { get; set; }
        public DateTime? DateTimeStart  { get; set; }
        public DateTime? DateTimeFinish { get; set; }
        public int       SamplesCnt     { get; set; }
        public string    Detectors      { get; set; }
        public string    Assistant      { get; set; }
        public string    Note           { get; set; }

    }
}

