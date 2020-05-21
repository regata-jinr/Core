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

// TODO: get texts data from db.wfl.formnametable 
// TODO: load one time during form init

namespace Regata.UITemplates
{
    public static class Labels
    {
        public static Languages CurrentLanguage;

        private static IDictionary<string, string> EnglishLabels = new Dictionary<string, string>
        {
            { "MenuItemMenu",            "Menu"                },
            { "MenuItemMenuLang",        "Language"            },
            { "MenuItemMenuLangRus",     "Russian"             },
            { "MenuItemMenuLangEng",     "English"             },
            { "MenuItemViewShowColumns", "Show columns"        },
            { "MenuItemView",            "View"                },
            { "FormText",                "RegataUITemplate"    },
            { "F_Country_Code",          "Country code"        },
            { "F_Client_Id",             "Client id"           },
            { "F_Year",                  "Year"                },
            { "F_Sample_Set_Id",         "Sample set id"       },
            { "F_Sample_Set_Index",      "Sample set index"    },
            { "A_Sample_ID",             "Sample id"           },
            { "A_Client_Sample_ID",      "Client sample id"    },
            { "A_Sample_Type",           "Type"                },
            { "A_Sample_Subtype",        "Subtype"             },
            { "A_Latitude",              "Latitude"            },
            { "A_Longitude",             "Longitude"           },
            { "A_Collection_Place",      "Collection place"    },
            { "A_Notes",                 "Notes"               },
            { "A_Determined_Elements",   "Determined elements" },
            { "A_Drying_Plan",           "Drying"              },
            { "A_Freeze_Drying_Plan",    "Freeze-drying"       },
            { "A_Homogenizing_Plan",     "Pulverization"       },
            { "A_Pelletization_Plan",    "Pelletizing"         },
            { "P_Weighting_SLI",         "Weight SLI, g."      },
            { "P_Weighting_LLI",         "Weight LLI, g."      }
        };

        private static IDictionary<string, string> RussianLabels = new Dictionary<string, string>
        {
            { "MenuItemMenu",            "Меню"                     },
            { "MenuItemMenuLang",        "Language"                 },
            { "MenuItemMenuLangRus",     "Russian"                  },
            { "MenuItemMenuLangEng",     "English"                  },
            { "MenuItemViewShowColumns", "Показывать столбцы"       },
            { "FormText",                "RegataUITemplate"         },
            { "MenuItemView",            "Вид"                      },
            { "F_Country_Code",          "Код страны"               },
            { "F_Client_Id",             "Код клиента"              },
            { "F_Year",                  "Год"                      },
            { "F_Sample_Set_Id",         "Номер партии"             },
            { "F_Sample_Set_Index",      "Индекс партии"            },
            { "A_Sample_ID",             "Номер образца"            },
            { "A_Client_Sample_ID",      "Клиентский номер образца" },
            { "A_Sample_Type",           "Тип"                      },
            { "A_Sample_Subtype",        "Подтип"                   },
            { "A_Latitude",              "Широта"                   },
            { "A_Longitude",             "Долгота"                  },
            { "A_Collection_Place",      "Место сбора"              },
            { "A_Notes",                 "Заметки"                  },
            { "A_Determined_Elements",   "Определяемые элементы"    },
            { "A_Drying_Plan",           "Сушка"                    },
            { "A_Freeze_Drying_Plan",    "Лиофилизация"             },
            { "A_Homogenizing_Plan",     "Гомогенизация"            },
            { "A_Pelletization_Plan",    "Пеллетезация"             },
            { "P_Weighting_SLI",         "Вес КЖИ, г."              },
            { "P_Weighting_LLI",         "Вес ДЖИ, г."              }
        };

        private static void AddLabels(ref IReadOnlyDictionary<string, string> fromDict, ref IDictionary<string, string> toDict)
        {
            foreach (var kv in fromDict)
            { 
                if (!toDict.ContainsKey(kv.Key))
                    toDict.Add(kv.Key, kv.Value);
                else
                    toDict[kv.Key] = kv.Value;
            }
        }

        public static void AddRussianLabels(ref IReadOnlyDictionary<string, string> rusLabels)
        {
            AddLabels(ref rusLabels, ref RussianLabels);
        }

        public static void AddEnglishLabels(ref IReadOnlyDictionary<string, string> engLabels)
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
