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


using System.ComponentModel.DataAnnotations.Schema;


namespace Regata.Core.DB.MSSQL.Models
{
    [Table("SharedSpectra")]
    public class SharedSpectrum
    {
        public string fileS { get; set; }
        public string token { get; set; }
    }

    public class SpectrumSLI
    {
        public string SampleType { get; set; }
        public string SampleSpectra { get; set; }
        public string token { get; set; }
    }

    public class SpectrumLLI
    {
        public int LoadNumber { get; set; }
        public string irtype { get; set; }
        public short Container { get; set; }
        public string SampleType { get; set; }
        public string SampleSpectra { get; set; }
        public string token { get; set; }
    }
}

