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
using System.Windows.Forms;
using Regata.Core.Settings;

namespace Regata.Core.WinForms
{
    /// <summary>
    /// Utilities contain additional functions,e.g. for changing language of form controls based on <inheritdoc Labels/> class.
    /// </summary>
    public static class Utilities
    {

        public static Languages CurrentLanguage { get; private set; }

        public static void SetControlsLabels(Control.ControlCollection controls, Languages clang)
        {
            CurrentLanguage = clang;
            foreach (var cont in controls)
                ApplyActionToComponent(cont, SetTextLabel);
        }

        public static void ChangeControlsLabels(Control.ControlCollection controls)
        {
            switch (CurrentLanguage)
            {
                case Languages.English:
                    CurrentLanguage = Languages.Russian;
                    break;
                case Languages.Russian:
                    CurrentLanguage = Languages.English;
                    break;
            }
            SetControlsLabels(controls, CurrentLanguage);
        }

        // TODO: check form label and footer status label
        private static void ApplyActionToComponent(object component, Action<object> act)
        {
            switch (component)
            {
                case DataGridView dgv:
                    act(dgv);
                    foreach (DataGridViewColumn col in dgv.Columns)
                        act(col);
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                    {
                        item.Text = Labels.GetLabel(item.Name, CurrentLanguage);
                        ApplyActionToComponent(item, act);

                    }
                    break;

                case ToolStripMenuItem tsi:
                    tsi.Text = Labels.GetLabel(tsi.Name, CurrentLanguage);
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        ApplyActionToComponent(innerTsi, act);
                    break;

                case Control nestedControl:
                    if (nestedControl.Controls.Count > 0)
                    {
                        foreach (Control nc in nestedControl.Controls)
                            ApplyActionToComponent(nc, act);
                    }
                    else
                        act(nestedControl);
                    break;

                case null:
                    throw new ArgumentNullException("Have trying to set language for null control");

                default:
                    act(component);
                    break;
            }
        }


        private static void SetTextLabel(object obj)
        {
            switch (obj)
            {
                case DataGridViewColumn dgvc:
                    var headerTmp = Labels.GetLabel(dgvc.Name, CurrentLanguage);
                    if (!string.IsNullOrEmpty(headerTmp))
                        dgvc.HeaderText = Labels.GetLabel(dgvc.Name, CurrentLanguage);
                    break;
                default:

                    var getNameMethod = obj.GetType().GetProperty("Name").GetGetMethod();
                    var setTextMethod = obj.GetType().GetProperty("Text").GetSetMethod();

                    if (getNameMethod == null || setTextMethod == null) return;

                    var propertyName = getNameMethod.Invoke(obj, null).ToString();
                    var NameFromLabels = Labels.GetLabel(propertyName, CurrentLanguage);

                    if (!string.IsNullOrEmpty(NameFromLabels))
                        setTextMethod.Invoke(obj, new object[] { NameFromLabels });
                    else
                        setTextMethod.Invoke(obj, new object[] { propertyName });

                    break;
            }
        }



    } //public static class Utilities
}     // namespace Regata.Core.WinForms

