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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: here I can add also the size of each columns

namespace Regata.Core.UI.WinForms.Controls.Settings
{


    public class RDataGridViewSettings
    {
        public List<string> HidedColumns = new List<string>();
        public List<string> ReadOnlyColumns = new List<string>();
        public DataGridViewAutoSizeColumnsMode ColumnSize = DataGridViewAutoSizeColumnsMode.Fill;
        public ColorizeMode ColorMode;
        
        
    }
}
