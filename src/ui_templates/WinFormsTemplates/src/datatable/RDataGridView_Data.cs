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

namespace Regata.Core.UI.WinForms
{

    public partial class RDataGridView<Model> : DataGridView
    {
        public void AddItem(Model m)
        {
            if (m == null)
                return;
            _data.Add(m);
        }

        // TODO: Does it make sence to do it async? 
        //       Adding items to inmemory container sohuld be very fast.
        public void AddRange(IEnumerable<Model> ms)
        {
            if (ms == null)
                return;
            foreach (var m in ms)
                _data.Add(m);
        }

        public void FillCellsByValue<T>(DataGridViewSelectedCellCollection cells, T value)
        {
            foreach (DataGridViewCell c in cells)
            {
                c.Value = value;
            }

        }


    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
