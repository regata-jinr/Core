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

namespace Regata.Core.UI.WinForms.Forms.Measurements

{
    public class MeasurementRegisterSettings : ASettings
    {
        public int DefaultPopUpMessageTimeoutSeconds { get; set; } = 5;

        public RDataGridViewSettings MainTableSettings { get; set; } = new RDataGridViewSettings() 
        { 
            HidedColumns = new List<string>() { "Id", "IrradiationId", "RegId", "Assistant", "AcqMode", "Type", "SetKey", "SampleKey"},
            WritableColumns = new List<string>() { "Note" }
        };

    } // class MeasurementRegisterSettings : ASettings
}     // namespace Regata.Desktop.WinForms.Measurements
