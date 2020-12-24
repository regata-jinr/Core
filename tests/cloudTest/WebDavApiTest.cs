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
using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Regata.Core.Cloud;
using Regata.Core;

namespace Test
{
    [TestClass]
    public class WebDavApiTest
    {

        public WebDavApiTest()
        {
            Report.LogConnectionStringTarget = "MeasurementsLogConnectionString";
            Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "WebDavApiTest");

            WebDavClientApi.DiskJinrTarget = "MeasurementsDiskJinr";
        }

        private static CancellationTokenSource _cts = new CancellationTokenSource();
        [TestMethod]
        public async Task UploadFileTest()
        {
           Assert.IsTrue(await WebDavClientApi.UploadFileAsync(@"D:\Spectra\2020\01\dji-1\1107815.cnf", _cts.Token));
           Assert.IsTrue(await WebDavClientApi.IsExistsAsync(@"D:\Spectra\2020\01\dji-1\1107815.cnf", _cts.Token));
        }

        [TestMethod]
        public async Task UploadNonExistedFileTest()
        {
            Assert.IsFalse(await WebDavClientApi.UploadFileAsync(@"D:\Spectra\2020\01\dji-1\110779.cnf", _cts.Token));
        }

        [TestMethod]
        public async Task CreateFolderTest()
        {
           await WebDavClientApi.CreateFolderAsync(@"D:\Spectra\2020\01\dji-1", _cts.Token);
           Assert.IsTrue(await WebDavClientApi.IsExistsAsync(@"D:\Spectra\2020\01\dji-1", _cts.Token));
        }

        [TestMethod]
        public async Task IsExistsAsyncTest()
        {
           Assert.IsTrue(await WebDavClientApi.IsExistsAsync(@"D:\Spectra\2020\01\dji-1", _cts.Token));
           Assert.IsFalse(await WebDavClientApi.IsExistsAsync(@"D:\Spectra\2020\01\dji-11", _cts.Token));
        }

        [TestMethod]
        public async Task MakeFileSharedTest()
        {
            Assert.AreEqual("qb7cqjj2LcaQpNW", await WebDavClientApi.MakeShareableAsync(@"D:\Spectra\2020\01\dji-1\1107798.cnf", _cts.Token));
        }

        [TestMethod]
        public async Task DownloadFileTest()
        {
            await WebDavClientApi.DownloadFileAsync("qb7cqjj2LcaQpNW", $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf", _cts.Token);
            Assert.IsTrue(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf"));
            File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf");
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf"));
        }

    }
}
