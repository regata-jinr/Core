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


namespace Regata.Core.UI.WinForms.Controls
{
    public interface ICheckedArrayControl
    {
        event System.Action SelectionChanged;

    }


    public interface ISingleCheckedArrayControl<T> : ICheckedArrayControl
    {
        void ClearSelection();
        T SelectedItem { get; }
    }

    public interface IMultiCheckedArrayControl<T> : ICheckedArrayControl
    {
        void ClearSelection();
        T[] SelectedItems { get; }
    }

    public interface IChecked
    {
        bool   Checked { get; set; }
        string Name { get; set; }
        System.Windows.Forms.DockStyle Dock { get; set; }
        string Text  { get; set; }
        event System.EventHandler CheckedChanged;
    }
}
