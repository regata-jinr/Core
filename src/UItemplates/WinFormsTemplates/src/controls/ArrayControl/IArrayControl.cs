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

using System;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{
    public interface ICheckedArrayControl<T>
    {
        event Action<CheckedArrayControl<T>> SelectionChanged;

    }

    public interface ISingleCheckedArrayControl<T> : ICheckedArrayControl<T>
    {
        void ClearSelection();
        T SelectedItem { get; }
    }

    public interface IMultiCheckedArrayControl<T> : ICheckedArrayControl<T>
    {
        void ClearSelection();
        T[] SelectedItems { get; }
    }

    public interface IChecked
    {
        bool   Checked { get; set; }
        string Name { get; set; }
        DockStyle Dock { get; set; }
        string Text  { get; set; }
        event EventHandler CheckedChanged;
    }
}
