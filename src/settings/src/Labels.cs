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

using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Regata.Core.Settings
{
    public class UILabels
    {
        public string FormName { get; set; }
        public string ComponentName { get; set; }
        public string RusText { get; set; }
        public string EngText { get; set; }
    }


    public static class Labels
    {
        private static IReadOnlyList<UILabels> _uil;
        private static string _path_labels_file;

        public static string PathToLabelsFile
        {
            get
            { return _path_labels_file; }
            set
            {
                try
                {
                    _path_labels_file = value;
                    if (!File.Exists(PathToLabelsFile))
                    {
                        Report.Notify(Codes.ERR_SET_LBL_FILE_NOT_EXST);
                        return;
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    _uil = JsonSerializer.Deserialize<IReadOnlyList<UILabels>>(File.ReadAllText(PathToLabelsFile), options);
                }
                catch
                {
                    Report.Notify(Codes.ERR_SET_LBL_FILE_UNREG);

                }
            }
        }

        private static IReadOnlyList<Message> _msgs;

        private static string _path_code_msgs_file;

        public static string PathToCodeMsgsFile
        {
            get
            { return _path_code_msgs_file; }
            set
            {
                try
                {
                    _path_code_msgs_file = value;
                    if (!File.Exists(PathToCodeMsgsFile))
                    {
                        Report.Notify(Codes.ERR_SET_CODE_FILE_NOT_EXST);
                        return;
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    _msgs = JsonSerializer.Deserialize<IReadOnlyList<Message>>(File.ReadAllText(PathToCodeMsgsFile), options);
                }
                catch
                {
                        Report.Notify(Codes.ERR_SET_CODE_FILE_UNREG);

                }
            }
        }


        public static string GetLabel(string compt, Languages cLang)
        {
            try
            {
                if (_uil == null) Report.Notify(Codes.ERR_SET_LBL_ARR_NULL);
                if (!_uil.Select(l => l.ComponentName).Contains(compt)) return "";
                if (cLang == Languages.Russian)
                    return _uil.Where(f => f.ComponentName == compt).Select(f => f.RusText).First();
                else
                    return _uil.Where(f => f.ComponentName == compt).Select(f => f.EngText).First();
            }
            catch
            {
                Report.Notify(Codes.ERR_SET_GET_LBL_UNREG);
                return string.Empty;

            }

        }


        public static Message GetMessage(ushort code, Languages cLang)
        {
            try
            {
                if (_msgs == null) Report.Notify(Codes.ERR_SET_MSG_ARR_NULL);
                if (!_msgs.Select(l => l.Code).Contains(code)) return null;
                if (cLang == Languages.Russian)
                    return _msgs.Where(f => f.Code == code).First();
                else
                    return _msgs.Where(f => f.Code == code).First();
            }
            catch
            {
                Report.Notify(Codes.ERR_SET_GET_MSG_UNREG);
                return null;

            }
        }


    } // public static class Labels
} // namespace Regata.Core.Settings
