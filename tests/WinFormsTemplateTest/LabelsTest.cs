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
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Regata.Core.UI.WinForms.Forms;
using Regata.Core.UI.WinForms;
using Regata.Core.DataBase.Models;
using Regata.Core.DataBase;
using Regata.Core.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Regata.Tests.WinForms
{

    public class LabelsSettings : ASettings
    { }

    [TestClass]
    public class LabelsTest
    {

        public LabelsTest()
        {
            Settings<LabelsSettings>.AssemblyName = "LabelsTest";
        }

        private string[] russianLabels1 = { "Журналы облучений1", "Журналы измерений1", "Список111", "Список121", "Список211", "Список221", "Загрузка1", "Дата1", "Дата1" };
        private string[] englishLabels1 = { "Irradiation Registers1", "Measurement Registers1", "List111", "List121", "List211", "List221", "LoadNumber1", "Date1", "Date1" };
        
        private string[] russianLabels = { "Журналы облучений", "Журналы измерений", "Список11", "Список12", "Список21", "Список22", "Загрузка", "Дата", "Дата" };
        private string[] englishLabels = { "Irradiation Registers", "Measurement Registers", "List11", "List12", "List21", "List22", "LoadNumber", "Date", "Date" };

        private RegisterForm<Irradiation> Create_Form(string name)
        {
            var f = new RegisterForm<Irradiation>(Settings<LabelsSettings>.CurrentSettings.CurrentLanguage);
            f.LangItem.CheckItem(Settings<LabelsSettings>.CurrentSettings.CurrentLanguage);
            f.LangItem.CheckedChanged += () => { Settings<LabelsSettings>.CurrentSettings.CurrentLanguage = f.LangItem.CheckedItem; Labels.SetControlsLabels(f.Controls); };
            f.Name = name;

            var btn = new Button();

            f.FunctionalLayoutPanel.Controls.Add(btn, 0, 0);
            using (var r = new RegataContext())
            {
                f.TabsPane[0, 0].DataSource = r.Irradiations.Where(ir => ir.Type == 1 && ir.DateTimeStart != null).Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date }).Distinct().Take(10).ToArray();

                f.TabsPane[1, 0].DataSource = r.Irradiations.Where(ir => ir.Type == 0 && ir.DateTimeStart != null).Select(ir => new { ir.DateTimeStart.Value.Date }).Distinct().Take(10).ToArray();
            }
            Labels.SetControlsLabels(f.Controls);
            return f;
        }

        private List<string> GetSomeTextFromForm(ref RegisterForm<Irradiation> f)
        {
            var labels = new List<string>();

            labels.Add(f.TabsPane.Pages[0].Text);
            labels.Add(f.TabsPane.Pages[1].Text);
            labels.AddRange(f.TabsPane.Pages[0].Controls[0].Controls.OfType<Label>().Select(l => l.Text).ToList());
            labels.AddRange(f.TabsPane.Pages[1].Controls[0].Controls.OfType<Label>().Select(l => l.Text).ToList());
            labels.AddRange(f.TabsPane[0, 0].Columns.OfType<DataGridViewColumn>().Select(l => l.HeaderText).ToList());
            labels.AddRange(f.TabsPane[1, 0].Columns.OfType<DataGridViewColumn>().Select(l => l.HeaderText).ToList());

            return labels;

        }

        public bool IsListsMatched(List<string> l, ref string[] langLabls)
        {
            bool isLang = true;

            if (langLabls.Length != l.Count) return false;

            for (var i = 0; i < langLabls.Length; ++i)
            {
                if (!isLang) return false;
                isLang = isLang && langLabls.Contains(l[i]);
            }

            return isLang;
        }

        [TestMethod]
        public void LanguageLoadedFromSettingsFileTest()
        {

            var set = File.ReadAllText(@"C:\Users\bdrum\AppData\Roaming\Regata\LabelsTest\settings.json");

            if (set.Contains("russian"))
                Assert.AreEqual(Language.Russian, Settings<LabelsSettings>.CurrentSettings.CurrentLanguage);
            else
                Assert.AreEqual(Language.English, Settings<LabelsSettings>.CurrentSettings.CurrentLanguage);


        }

        [TestMethod]
        public void GlobalSettingsLanguageLoadedFromSettingsFileTest()
        {

            var set = File.ReadAllText(@"C:\Users\bdrum\AppData\Roaming\Regata\LabelsTest\settings.json");

            if (set.Contains("russian"))
                Assert.AreEqual(Language.Russian, GlobalSettings.CurrentLanguage);
            else
                Assert.AreEqual(Language.English, GlobalSettings.CurrentLanguage);

        }

        [TestMethod]
        public void SwitchingLanguageMultiFormTest()
        {
            // two forms here to check that switching on a one form lead to switching on another
            var f = Create_Form("TestRegisterForm");
            var f1 = Create_Form("TestRegisterForm1");

            // switch to russian 

            f1.LangItem.EnumMenuItem.DropDownItems[0].PerformClick();
            f.LangItem.EnumMenuItem.DropDownItems[0].PerformClick();

            var l = GetSomeTextFromForm(ref f);
            var l1 = GetSomeTextFromForm(ref f1);

            Assert.AreEqual(l.Count, l1.Count);

            Assert.AreEqual(Language.Russian, GlobalSettings.CurrentLanguage);
            Assert.AreNotEqual(Language.English, GlobalSettings.CurrentLanguage);
            Assert.AreEqual(Language.Russian, f1.LangItem.CheckedItem);
            Assert.AreNotEqual(Language.English, f1.LangItem.CheckedItem);


            Assert.IsTrue(IsListsMatched(l, ref russianLabels));
            Assert.IsTrue(IsListsMatched(l1, ref russianLabels1));

            // switch to english

            f1.LangItem.EnumMenuItem.DropDownItems[1].PerformClick();
            f.LangItem.EnumMenuItem.DropDownItems[1].PerformClick();

            Assert.AreEqual(Language.English, GlobalSettings.CurrentLanguage);
            Assert.AreNotEqual(Language.Russian, GlobalSettings.CurrentLanguage);

            l = GetSomeTextFromForm(ref f);
            l1 = GetSomeTextFromForm(ref f1);

            Assert.AreEqual(l.Count, l1.Count);

            Assert.IsTrue(IsListsMatched(l, ref englishLabels));
            Assert.IsTrue(IsListsMatched(l1, ref englishLabels1));

            Assert.AreEqual(Language.English, f1.LangItem.CheckedItem);
            Assert.AreNotEqual(Language.Russian, f1.LangItem.CheckedItem);

        }

    } // class LabelsTest
}     // namespace Regata.Tests.WinForms
