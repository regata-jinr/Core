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

namespace Regata.Core.Settings
{
    public enum Language { Russian, English };

    public interface ISettings
    {
        Language CurrentLanguage { get; set; }
        Status   Verbosity       { get; set; }
    }
}     // namespace Regata.Core.Settings
