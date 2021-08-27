/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.Hardware;

namespace Regata.Tests.Hardware.Detectors
{
    [TestClass]
    public class SampleChangerTest
    {
        [TestMethod]
        public void SampleChangerErrorHandlingTest()
        {
            using (var s = new SampleChanger(107376))
            {
                Assert.AreEqual(5, s.ComPort);
            }
        }
    }
}
