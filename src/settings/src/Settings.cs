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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Regata.Core.Settings
{

    public enum Languages { Russian, English };

    public static class Settings<AppSettings> 
    {
        public static AppSettings CurrentSettings;

        public static string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(AssemblyName))
                {
                    Report.Notify(Codes.ERR_SET_GET_FILE_SET_EMPT_ASMBL);
                    return string.Empty;
                }
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Regata",AssemblyName,"settings.json");
            }
        }

        private  static string _assmName;

        public  static string AssemblyName { 
            get { return _assmName; }
            set
            {
                _assmName = value;
                Report.Notify(Codes.INFO_SET_SET_ASMBL_NAME);
                if (string.IsNullOrEmpty(AssemblyName))
                {
                    Report.Notify(Codes.ERR_SET_SET_ASMBL_NAME_EMPTY);
                    return;
                }

                Load();
            }
        }

        public static void Load()
        {

            if (string.IsNullOrEmpty(AssemblyName))
            {
                Report.Notify(Codes.ERR_SET_LOAD_EMPT_ASMBL);
                return;
            }

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
                    Report.Notify(Codes.WARN_SET_FILE_NOT_EXST);
                    ResetToDefaults();
                }
            }
            catch
            {
                Report.Notify(Codes.ERR_SET_LOAD_UNREG);
                ResetToDefaults();
            }
        }

        public static void ResetToDefaults()
        {
            if (string.IsNullOrEmpty(AssemblyName))
            {
                Report.Notify(Codes.ERR_SET_RST_EMPT_ASMBL);
                return;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                CurrentSettings = Activator.CreateInstance<AppSettings>();
                Save();
            }
            catch
            {
                Report.Notify(Codes.ERR_SET_RST_UNREG);
            }
        }

        public static void Save()
        {
            if (string.IsNullOrEmpty(AssemblyName))
            {
                Report.Notify(Codes.ERR_SET_SAVE_EMPT_ASMBL);
                return;
            }

            try
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                options.WriteIndented = true;
                File.WriteAllText(FilePath, JsonSerializer.Serialize(CurrentSettings, options));
            }
            catch
            {
                Report.Notify(Codes.ERR_SET_SAVE_UNREG);
            }
        }

    } // public static class Settings<AppSettings> 
}     // namespace Regata.Core.Settings
