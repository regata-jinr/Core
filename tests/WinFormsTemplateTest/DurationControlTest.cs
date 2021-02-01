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

using System;
using Regata.Core.UI.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Regata.Tests.WinForms
{
    [TestClass]
    public class DurationControlTest
    {
        //private DGVTabPaneControl _dgvTabs;
        private TimeSpan ts = new TimeSpan(1, 1, 1, 1);

        [TestMethod]
        public void TimeSpanTest()
        {
            var d = new DurationControl(1, 1, 1, 1);

            Assert.AreEqual(new TimeSpan(1, 1, 1, 1), d.Duration);

            d.DurationChanged += D_DurationChanged;
            d.MinutesChanged += D_MinutesChanged;
            d.SecondsChanged += D_SecondsChanged; ;

            d.Duration = ts.Add(TimeSpan.FromMinutes(20));

        }

        private void D_SecondsChanged(object sender, EventArgs e)
        {
            Assert.IsTrue(false);
        }

        private void D_MinutesChanged(object sender, EventArgs e)
        {
            var d = sender as DurationControl;
            Assert.AreEqual(new TimeSpan(1, 1, 21, 1), d.Duration);
        }

        private void D_DurationChanged(object sender, EventArgs e)
        {
            var d = sender as DurationControl;
            Assert.AreEqual(new TimeSpan(1, 1, 21, 1), d.Duration);
        }

    } // class DGVTabPaneControlTest
}     // namespace Regata.Tests.WinForms
