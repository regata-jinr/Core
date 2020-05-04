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
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Regata.Utilities
{
    public enum Status { Info, Processing, Success, Canceled };
    public enum Languages { Russian, English };

    // NOTE: perhaps internal localization tools in winforms is better, but how I iderstood it doesn't allow to switch language during the runtime

    public class Settings
    {
        private static bool _isFirstreading = true;
        private Languages _currentLanguage;
        private readonly string _path;

        public Languages CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                Labels.CurrentLanguage = _currentLanguage;
            }
        }

        public IList<string> NonDisplayedColumns { get; set; }

        private void ReadSettings()
        {
            try
            {
                if (_isFirstreading)
                {
                    _isFirstreading = false; // this fix stack overflow in json desirialize bellow
                    if (File.Exists(_path))
                    {
                        var options = new JsonSerializerOptions();
                        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                        var set = JsonSerializer.Deserialize<Settings>(File.ReadAllText(_path), options);
                        _currentLanguage = set.CurrentLanguage;
                        NonDisplayedColumns = set.NonDisplayedColumns;
                        if (NonDisplayedColumns == null)
                            NonDisplayedColumns = new List<string>();
                    }
                    else
                        ResetFileSettings();
                }
            }
            catch (JsonException)
            {
                ResetFileSettings();
            }
        }

        private void ResetFileSettings()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
            using (var f = File.CreateText(_path))
            { }
            
            CurrentLanguage = Languages.English;
            NonDisplayedColumns = new List<string>();
        }

        public Settings(string AssemblyName)
        {
            if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");
            _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Regata\\{AssemblyName}\\settings.json";
            ReadSettings();
        }

        public void SaveSettings()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;
            File.WriteAllText(_path, JsonSerializer.Serialize(this, options));
        }
    }

    public static class Labels
    {
        public static Languages CurrentLanguage;
        public static Status CurrentStatus;

        #region ControlLabels

        public static string ToolStripMenuItemMenu
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Menu";
                    case Languages.English:
                        return "Menu";
                    default: return "";
                }
            }
        }

        public static string ToolStripMenuItemMenuLang
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Language";
                    case Languages.English:
                        return "Language";
                    default: return "";
                }
            }
        }

        public static string ToolStripMenuItemMenuLangRus
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Russian";
                    case Languages.English:
                        return "Russian";
                    default: return "";
                }
            }
        }

        public static string ToolStripMenuItemMenuLangEng
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "English";
                    case Languages.English:
                        return "English";
                    default: return "";
                }
            }
        }

        public static string ToolStripMenuItemMenuChoseSpectra
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Выбрать файлы спектров";
                    case Languages.English:
                        return "Choose spectra files";
                    default: return "";
                }
            }
        }

        public static string FaceFormButtonStart
        {
            get
            {
                switch (CurrentStatus)
                {
                    case Status.Info:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Пуск";
                            case Languages.English:
                                return "Start";
                            default: return "";
                        }
                    case Status.Processing:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Продолжить";
                            case Languages.English:
                                return "Continue";
                            default: return "";
                        }
                    case Status.Canceled:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Продолжить";
                            case Languages.English:
                                return "Continue";
                            default: return "";
                        }
                    case Status.Success:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Пуск";
                            case Languages.English:
                                return "Start";
                            default: return "";
                        }
                    default:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Пуск";
                            case Languages.English:
                                return "Start";
                            default: return "";
                        }
                }
            }
        }

        public static string FaceFormButtonCancel
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Отмена";
                    case Languages.English:
                        return "Cancel";
                    default: return "";
                }
            }
        }

        public static string FaceFormButtonClear
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Очистить";
                    case Languages.English:
                        return "Clear";
                    default: return "";
                }
            }
        }

        //public static string FaceForm
        //{
        //    get
        //    {
        //        return $"{Labels.FaceForm} - {vers.Major.ToString()}.{vers.Minor.ToString()}.{vers.Build.ToString()}";
        //    }
        //}

        public static string FaceFormToolStripStatusLabel
        {
            get
            {
                switch (CurrentStatus)
                {
                    case Status.Canceled:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Обработка была отменена. Нажмите 'Продолжить' чтобы возобновить обработку или очистите таблицу и выберите новые файлы";
                            case Languages.English:
                                return "Process has been canceled. Press 'Continue' to run it again or clear the table and choose new files";
                            default: return "";
                        }
                    case Status.Success:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Все файлы были успешно обработаны";
                            case Languages.English:
                                return "All files were processed successfuly";
                            default: return "";
                        }
                    case Status.Info:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Нажмите 'Пуск' чтобы начать обработку. Количество выбранных файлов - ";
                            case Languages.English:
                                return "Press 'Start' button to process chosen files.The number of chosen files is ";
                            default: return "";
                        }
                    case Status.Processing:
                        switch (CurrentLanguage)
                        {
                            case Languages.Russian:
                                return "Обрабатывается файл ";
                            case Languages.English:
                                return "Processing of file ";
                            default:
                                return "Processing of file ";
                        }
                    default: return "";
                }
            }
        }

        public static string statusStrip1 { get { return ""; } }

        public static string ToolStripMenuItemViewShowColumns { 
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Показывать столбцы";
                    case Languages.English:
                        return "Show columns";
                    default:
                        return "Show columns";
                }
            }
        }

        public static string ToolStripMenuItemView
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Вид";
                    case Languages.English:
                        return "View";
                    default:
                        return "View";
                }
            }
        }



        #endregion

        #region ColumnLabels

        public static string File
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Файл";
                    case Languages.English:
                        return "File";
                    default: return "";
                }
            }
        }

        public static string Id
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Номер партии";
                    case Languages.English:
                        return "SampleSet";
                    default: return "";
                }
            }
        }

        public static string Title
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Номер образца";
                    case Languages.English:
                        return "SampleId";
                    default: return "";
                }
            }
        }

        public static string CollectorName
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Оператор";
                    case Languages.English:
                        return "Operator";
                    default: return "";
                }
            }
        }

        public static string Type
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Тип";
                    case Languages.English:
                        return "Type";
                    default: return "";
                }
            }
        }

        public static string Quantity
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Вес";
                    case Languages.English:
                        return "Weight";
                    default: return "";
                }
            }
        }

        public static string Uncertainty
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Погрешность";
                    case Languages.English:
                        return "Error";
                    default: return "";
                }
            }
        }

        public static string Units
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Единицы измерения";
                    case Languages.English:
                        return "Units";
                    default: return "";
                }
            }
        }

        public static string Geometry
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Высота";
                    case Languages.English:
                        return "Height";
                    default: return "";
                }
            }
        }

        public static string Duration
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Продолжительность, с";
                    case Languages.English:
                        return "Duration, s";
                    default: return "";
                }
            }
        }

        public static string DeadTime
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Мертвое время";
                    case Languages.English:
                        return "Dead time";
                    default: return "";
                }
            }
        }

        public static string BuildUpType
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Тип образования";
                    case Languages.English:
                        return "Build up type";
                    default: return "";
                }
            }
        }

        public static string BeginDate
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Дата начала облучения";
                    case Languages.English:
                        return "Irradiation begin date";
                    default: return "";
                }
            }
        }

        public static string EndDate
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Дата окончания облучения";
                    case Languages.English:
                        return "Irradiation end date";
                    default: return "";
                }
            }
        }

        public static string Description
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Описание";
                    case Languages.English:
                        return "Description";
                    default: return "";
                }
            }
        }

        public static string ReadSuccess
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Успешно прочитан";
                    case Languages.English:
                        return "Read successfully";
                    default: return "";
                }
            }
        }

        public static string ErrorMessage
        {
            get
            {
                switch (CurrentLanguage)
                {
                    case Languages.Russian:
                        return "Сообщение об ошибке";
                    case Languages.English:
                        return "Error message";
                    default: return "";
                }
            }
        }

        #endregion
    }
}
