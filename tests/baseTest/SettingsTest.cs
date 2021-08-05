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
using Regata.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Regata.Tests.Base
{
    public enum TestEnum { One, Two, Three }

    public class DetectorSettings
    {
        public int       count_time  { get; set; }  = 10;
        public float     height      { get; set; } = 1;
        public string    name        { get; set; } = "D1";
        public bool      iswrite     { get; set; } = true;
        public TestEnum  iset5       { get; set; } = TestEnum.One;
        public List<int> iset6       { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
    }

    public class AppSettings : ASettings, IEquatable<AppSettings>
    {

        public float                  width                { get; set; } = 0.1f;
        public float                  height               { get; set; } = 1;
        public string                 name                 { get; set; } = "TestApp";
        public bool                   showTitle            { get; set; } = true;
        public TestEnum               iset5                { get; set; } = TestEnum.One;
        public List<int>              iset6                { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        public List<DetectorSettings> detectors            { get; set; } = new List<DetectorSettings>() { new DetectorSettings() };

        public bool Equals(AppSettings rhv)
        {
            var comparisonList = new List<bool>();

            comparisonList.Add(width            == rhv.width);
            comparisonList.Add(height           == rhv.height);
            comparisonList.Add(name             == rhv.name);
            comparisonList.Add(showTitle        == rhv.showTitle);
            comparisonList.Add(iset5            == rhv.iset5);
         // comparisonList.Add(set6 iset6     == rhv.set6);
            comparisonList.Add(detectors[0].count_time  == rhv.detectors[0].count_time);
            comparisonList.Add(detectors[0].height      == rhv.detectors[0].height);
            comparisonList.Add(detectors[0].name        == rhv.detectors[0].name);
            comparisonList.Add(detectors[0].iswrite     == rhv.detectors[0].iswrite);
            comparisonList.Add(detectors[0].iset5       == rhv.detectors[0].iset5);
            //comparisonList.Add(set7.iset6     == rhv.set7.iset6);

            return comparisonList.All(b => b);

        }

        public override bool Equals(Object rho)
        {
            if (rho == null)
                return false;

            var casted_rho = rho as AppSettings;
            if (casted_rho == null)
                return false;
            else
                return Equals(casted_rho);
        }


        public static bool operator == (AppSettings lhv, AppSettings rhv)
        {
            if (((object)lhv) == null || ((object)rhv) == null)
                return Equals(lhv, rhv);

            return lhv.Equals(rhv);
        }

        public static bool operator !=(AppSettings lhv, AppSettings rhv)
        {
            if (((object)lhv) == null || ((object)rhv) == null)
                return !Equals(lhv, rhv);

            return !lhv.Equals(rhv);
        }

       
    }

    [TestClass]
    public class SettingsTest
    {
        public AppSettings dfltSettings = new AppSettings();

        public SettingsTest()
        {
            Settings<AppSettings>.AssemblyName = "TestSettings";

        }

        [TestMethod]
        public void CreateSettingsFromScratchTest()
        {

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Regata", "TestSettings", "settings.json")))
                File.Delete(Settings<AppSettings>.FilePath);
            
            Settings<AppSettings>.AssemblyName = "TestSettings";

            Assert.AreEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Assert.IsTrue(File.Exists(Settings<AppSettings>.FilePath));
        }

        [TestMethod]
        public void ChangeAndSaveTest()
        {
            Settings<AppSettings>.CurrentSettings.height = 2.5f;
            Settings<AppSettings>.Save();
            Assert.IsTrue(File.Exists(Settings<AppSettings>.FilePath));
            Assert.AreNotEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Settings<AppSettings>.CurrentSettings.height = 1f;
            Assert.AreEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Settings<AppSettings>.Load();
            Assert.AreNotEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Assert.AreEqual(2.5f, Settings<AppSettings>.CurrentSettings.height);

        }

        [TestMethod]
        public void ResetToDefaultsTest()
        {
            Settings<AppSettings>.CurrentSettings.height = 222f;
            Assert.AreNotEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Settings<AppSettings>.ResetToDefaults();
            Assert.AreEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Settings<AppSettings>.Load();
            Assert.AreEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
        }

        [TestMethod]
        public void SyncWithGlobalSettingsTest()
        {
            Settings<AppSettings>.ResetToDefaults();

            Assert.AreEqual(Settings<AppSettings>.CurrentSettings, dfltSettings);
            Assert.AreEqual(Language.Russian, GlobalSettings.CurrentLanguage);

            Settings<AppSettings>.CurrentSettings.CurrentLanguage = Language.English;
            
            Assert.AreNotEqual(Language.Russian, Settings<AppSettings>.CurrentSettings.CurrentLanguage);
            Assert.AreNotEqual(Language.Russian, GlobalSettings.CurrentLanguage);

            Settings<AppSettings>.Save();

            Settings<AppSettings>.CurrentSettings.CurrentLanguage = Language.Russian;
            Assert.AreEqual(Language.Russian, Settings<AppSettings>.CurrentSettings.CurrentLanguage);
            Assert.AreEqual(Language.Russian, GlobalSettings.CurrentLanguage);

            Settings<AppSettings>.Load();
            Assert.AreEqual(Language.English, Settings<AppSettings>.CurrentSettings.CurrentLanguage);
            Assert.AreEqual(Language.English, GlobalSettings.CurrentLanguage);


        }

    } // public class SettingsTest
}     // namespace Regata.Tests.Base.Settings
