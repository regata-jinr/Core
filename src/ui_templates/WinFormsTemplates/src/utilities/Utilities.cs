/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Windows.Forms;
using Regata.Core.UI.WinForms.Controls;
using RCM=Regata.Core.Messages;

namespace Regata.Core.UI.WinForms
{
    /// <summary>
    /// Utilities contain additional functions,e.g. for changing language of form controls based on <inheritdoc Labels/> class.
    /// </summary>
    public static class Utilities
    {
        // TODO: check form label and footer status label
        /// <summary>
        /// Allows to apply any action to winforms controls
        /// </summary>
        /// <param name="control">Any control from winforms, including nested controls</param>
        /// <param name="act">Action delegate with object as paramter</param>
        internal static void ApplyActionToControl(object control, Action<object> act)
        {
            switch (control)
            {
                case DataGridView dgv:
                    act(dgv);
                    foreach (DataGridViewColumn col in dgv.Columns)
                        act(col);
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                    {
                        act(item);
                        ApplyActionToControl(item, act);
                    }
                    break;

                case StatusStrip ss:
                    foreach (ToolStripItem item in ss.Items)
                    {
                        act(item);
                        ApplyActionToControl(item, act);
                    }
                    break;

                case ToolStripMenuItem tsi:
                    act(tsi);
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        ApplyActionToControl(innerTsi, act);
                    break;

                case TabControl tab:
                    foreach (TabPage page in tab.TabPages)
                        ApplyActionToControl(page, act);
                    break;

                case DGVTabPaneControl dgv_tab:
                    foreach (TabPage page in dgv_tab.Pages)
                        ApplyActionToControl(page, act);
                    
                    break;

                case Control nestedControl:
                    if (nestedControl.Controls.Count > 0)
                    {
                        foreach (Control nc in nestedControl.Controls)
                            ApplyActionToControl(nc, act);
                    }
                    act(nestedControl);
                    break;

                case null:
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_UTLT_NULL_CONTRL));
                    break;

                default:
                    act(control);
                    break;
            }
        }

    } //public static class Utilities
}     // namespace Regata.Core.WinForms
