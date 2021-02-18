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

using System.Linq;
using System;
using Regata.Core.Settings;
using System.Windows.Forms;
using Regata.Core.DB.MSSQL.Context;

namespace Regata.Core.UI.WinForms
{ 
    // TODO: how to call ChangeControlLabels for all opening forms? event?
    public static class Labels
    {
        private static Language _lang;

        public static Language CurrentLanguage 
        {
            get { return _lang; }
            set
            {
                _lang = value;
                LanguageChanged?.Invoke();
            }
        }

        public static event Action LanguageChanged;

        public static void SetControlsLabels(Control.ControlCollection controls)
        {
            foreach (var cont in controls)
                Utilities.ApplyActionToControl(cont, SetTextField);
        }

        /// <summary>
        /// Method set text field of object in case it exists. The value of text it take from db. <see cref="Labels"/>
        /// </summary>
        /// <param name="obj"></param>
        private static void SetTextField(object obj)
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

        public static string GetLabel(string compt, Language cLang)
        {
            try
            {
                using (var r = new RegataContext())
                {
                    if (!r.UILabels.Where(l => l.ComponentName == compt).Any()) return compt;
                    if (cLang == Language.Russian)
                        return r.UILabels.Where(f => f.ComponentName == compt).Select(f => f.RusText).First();
                    else
                        return r.UILabels.Where(f => f.ComponentName == compt).Select(f => f.EngText).First();
                }
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_SET_GET_LBL_UNREG));
                return string.Empty;

            }

        }

    } // public static class Labels
} // namespace Regata.Core.Settings
