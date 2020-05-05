/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System.Collections.Generic;

namespace Regata.UITemplates
{
    public static class Labels
    {
        public static Languages CurrentLanguage;

        private static IDictionary<string, string> EnglishLabels = new Dictionary<string, string>
        { { "MenuItemMenu",            "Menu"},
          { "MenuItemMenuLang",        "Language"},
          { "MenuItemMenuLangRus",     "Russian"},
          { "MenuItemMenuLangEng",     "English"},
          { "MenuItemViewShowColumns", "Show columns"},
          { "MenuItemView",            "View"}
        };

        private static IDictionary<string, string> RussianLabels = new Dictionary<string, string>
        { { "MenuItemMenu",            "Меню"},
          { "MenuItemMenuLang",        "Language"},
          { "MenuItemMenuLangRus",     "Russian"},
          { "MenuItemMenuLangEng",     "English"},
          { "MenuItemViewShowColumns", "Показывать столбцы"},
          { "MenuItemView",            "Вид"}
        };

        private static void AddLabels(ref IDictionary<string, string> fromDict, ref IDictionary<string, string> toDict)
        {
            foreach (var kv in fromDict)
            { 
                if (!toDict.ContainsKey(kv.Key))
                    toDict.Add(kv.Key, kv.Value);
                else
                    toDict[kv.Key] = kv.Value;
            }
        }

        public static void AddRussianLabels(ref IDictionary<string, string> rusLabels)
        {
            AddLabels(ref rusLabels, ref RussianLabels);
        }

        public static void AddEnglishLabels(ref IDictionary<string, string> engLabels)
        {
            AddLabels(ref engLabels, ref EnglishLabels);
        }

        public static string GetLabel(string name)
        {
            try
            {
                if (CurrentLanguage == Languages.English)
                    return EnglishLabels[name];
                if (CurrentLanguage == Languages.Russian)
                    return RussianLabels[name];
            }
            catch (KeyNotFoundException) { }
            return name;
        }

    } // public static class Labels
} // namespace Regata.UITemplates
