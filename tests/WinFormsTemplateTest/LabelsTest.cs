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
using System.Collections.Generic;
using System.Windows.Forms;
using Regata.Core.UI.WinForms.Forms;
using Regata.Core.DB.MSSQL.Models;
using Regata.Core.DB.MSSQL.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Regata.Tests.WinForms
{
    [TestClass]
    public class LabelsTest
    {

        private string[] russianLabels = { "Журнал облучений1", "Журнал облучений2", "Список11", "Список12", "Список21", "Список22", "Загрузка", "Дата", "Дата" };
        private string[] englishLabels = { "Irradiation Register", "Irradiation Register2", "List11", "List12", "List21", "List22", "Load Number", "Date", "Date" };

        private RegisterForm<Irradiation> Create_Form()
        {
            var f = new RegisterForm<Irradiation>();
            f.Name = "TestRegisterForm";

            var btn = new Button();

            f.FunctionalLayoutPanel.Controls.Add(btn, 0, 0);
            using (var r = new RegataContext())
            {
                f.TabsPane[0, 0].DataSource = r.Irradiations.Where(ir => ir.Type == 1 && ir.DateTimeStart != null).Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date }).Distinct().Take(10).ToArray();

                f.TabsPane[1, 0].DataSource = r.Irradiations.Where(ir => ir.Type == 0 && ir.DateTimeStart != null).Select(ir => new { ir.DateTimeStart.Value.Date }).Distinct().Take(10).ToArray();
            }

            return f;
        }

        private List<string> FillList(ref RegisterForm<Irradiation> f)
        {
            var labels = new List<string>();

            labels.Add(f.TabsPane.Pages[0].Text);
            labels.Add(f.TabsPane.Pages[1].Text);
            labels.AddRange(f.TabsPane.Pages[0].Controls.OfType<Label>().Select(l => l.Text).ToList());
            labels.AddRange(f.TabsPane.Pages[1].Controls.OfType<Label>().Select(l => l.Text).ToList());
            labels.AddRange(f.TabsPane[0, 0].Columns.OfType<DataGridViewColumn>().Select(l => l.HeaderText).ToList());
            labels.AddRange(f.TabsPane[1, 0].Columns.OfType<DataGridViewColumn>().Select(l => l.HeaderText).ToList());

            return labels;

        }

        public bool IsLang(List<string> l, ref string[] langLabls)
        {
            bool isLang = true;

            if (langLabls.Length != l.Count) return false;

            for (var i = 0; i < langLabls.Length; ++i)
                isLang = isLang && langLabls.Contains(l[i]);

            return isLang;
        }


        [TestMethod]
        public void SwitchingLanguageMultiFormTest()
        {
            // two forms here to check that switching on a one form lead to switching on another
            var f1 = Create_Form();
            var f2 = Create_Form();

            var l1 = FillList(ref f1);
            var l2 = FillList(ref f2);

            Assert.AreEqual(l1.Count, l2.Count);

            for (var i = 0; i < l1.Count; ++i)
                Assert.AreEqual(l1[i], l2[i]);

            Assert.IsTrue(IsLang(l1, ref russianLabels));
            Assert.IsTrue(IsLang(l2, ref russianLabels));

            Assert.IsFalse(IsLang(l1, ref englishLabels));
            Assert.IsFalse(IsLang(l2, ref englishLabels));

            Assert.AreEqual(Core.Settings.Language.Russian, f1.LangItem.CheckedItem);

            f1.LangItem.EnumMenuItem.DropDownItems[1].PerformClick();


            Assert.AreEqual(Core.Settings.Language.English, f1.LangItem.CheckedItem);

            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsTrue(IsLang(l1, ref englishLabels));
            Assert.IsTrue(IsLang(l2, ref englishLabels));

            Assert.IsFalse(IsLang(l1, ref russianLabels));
            Assert.IsFalse(IsLang(l2, ref russianLabels));


            Regata.Core.Settings.GlobalSettings.CurrentLanguage = Core.Settings.Language.Russian;

            Assert.AreEqual(Core.Settings.Language.Russian, f1.LangItem.CheckedItem);


            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsTrue(IsLang(l1, ref russianLabels));
            Assert.IsTrue(IsLang(l2, ref russianLabels));

            Assert.IsFalse(IsLang(l1, ref englishLabels));
            Assert.IsFalse(IsLang(l2, ref englishLabels));


            f2.LangItem.EnumMenuItem.DropDownItems[0].PerformClick();

            Assert.AreEqual(Core.Settings.Language.Russian, f1.LangItem.CheckedItem);

            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsTrue(IsLang(l1, ref russianLabels));
            Assert.IsTrue(IsLang(l2, ref russianLabels));

            Assert.IsFalse(IsLang(l1, ref englishLabels));
            Assert.IsFalse(IsLang(l2, ref englishLabels));

        }

    } // class LabelsTest
}     // namespace Regata.Tests.WinForms
