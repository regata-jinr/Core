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

using System.Windows.Forms;
using Regata.Core.UI.WinForms.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Regata.Tests.WinForms
{
    [TestClass]
    public class DGVTabPaneControlTest
    {
        //private DGVTabPaneControl _dgvTabs;

        [TestMethod]
        public void SizesAndPositionsTest()
        {
            SizesAndPositions(new DGVTabPaneControl(3, 2, 0.66f));
            SizesAndPositions(new DGVTabPaneControl(3, 1));
        }

        public void SizesAndPositions(DGVTabPaneControl _dgvTabs)
        { 
            for (var i = 0; i < _dgvTabs.Pages.Count; ++i)
            {
                
                Assert.AreEqual($"tabPage{i + 1}", _dgvTabs.Pages[i].Name);
                Assert.AreEqual($"dgv_{i+1}_{1}", _dgvTabs[i,0].Name);



                if (_dgvTabs.Pages[0].Controls.OfType<DataGridView>().Count() > 1)
                {
                }
            }
        }

    } // class DGVTabPaneControlTest
}     // namespace Regata.Tests.WinForms
