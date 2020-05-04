/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Windows.Forms;

namespace Regata.Utilities
{
    public static class LangSwitcher
    {
        public static void ChangeFormLanguage(Form form)
        {

            SetLanguageToControls(form.Controls);
        }

        private static void SetLanguageToControls(Control.ControlCollection controls)
        {
            foreach (var cont in controls)
                SetLanguageToObject(cont);
        }

        public static string GetValueOfSetting(string name)
        {
            return typeof(Labels).GetProperty(name)?.GetValue(null).ToString();
        }

        private static void SetLanguageToObject(object cont)
        {
            switch (cont)
            {
                case GroupBox grpb:
                    grpb.Text = GetValueOfSetting(grpb.Name);
                    SetLanguageToControls(grpb.Controls);
                    break;

                case TabControl tbcont:
                    foreach (TabPage page in tbcont.TabPages)
                    {
                        page.Text = GetValueOfSetting(page.Name);
                        SetLanguageToControls(page.Controls);
                    }
                    break;

                case DataGridView dgv:
                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = GetValueOfSetting(col.Name);
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                        SetLanguageToObject(item);
                    break;

                case ToolStripMenuItem tsi:
                    tsi.Text = GetValueOfSetting(tsi.Name);
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        SetLanguageToObject(innerTsi);
                    break;

                default:
                    var getName = cont.GetType().GetProperty("Name").GetGetMethod();
                    var setText = cont.GetType().GetProperty("Text").GetSetMethod();

                    setText.Invoke(cont, new object[] { GetValueOfSetting(getName.Invoke(cont, null).ToString()) });
                    break;

                case null:
                    throw new ArgumentNullException("Have trying to set language for null control");
            }
        }
    } // internal static class Utilities

} // namespace GSI.Utilities
