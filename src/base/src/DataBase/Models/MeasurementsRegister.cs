/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;

namespace Regata.Core.DataBase.Models
{
    public class MeasurementsRegister
    {
        public int       Id              { get; set; }
        public DateTime? IrradiationDate { get; set; }
        public string    Name            { get; set; }
        public int?      LoadNumber      { get; set; }
        public int       Type            { get; set; }
        public DateTime? DateTimeStart   { get; set; }
        public DateTime? DateTimeFinish  { get; set; }
        public int       SamplesCnt      { get; set; }
        public string    Detectors       { get; set; }
        public int?      Assistant       { get; set; }
        public string    Note            { get; set; }

    }
}

