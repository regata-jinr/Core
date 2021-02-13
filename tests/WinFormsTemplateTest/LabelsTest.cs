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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Regata.Tests.WinForms
{
    [TestClass]
    public class LabelsTest
    {

        private string[] russianLabels = { "Журнал облучений1", "Журнал облучений2", "Список11", "Список12", "Список21", "Список22", "Загрузка", "Дата" };
        private string[] englishLabels = { "Irradiation Register", "Irradiation Register2", "List11", "List12", "List21", "List22", "LoadNumber", "Date" };

        private RegisterForm<Irradiation> Create_Form()
        {
            var f = new RegisterForm<Irradiation>();
            f.Name = "TestRegisterForm";

            var btn = new Button();

            f.FunctionalLayoutPanel.Controls.Add(btn, 0, 0);
            btn.Click += (sender, e) => { Create_Form(); };

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
            for (var i = 0; i < l.Count; ++i)
                isLang = isLang && langLabls.Contains(l[i]);

            return isLang;
        }


        [TestMethod]
        public void SwitchingLanguageMultiFormTest()
        {

            var f1 = Create_Form();
            var f2 = Create_Form();

            var l1 = FillList(ref f1);
            var l2 = FillList(ref f2);

            Assert.AreEqual(l1.Count, l2.Count);

            for (var i = 0; i < l1.Count; ++i)
                Assert.AreEqual(l1[i], l2[i]);

            Assert.IsTrue(IsLang(l1, ref englishLabels));
            Assert.IsTrue(IsLang(l2, ref englishLabels));

            Assert.IsFalse(IsLang(l1, ref russianLabels));
            Assert.IsFalse(IsLang(l2, ref russianLabels));

            Assert.AreEqual(Core.Settings.Language.English, f1.LangItem.CheckedItem);

            f1.LangItem.EnumMenuItem.DropDownItems[0].PerformClick();


            Assert.AreEqual(Core.Settings.Language.Russian, f1.LangItem.CheckedItem);

            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsFalse(IsLang(l1, ref englishLabels));
            Assert.IsFalse(IsLang(l2, ref englishLabels));

            Assert.IsTrue(IsLang(l1, ref russianLabels));
            Assert.IsTrue(IsLang(l2, ref russianLabels));


            Regata.Core.UI.WinForms.Labels.CurrentLanguage = Core.Settings.Language.English;

            Assert.AreEqual(Core.Settings.Language.English, f1.LangItem.CheckedItem);


            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsTrue(IsLang(l1, ref englishLabels));
            Assert.IsTrue(IsLang(l2, ref englishLabels));

            Assert.IsFalse(IsLang(l1, ref russianLabels));
            Assert.IsFalse(IsLang(l2, ref russianLabels));


            f2.LangItem.EnumMenuItem.DropDownItems[1].PerformClick();

            Assert.AreEqual(Core.Settings.Language.English, f1.LangItem.CheckedItem);

            l1 = FillList(ref f1);
            l2 = FillList(ref f2);

            Assert.IsTrue(IsLang(l1, ref englishLabels));
            Assert.IsTrue(IsLang(l2, ref englishLabels));

            Assert.IsFalse(IsLang(l1, ref russianLabels));
            Assert.IsFalse(IsLang(l2, ref russianLabels));

        }

    } // class LabelsTest
}     // namespace Regata.Tests.WinForms
