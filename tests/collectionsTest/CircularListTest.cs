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
using Regata.Core.Collections;
using System.Linq;


namespace Regata.Tests.Collections
{
    [TestClass]
    public class CircularListTest
    {
        [TestMethod]
        public void BasicFunctionsTest()
        {
            var cl = new CircularList<int>(new int[] {1, 2, 3, 4, 5, 6, 7 });

            Assert.AreEqual(7, cl.Count());

            Assert.AreEqual(1, cl.CurrentItem);
            Assert.AreEqual(2, cl.NextItem);
            Assert.AreEqual(3, cl.NextItem);
            Assert.AreEqual(4, cl.NextItem);
            Assert.AreEqual(5, cl.NextItem);
            Assert.AreEqual(6, cl.NextItem);
            Assert.AreEqual(7, cl.NextItem);

            Assert.AreEqual(1, cl.NextItem);
            Assert.AreEqual(2, cl.NextItem);
            Assert.AreEqual(3, cl.NextItem);
            Assert.AreEqual(4, cl.NextItem);
            Assert.AreEqual(5, cl.NextItem);
            Assert.AreEqual(6, cl.NextItem);
            Assert.AreEqual(7, cl.NextItem);

            Assert.AreEqual(6, cl.PrevItem);
            Assert.AreEqual(5, cl.PrevItem);
            Assert.AreEqual(4, cl.PrevItem);
            Assert.AreEqual(3, cl.PrevItem);
            Assert.AreEqual(2, cl.PrevItem);
            Assert.AreEqual(1, cl.PrevItem);

            Assert.AreEqual(7, cl.PrevItem);
            Assert.AreEqual(6, cl.PrevItem);
            Assert.AreEqual(5, cl.PrevItem);
            Assert.AreEqual(4, cl.PrevItem);
            Assert.AreEqual(3, cl.PrevItem);
            Assert.AreEqual(2, cl.PrevItem);
            Assert.AreEqual(1, cl.PrevItem);

            Assert.AreEqual(2, cl.NextItem);
            Assert.AreEqual(1, cl.PrevItem);
            Assert.AreEqual(2, cl.NextItem);
        
        }

    } //public class CircularListTest
}     // namespace Regata.Tests.Collections
