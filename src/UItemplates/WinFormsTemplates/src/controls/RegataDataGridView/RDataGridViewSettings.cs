﻿/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Collections.Generic;
using System.Windows.Forms;

// TODO: here I can add also the size of each columns

namespace Regata.Core.UI.WinForms.Controls.Settings
{

    public class RDataGridViewSettings
    {
        public List<string> HidedColumns { get; set; } = new List<string>();
        public List<string> ReadOnlyColumns { get; set; } = new List<string>();
        public List<string> WritableColumns { get; set; } = new List<string>();
        public DataGridViewAutoSizeColumnsMode ColumnSize { get; set; } = DataGridViewAutoSizeColumnsMode.Fill;
        public ColorizeMode ColorMode { get; set; }
        
    }
}
