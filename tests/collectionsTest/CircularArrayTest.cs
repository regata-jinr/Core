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


namespace Regata.Tests.Collections
{
    [TestClass]
    public class CircularArrayTest
    {
        [TestMethod]
        public void MovingTest()
        {
            var cl = new CircleArray<int>(new int[] { 0, 1, 2, 3, 4, 5, 6 });

            Assert.AreEqual(7, cl.Length);

            for (var i = 0; i <= 19; ++i) // here we has stopped on 19 in order to test reset
            {
                Assert.AreEqual(i % 7, cl.Current);
                cl.MoveForward();
            }

            cl.Reset();

            for (var i = 21; i >= 0; --i)
            {
                Assert.AreEqual(i % 7, cl.Current);
                cl.MoveBack();
            }
        }

    } // public class CircularListTest
}     // namespace Regata.Tests.Collections 