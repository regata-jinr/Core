using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.UITemplates;


namespace UITemplateWinFormTest
{
    [TestClass]
    public partial class TestForm : Regata.UITemplates.DataTableForm<SamplesSetModel>
    {
        public TestForm() : base ("testAssembly")
        {
            //InitializeComponent();

            Assert.AreEqual("Country_Code", DataGridView.Columns[0].HeaderText);


        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(1, 1);
        }


    }
}
