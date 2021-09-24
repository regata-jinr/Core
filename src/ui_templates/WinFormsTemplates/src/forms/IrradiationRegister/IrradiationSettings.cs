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

using Regata.Core.Settings;
using Regata.Core.UI.WinForms.Controls.Settings;
using System.Collections.Generic;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public class IrradiationSettings : ASettings
    {
        public int DefaultSLITime { get; set; } = 180;
        public int DefaultLLITime { get; set; } = 259200;
        public int DefaultPopUpMessageTimeoutSeconds { get; set; } = 5;

        public RDataGridViewSettings MainTableSettings { get; set; } = new RDataGridViewSettings()
        {
            HidedColumns = new List<string>() { "Id", "Type", "Rehandler", "Assistant", "LoadNumber", "SetKey", "SampleKey" },
            WritableColumns = new List<string>() { "Note" }
        };


    } // class MeasurementsSettings : ASettings
}     // namespace Regata.Desktop.WinForms.Measurements
