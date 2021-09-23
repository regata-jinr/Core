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


namespace Regata.Core.DataBase.Models
{

    public interface IWeightedSample : ISample
    {
        float? SLIWeight { get; set; }
        float? LLIWeight { get; set; }
    }

    public interface IReWeightedSample : ISample
    {
        float? InitWght { get; set; }
        float? EmptyContWght { get; set; }
        float? ContWithSampleWght { get; set; }
        float? ARepackWght { get; set; }
    }
}
