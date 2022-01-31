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

using Regata.Core.Settings;
using Regata.Core.DataBase.Models;
using RCM=Regata.Core.Messages;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms
{ 
    public static class Labels
    {
        private static string _formName = "";
        private static readonly IReadOnlyList<UILabel> _labels;

       static Labels()
       {
            try
            {
                string fileName = "labels.json";

                var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic) };
                string jsonString = File.ReadAllText(fileName);
                _labels = JsonSerializer.Deserialize<List<UILabel>>(jsonString, options);

            }
            catch (FileNotFoundException)
            {
                Report.Notify(new RCM.Message(Codes.ERR_LBL_FNF) { DetailedText = "File labels.json was not found in app directory." });
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_LBL_UNREG) { DetailedText = ex.Message });
            }


        }

        public static void SetControlsLabels(Control control)
        {
            _formName = control.TopLevelControl.Name;
             Utilities.ApplyActionToControl(control, SetTextField);
            //foreach (var cont in control.Controls)
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
                    var headerTmp = GetLabel(dgvc.Name, GlobalSettings.CurrentLanguage);
                    dgvc.HeaderText = !string.IsNullOrEmpty(headerTmp) ? headerTmp : dgvc.Name;
                    break;
                default:
                    
                    var getNameMethod = obj.GetType().GetProperty("Name").GetGetMethod();
                    var setTextMethod = obj.GetType().GetProperty("Text").GetSetMethod();

                    if (getNameMethod == null || setTextMethod == null) return;

                    var propertyName = getNameMethod.Invoke(obj, null).ToString();
                    var NameFromLabels = Labels.GetLabel(propertyName, GlobalSettings.CurrentLanguage);

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
                if (_labels != null && _labels.Where(l => l.FormName == _formName && l.ComponentName == compt).Any())
                {
                    if (cLang == Language.Russian)
                        return _labels.Where(f => f.FormName == _formName && f.ComponentName == compt).Select(f => f.RusText).First();
                    else
                        return _labels.Where(f => f.FormName == _formName && f.ComponentName == compt).Select(f => f.EngText).First();
                }
                else
                {
                    //Report.Notify(new RCM.Message(Codes.WARN_LBL_NOT_EXIST));
                    return compt;
                }

                  
            }
            catch (Exception  ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_SET_GET_LBL_UNREG) { DetailedText = ex.ToString() });
                return string.Empty;

            }
        }

    } // public static class Labels
} // namespace Regata.Core.Settings
