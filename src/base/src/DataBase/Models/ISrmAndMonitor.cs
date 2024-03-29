﻿/***************************************************************************
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
        string SetName { get; set; }
        string SetNumber { get; set; }
        string Number { get; set; }
        float? SLIWeight { get; set; }
        float? LLIWeight { get; set; }
    }
}
