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

namespace Regata.Core.Settings
{

    public static class Settings<AppSettings> 
    {
        public static AppSettings CurrentSettings;

        public static string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Regata",AssemblyName,"settings.json");
            }
        }

        private  static string _assmName;

        public  static string AssemblyName { 
            get { return _assmName; }
            set
            {
                _assmName = value;
                Load();
            }
        }

        public static void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    CurrentSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(FilePath), options);
                }
                else
                {
                    ResetToDefaults();
                }
            }
            catch (JsonException)
            {
                ResetToDefaults();
            }
        }

        public static void ResetToDefaults()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            CurrentSettings = Activator.CreateInstance<AppSettings>();
            Save();
        }

        public static void Save()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;
            File.WriteAllText(FilePath, JsonSerializer.Serialize(CurrentSettings, options));
        }

    } // public static class Settings<AppSettings> 
}     // namespace Regata.Core.Settings
