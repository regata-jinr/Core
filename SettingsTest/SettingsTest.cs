using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SettingsTest
{

    public enum TestEnum { One, Two, Three }

    public class InternalSettings
    {
        public float     iset1 { get; set; }  = 0.1f;
        public int       iset2 { get; set; } = -1;
        public string    iset3 { get; set; } = "DefStr";
        public bool      iset4 { get; set; } = true;
        public TestEnum  iset5 { get; set; } = TestEnum.One;
        public List<int> iset6 { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
    }

    public class TestSettings : IEquatable<TestSettings>
    {
        public float            set1 { get; set; } = 0.1f;
        public int              set2 { get; set; } = -1;
        public string           set3 { get; set; } = "DefStr";
        public bool             set4 { get; set; } = true;
        public TestEnum         set5 { get; set; } = TestEnum.One;
        public List<int>        set6 { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        public InternalSettings set7 { get; set; } = new InternalSettings();

        public bool Equals(TestSettings rhv)
        {
            //var ts = typeof(TestSettings);
            //var its = typeof(TestSettings);
            //bool res = true;
            //foreach (var p in rhv.GetType().GetProperties())
            //{
            //    if (p.PropertyType == its)
            //    {
            //        foreach (var ip in its.GetProperties())
            //        {
            //            var v1 = its.GetProperty(ip.Name).GetValue(rhv.set7);
            //            var v2 = its.GetProperty(ip.Name).GetValue(set7);
            //            res = res && (v1 == v2);
            //        }
            //    }
            //    else
            //    {
            //        var v1 = ts.GetProperty(p.Name).GetValue(rhv);
            //        var v2 = ts.GetProperty(p.Name).GetValue(this);
            //        res = res && p.PropertyType.Equals.Equals(v1,v2);
            //    }
            //}

            //return res;

            var comparisonList = new List<bool>();

            comparisonList.Add(set1 == rhv.set1);
            comparisonList.Add(set2 == rhv.set2);
            comparisonList.Add(set3 == rhv.set3);
            comparisonList.Add(set4 == rhv.set4);
            comparisonList.Add(set5 == rhv.set5);
            //comparisonList.Add(set6 == rhv.set6);
            comparisonList.Add(set7.iset1 == rhv.set7.iset1);
            comparisonList.Add(set7.iset2 == rhv.set7.iset2);
            comparisonList.Add(set7.iset3 == rhv.set7.iset3);
            comparisonList.Add(set7.iset4 == rhv.set7.iset4);
            comparisonList.Add(set7.iset5 == rhv.set7.iset5);
            //comparisonList.Add(set7.iset6 == rhv.set7.iset6);

            return comparisonList.All(b => b);


        }

        public override bool Equals(Object rho)
        {
            if (rho == null)
                return false;

            var casted_rho = rho as TestSettings;
            if (casted_rho == null)
                return false;
            else
                return Equals(casted_rho);
        }


        public static bool operator == (TestSettings lhv, TestSettings rhv)
        {
            if (((object)lhv) == null || ((object)rhv) == null)
                return Equals(lhv, rhv);

            return lhv.Equals(rhv);
        }

        public static bool operator !=(TestSettings lhv, TestSettings rhv)
        {
            if (((object)lhv) == null || ((object)rhv) == null)
                return !Equals(lhv, rhv);

            return !lhv.Equals(rhv);
        }
    }

    [TestClass]
    public class SettingsTest
    {
        public TestSettings dfltSettings = new TestSettings();

        [TestMethod]
        public void CreateSettingsFromScratchTest()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Regata", "TestSettings", "settings.json")))
                File.Delete(Settings<TestSettings>.FilePath);

            Settings<TestSettings>.AssemblyName = "TestSettings";
            Assert.AreEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Assert.IsTrue(File.Exists(Settings<TestSettings>.FilePath));
        }

        [TestMethod]
        public void ChangeAndSaveTest()
        {
            Settings<TestSettings>.AssemblyName = "TestSettings";
            Settings<TestSettings>.CurrentSettings.set2 = 22;
            Settings<TestSettings>.Save();
            Assert.IsTrue(File.Exists(Settings<TestSettings>.FilePath));
            Assert.AreNotEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Settings<TestSettings>.CurrentSettings.set2 = -1;
            Assert.AreEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Settings<TestSettings>.Load();
            Assert.AreNotEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Assert.AreEqual(22, Settings<TestSettings>.CurrentSettings.set2);

        }

        [TestMethod]
        public void ResetToDefaultsTest()
        {
            Settings<TestSettings>.AssemblyName = "TestSettings";
            Settings<TestSettings>.CurrentSettings.set2 = 222;
            Assert.AreNotEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Settings<TestSettings>.ResetToDefaults();
            Assert.AreEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
            Settings<TestSettings>.Load();
            Assert.AreEqual(Settings<TestSettings>.CurrentSettings, dfltSettings);
        }

    }
}
