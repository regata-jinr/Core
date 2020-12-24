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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Regata.Core.UI.WinForms.Settings;

// TODO: add column filling by value
// TODO: add data validation
// TODO: add export to excel (via csv)
// TODO: add filtering
// TODO: add colorizing
// TODO: add sorting
// TODO: add tests
// TODO: add autoupdate based on github releases

namespace Regata.Core.UI.WinForms
{
    public enum ColorizeMode { None, Even, ByValue }

    public abstract partial class RDataGridView<Model> : DataGridView
    {

        public readonly BindingList<Model> Data;

        public static RDataGridViewSettings RDGV_Set = new RDataGridViewSettings();

        public RDataGridView() : base()
        {
            RowHeadersVisible = false;

            AutoSizeColumnsMode = RDGV_Set.ColumnSize;
            HideColumns();
            Data = new BindingList<Model>();

        }

        public void HideColumns()
        {
            foreach (DataGridViewColumn cl in Columns)
            {
                if (RDGV_Set.HidedColumns.Contains(cl.Name))
                    Columns[cl.Name].Visible = false;
                else
                    Columns[cl.Name].Visible = true;
            }
        }

        public void SetUpReadOnlyColumns()
        {
            foreach (DataGridViewColumn cl in Columns)
            {
                if (RDGV_Set.ReadOnlyColumns.Contains(cl.Name))
                    Columns[cl.Name].ReadOnly = true;
                else
                    Columns[cl.Name].ReadOnly = false;
            }
        }


    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
