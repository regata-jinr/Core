/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.UI.WinForms;
using System.Linq;
using System.Windows.Forms;
using Regata.Core.DB.MSSQL.Models;


namespace Regata.Tests.WinForms

{
    [TestClass]
    public class RDataGridViewTest
    {

        public const string CSTarger = @"MSSQL_TEST_DB_ConnectionString";

        private RDataGridView<Irradiation> _rdgv;

        public RDataGridViewTest()
        {

            _rdgv = new RDataGridView<Irradiation>(CSTarger);

            _rdgv.CurrentDbSet.Where(ir => ir.LoadNumber == 122).ToArray();
            _rdgv.DataSource = _rdgv.CurrentDbSet.Local.ToBindingList();


            #region will_not_populate_dgv_wo_this

            using (var g = _rdgv.CreateGraphics()) { }
            using (var f = new Form())
            {
                f.Controls.Add(_rdgv);
                f.Controls.Remove(_rdgv);
            }

            #endregion


        }

        [TestMethod]
        public void ConstructorInitTest()
        {
            
            Assert.AreEqual(20, _rdgv.ColumnCount);
            Assert.AreEqual(119, _rdgv.RowCount);
            Assert.AreEqual(20359, _rdgv[0, 0].Value);
            Assert.AreEqual("m", _rdgv[1, 0].Value);
            Assert.AreEqual(10, _rdgv[9, 0].Value);
            Assert.AreEqual(344340,  _rdgv[9, 1].Value);
            Assert.AreEqual("m",  _rdgv[1, 3].Value);

        }

        [TestMethod]
        public void FillItemsTest()
        {

        }


    }
}
