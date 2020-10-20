using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UITemplateWinFormTest
{
    [TestClass]
    public partial class TestForm : Regata.UITemplates.DataTableForm<SamplesSetModel>
    {
        public TestForm() : base ("testAssembly")
        {
            Assert.AreEqual("Country_Code", DataGridView.Columns[0].HeaderText);

        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(1, 1);
        }


    }
}
