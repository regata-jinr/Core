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
using Regata.Core.DataBase.Models;

namespace Regata.Tests.Base.Data
{
    [TestClass]
    public class WeightsTest
    {
        [TestMethod]
        public void WrongSampleTest()
        {
            // "ZZZ-001-180-390-ma-002" - non existed wrong data sample

            Sample s = Core.Data.GetISample<Sample>("ZZZ-001-180-390-ma-002");
            Irradiation i = Core.Data.GetISample<Irradiation>("ZZZ-001-180-390-ma-002");
            Measurement m = Core.Data.GetISample<Measurement>("ZZZ-001-180-390-ma-002");

            WeightIsNull(s, i, m);
        }

        [TestMethod]
        public void NonExistedSampleTest()
        {
            // "ZZ-01-18-39-m-02" - non existed sample

            Sample s = Core.Data.GetISample<Sample>("ZZ-01-18-39-m-02");
            Irradiation i = Core.Data.GetISample<Irradiation>("ZZ-01-18-39-m-02");
            Measurement m = Core.Data.GetISample<Measurement>("ZZ-01-18-39-m-02");

            WeightIsNull(s, i, m);
        }

        [TestMethod]
        public void AllWeightsAreNullTest()
        {
            // "AZ-01-18-39-m-02" - all weights are null


            Sample s = Core.Data.GetISample<Sample>("AZ-01-18-39-m-02");
            Irradiation i = Core.Data.GetISample<Irradiation>("AZ-01-18-39-m-02");
            Measurement m = Core.Data.GetISample<Measurement>("AZ-01-18-39-m-02");

            WeightIsNull(s, i, m);
        }

        [TestMethod]
        public void OnlySLIIsNotNullTest()
        {
            // "VN-03-13-29-c-25" - rw null samples sli not null

            Sample s = Core.Data.GetISample<Sample>("VN-03-13-29-c-25");
            Irradiation i = Core.Data.GetISample<Irradiation>("VN-03-13-29-c-25");
            Measurement m = Core.Data.GetISample<Measurement>("VN-03-13-29-c-25");

            OnlySLIWeight(s, i, m);
        }

        [TestMethod]
        public void OnlyLLIIsNotNullTest()
        {
            // "RU-14-17-15-o-01" - rw null samples lli not null

            Sample s = Core.Data.GetISample<Sample>("RU-14-17-15-o-01");
            Irradiation i = Core.Data.GetISample<Irradiation>("RU-14-17-15-o-01");
            Measurement m = Core.Data.GetISample<Measurement>("RU-14-17-15-o-01");

            OnlySampleLLIWeight(s, i, m);
        }

        [TestMethod]
        public void ReWeightsAreNullTest()
        {
            // "BG-01-15-61-i-01" - rw null samples wghts not null

            Sample s = Core.Data.GetISample<Sample>("BG-01-15-61-i-01");
            Irradiation i = Core.Data.GetISample<Irradiation>("BG-01-15-61-i-01");
            Measurement m = Core.Data.GetISample<Measurement>("BG-01-15-61-i-01");

            OnlySamplesWeight(s, i, m);
        }

        [TestMethod]
        public void OnlyReweightIsNullTest()
        {
            // "AZ-01-19-87-i-01" - all not null except ARepack

            Sample s = Core.Data.GetISample<Sample>("AZ-01-19-87-i-01");
            Irradiation i = Core.Data.GetISample<Irradiation>("AZ-01-19-87-i-01");
            Measurement m = Core.Data.GetISample<Measurement>("AZ-01-19-87-i-01");

            OnlyReweightIsNull(s, i, m);
        }

        [TestMethod]
        public void AllWeightsAreNotNullTest()
        {
            // "RS-02-21-34-h-01" - all not null

            Sample s = Core.Data.GetISample<Sample>("RS-02-21-34-h-01");
            Irradiation i = Core.Data.GetISample<Irradiation>("RS-02-21-34-h-01");
            Measurement m = Core.Data.GetISample<Measurement>("RS-02-21-34-h-01");

            AllNotNull(s, i, m);
        }


        [TestMethod]
        public void GetConcreteWeightValuesSampleTest()
        {
            // "RU-53-20-31-e-10" loadNumber 234
            // initWeight 0.1041
            // repack     0.098400116
            // sli       0.1041

            Sample s = Core.Data.GetISample<Sample>("RU-53-20-31-e-10");
            Irradiation i = Core.Data.GetISample<Irradiation>("RU-53-20-31-e-10");
            Measurement m = Core.Data.GetISample<Measurement>("RU-53-20-31-e-10");

            CheckConcreteValues(s, i, m, 0.1041f, 0.1041f, 0.098400116f, 0.02f);

        }

        [TestMethod]
        public void GetConcreteWeightValuesSRMTest()
        {
            // "s-s-s-OBTL5-01-08"
            // lli       0.2032
            // sli       0.2174

            Standard s = Core.Data.GetSrmOrMonitor<Standard>("s-s-s-OBTL5-01-08");

            Assert.AreEqual(0.2174f, s.SLIWeight.Value, 0.02f);
            Assert.AreEqual(0.2032f, s.LLIWeight.Value, 0.02f);

        }

        [TestMethod]
        public void GetConcreteWeightValuesMonitorTest()
        {
            // "m-m-m-Zr-05-01"
            // lli       0.0056
            // sli       0.0176

            Monitor m = Core.Data.GetSrmOrMonitor<Monitor>("m-m-m-Zr-05-01");

            Assert.AreEqual(0.0176f, m.SLIWeight.Value, 0.02f);
            Assert.AreEqual(0.0056f, m.LLIWeight.Value, 0.02f);

        }

        private void WeightIsNull(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void OnlySLIWeight(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void OnlySampleLLIWeight(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void OnlySamplesWeight(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void OnlyReweightIsNull(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void AllNotNull(Sample s, Irradiation i, Measurement m)
        {
            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(s));
            Assert.IsNotNull(Core.Data.GetSampleLLIReWeight(s));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(i));
            Assert.IsNotNull(Core.Data.GetSampleLLIReWeight(i));

            Assert.IsNotNull(Core.Data.GetSampleLLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleSLIWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleLLIInitWeight(m));
            Assert.IsNotNull(Core.Data.GetSampleLLIReWeight(m));

        }

        private void CheckConcreteValues(Sample s, Irradiation i, Measurement m, float sli, float lli, float rep, float delta)
        {
            Assert.AreEqual(sli, Core.Data.GetSampleSLIWeight(s).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIWeight(s).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIInitWeight(s).Value, delta);
            Assert.AreEqual(rep, Core.Data.GetSampleLLIReWeight(s).Value, delta);

            Assert.AreEqual(sli, Core.Data.GetSampleSLIWeight(i).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIWeight(i).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIInitWeight(i).Value, delta);
            Assert.AreEqual(rep, Core.Data.GetSampleLLIReWeight(i).Value, delta);

            Assert.AreEqual(sli, Core.Data.GetSampleSLIWeight(m).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIWeight(m).Value, delta);
            Assert.AreEqual(lli, Core.Data.GetSampleLLIInitWeight(m).Value, delta);
            Assert.AreEqual(rep, Core.Data.GetSampleLLIReWeight(m).Value, delta);
        }

    } // public class DataTest
}     // namespace Regata.Tests.Base.Data
