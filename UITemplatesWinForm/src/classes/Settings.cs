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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;

namespace Regata.UITemplates
{
    public enum Languages { Russian, English };

    public struct Parameters
    {
        public Languages CurrentLanguage { get; set; }
        public Dictionary<string, List<string>>  FormNonDisplayedColumns { get; set; }
    }

    public static class Settings
    {
        public static string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Regata\\{AssemblyName}\\settings.json";
            }
        }

        private  static string _assmName;
        public  static string AssemblyName { 
            get { return _assmName; }
            set
            {
                _assmName = value;
                ReadSettings();

            }
        }

        private static Languages _currentLanguage;
        public static Dictionary<string, List<string>> FormNonDisplayedColumns { get; private set; }

        private static void InitFormNonDisplayedColumnsDict(string formName)
        {
            if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");

            if (FormNonDisplayedColumns.ContainsKey(formName)) return;

            FormNonDisplayedColumns.Add(formName, new List<string>());

        }

        public static void ShowColumn(string formName, string columnName)
        {
            InitFormNonDisplayedColumnsDict(formName);
            if (FormNonDisplayedColumns[formName].Contains(columnName))
                FormNonDisplayedColumns[formName].Remove(columnName);
            SaveSettings();
        }

        public static void HideColumn(string formName, string columnName)
        {
            InitFormNonDisplayedColumnsDict(formName);
            if (!FormNonDisplayedColumns[formName].Contains(columnName))
                FormNonDisplayedColumns[formName].Add(columnName);
            SaveSettings();
        }

        public static Languages CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                Labels.CurrentLanguage = _currentLanguage;
                LanguageChanged?.Invoke();
                SaveSettings();
            }
        }

        public static event Action LanguageChanged;

        private static void ReadSettings()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    var parameters = JsonSerializer.Deserialize<Parameters>(File.ReadAllText(FilePath), options);
                    FormNonDisplayedColumns = parameters.FormNonDisplayedColumns;
                    CurrentLanguage = parameters.CurrentLanguage;

                    if (FormNonDisplayedColumns == null)
                        FormNonDisplayedColumns = new Dictionary<string, List<string>>();
                }
                else
                    ResetFileSettings();
            }
            catch (JsonException)
            {
                ResetFileSettings();
            }
        }

        private static void ResetFileSettings()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            using (var f = File.CreateText(FilePath))
            { }

            CurrentLanguage = Languages.English;
            FormNonDisplayedColumns = new Dictionary<string, List<string>>();
            SaveSettings();
        }

        public static void SaveSettings()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;
            File.WriteAllText(FilePath, JsonSerializer.Serialize(new Parameters { CurrentLanguage = CurrentLanguage, FormNonDisplayedColumns = FormNonDisplayedColumns }, options));
        }

    } // public class Settings
} // namespace Regata.UITemplates
