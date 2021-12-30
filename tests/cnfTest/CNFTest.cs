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
using Regata.Core.CNF;
using System;

namespace Regata.Tests.CNF
{
    [TestClass]
    public class CNFTest
    {
        [TestMethod]
        public void TestCNFParsing()
        {
            // File	SampleSet	SampleId	Operator	Type	Weight	Error	Units	Height	Duration, s	Dead time	Build up type	Irradiation begin date	Irradiation end date	Description	Read successfuly	Error message
            // 1006379 RU - 33 - 19 - 88 - j   j - 01        SLI - 2   0.2053  0   gram    20  900 0.21    IRRAD   19.03.2020 9:50 19.03.2020 9:53 Vergel_K.N.Journal_18 True

            var spectra = new Spectra(@"D:\Spectra\2020\03\kji\1006379");

            Assert.AreEqual("1006379", spectra.SpectrumData.File);
            Assert.AreEqual("RU-33-19-88-j", spectra.SpectrumData.Id);
            Assert.AreEqual("j-01", spectra.SpectrumData.Title);
            Assert.IsTrue(string.IsNullOrEmpty(spectra.SpectrumData.CollectorName));
            Assert.AreEqual("SLI-2", spectra.SpectrumData.Type);
            Assert.AreEqual(0.2053, spectra.SpectrumData.Quantity, 2);
            Assert.AreEqual(0, spectra.SpectrumData.Uncertainty, 2);
            Assert.AreEqual("gram", spectra.SpectrumData.Units);
            Assert.AreEqual(20, spectra.SpectrumData.Geometry, 2);
            Assert.AreEqual(900, spectra.SpectrumData.Duration, 2);
            Assert.AreEqual(0.21, (double)spectra.SpectrumData.DeadTime, 2);
            Assert.AreEqual("IRRAD", spectra.SpectrumData.BuildUpType);
            //Assert.AreEqual(DateTime.Parse("19.03.2020 9:50"), spectra.SpectrumData.IrrBeginDate, TimeSpan.FromMinutes(1));
            //Assert.AreEqual(DateTime.Parse("19.03.2020 9:53"), spectra.SpectrumData.IrrEndDate, TimeSpan.FromMinutes(1));
            //Assert.AreEqual(DateTime.Parse("19.03.2020 9:58"), spectra.SpectrumData.AcqStartDate, TimeSpan.FromMinutes(1));
            Assert.AreEqual("Vergel_K.N.Journal_18", spectra.SpectrumData.Description);
            Assert.IsTrue(spectra.ReadSuccess);
            Assert.IsTrue(string.IsNullOrEmpty(spectra.ErrorMessage));
        }
    } // public class CodesTest
}     // namespace Regata.Tests.Base.Reports
