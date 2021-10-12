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

using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Regata.Core.Messages;
using System.Runtime.CompilerServices;

namespace Regata.Core.Settings
{
    public enum Language { Russian, English};

    public abstract class ASettings : INotifyPropertyChanged
    {
        private Language _lang;
        public Language CurrentLanguage
        {
            get { return _lang; }

            set
            {
                _lang = value;
                GlobalSettings.CurrentLanguage = _lang;
                NotifyPropertyChanged();
            }
        }
        public Status _verbosity = Status.Error;
        public Status Verbosity
        {
            get
            {
                return _verbosity;
            }
            set
            {
                _verbosity = value;
                GlobalSettings.Verbosity = _verbosity;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Target
    {
        public string DiskJinr { get; set; }
        public string DB { get; set; }
        public string LogPath { get; set; }
    }

    public static class GlobalSettings
    {
        public static Target Targets;
        static private IConfiguration Configuration { get; set; }

        static GlobalSettings()
        {
            Targets = new Target();
            try
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("targets.json", optional: false, reloadOnChange: true)
                    .Build();


                Configuration.GetSection(nameof(Target)).Bind(Targets);
            }
            // todo: add file not found or file corrupted
            catch
            {
                Report.Notify(new Message(Codes.ERR_SET_CONFIG_LOAD_UNREG));
                throw;
            }
        }

        public static string User { get; set; }

        public static string MailHostTarget;
        public static string EmailRecipients;
        
        public static Status Verbosity { get; set; }

        public static Language CurrentLanguage { get; internal set; }

    }

    public static class Settings<TSettings> 
        where TSettings : ASettings
    {
        public static TSettings CurrentSettings;

        public static string FilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Regata",AssemblyName,"settings.json");
            }
        }

        private  static string _assmName;

        public  static string AssemblyName { 
            get 
            {
                if (string.IsNullOrEmpty(_assmName))
                {
                    Report.Notify(new Message(Codes.ERR_SET_LOAD_EMPT_ASMBL));
                    throw new ArgumentNullException("For the correct usage of settings system you have to provide AssemblyName");
                }
                return _assmName; 
            }
            set
            {
                _assmName = value;
                //Report.Notify(new Message(Codes.INFO_SET_SET_ASMBL_NAME));
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
                    CurrentSettings = JsonSerializer.Deserialize<TSettings>(File.ReadAllText(FilePath), options);
                }
                else
                {
                    Report.Notify(new Message(Codes.WARN_SET_FILE_NOT_EXST));
                    ResetToDefaults();
                }

                GlobalSettings.CurrentLanguage = CurrentSettings.CurrentLanguage;
                GlobalSettings.Verbosity = CurrentSettings.Verbosity;
                // FIXME: where to remove subscription? IDisposable?
                CurrentSettings.PropertyChanged += (s,e) => { Save(); };

            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_SET_LOAD_UNREG) { DetailedText = ex.ToString() });
                ResetToDefaults();
            }
        }

        public static void ResetToDefaults()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                CurrentSettings = Activator.CreateInstance<TSettings>();
                Save();
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_SET_RST_UNREG) { DetailedText = ex.ToString() });
            }
        }

        public static void Save()
        {
            try
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                options.WriteIndented = true;
                var s = JsonSerializer.Serialize(CurrentSettings, options);
                File.WriteAllText(FilePath, s);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_SET_SAVE_UNREG) { DetailedText = ex.ToString() } );
            }
        }

    } // public static class Settings<AppSettings> 
}     // namespace Regata.Core.Settings
