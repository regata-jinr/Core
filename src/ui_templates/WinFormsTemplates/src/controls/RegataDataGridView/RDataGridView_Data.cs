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

using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{

    public partial class RDataGridView<Model> : DataGridView
    {
        public void FillCellsByValue<T>(DataGridViewSelectedCellCollection cells, T value)
        {
            foreach (DataGridViewCell c in cells)
            {
                if (c.ValueType != typeof(T))
                {
                    Report.Notify(new Message(Codes.WARN_UI_WF_RDGV_Wrong_Value_Type));
                    continue;
                }
                c.Value = value;
            }

        }



    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
