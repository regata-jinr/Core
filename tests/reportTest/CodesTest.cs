/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core;

// TODO: add checking for level bins - INFO from 1 to 999 and so on
namespace Regata.Tests.Reports
{
    [TestClass]
    public class CodesTest
    {
        [TestMethod]
        public void AllCodesUniqueTest()
        {
            Assert.IsTrue(Codes.Contains(0));
            Assert.IsTrue(Codes.Contains(1));
            Assert.IsTrue(Codes.Contains(2));
            //Assert.IsTrue(Codes.Contains(1000));
            //Assert.IsTrue(Codes.Contains(1001));
            //Assert.IsTrue(Codes.Contains(1002));
            //Assert.IsTrue(Codes.Contains(2002));
            Assert.IsTrue(Codes.Contains(3002));
            Assert.IsFalse(Codes.Contains(4002));
            Assert.IsFalse(Codes.Contains(5002));
            Assert.IsFalse(Codes.Contains(6002));
        }
    } // public class CodesTest
}     // namespace Tests
