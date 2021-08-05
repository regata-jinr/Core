/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


namespace Regata.Core.DataBase.Models
{
    public interface ISrmAndMonitor
    {
        public string SetName { get; set; }
        public string SetNumber { get; set; }
        public string Number { get; set; }
        public float? SLIWeight { get; set; }
        public float? LLIWeight { get; set; }
    }
}
