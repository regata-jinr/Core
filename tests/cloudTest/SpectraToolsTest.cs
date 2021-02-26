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
using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Regata.Core.Cloud;
using Regata.Core;

namespace Regata.Tests.Cloud
{
    [TestClass]
    public class SpectraToolsTest
    {

        public SpectraToolsTest()
        {
            Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "SpectraToolsTest");
        }

        private static CancellationTokenSource _cts = new CancellationTokenSource();

        [TestMethod]
        public async Task DownloadFileByNameTest()
        {
            //var spctr = "1107816";
            var spctr = "1006019";
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
            await SpectraTools.DownloadSpectraAsync(spctr, $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", _cts.Token);
            Assert.IsTrue(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
            File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf");
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
        }

        [TestMethod]
        public async Task DownloadNonExistedFileTest()
        {
            var spctr = "11078166";
            await SpectraTools.DownloadSpectraAsync(spctr, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _cts.Token);
            Assert.IsFalse(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),$"{spctr}.cnf")));

        }

        [TestMethod]
        public async Task SLIFilesListTest()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "SLIFilesListTest");

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);

            Directory.CreateDirectory(dir);
            var SetKey = "VN-02-18-56-d";
            await foreach (var ssi in SpectraTools.GetSLISpectraForSampleSetAsync(SetKey, _cts.Token))
            {
                await SpectraTools.DownloadSpectraAsync(ssi.SampleSpectra, dir, _cts.Token);
            }
            Assert.AreEqual(77, Directory.GetFiles(dir).Length);

            Directory.Delete(dir, true);
        }

        [TestMethod]
        public async Task LLI1FilesListTest()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "LLI1FilesListTest");

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);

            Directory.CreateDirectory(dir);
            var SetKey = "VN-02-18-56-d";
            await foreach (var ssi in SpectraTools.GetLLISpectraForSampleSetAsync(SetKey, IrradiationType.LLI1, _cts.Token))
            {
                await SpectraTools.DownloadSpectraAsync(ssi.SampleSpectra, dir, _cts.Token);
            }
            Assert.AreEqual(70, Directory.GetFiles(dir).Length);

            Directory.Delete(dir, true);
        }

        [TestMethod]
        public async Task LLI2FilesListTest()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "LLI2FilesListTest");

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);

            Directory.CreateDirectory(dir);
            var SetKey = "VN-02-18-56-d";
            await foreach (var ssi in SpectraTools.GetLLISpectraForSampleSetAsync(SetKey, IrradiationType.LLI2, _cts.Token))
            {
                await SpectraTools.DownloadSpectraAsync(ssi.SampleSpectra, dir, _cts.Token);
            }
            Assert.AreEqual(70, Directory.GetFiles(dir).Length);

            Directory.Delete(dir, true);
        }

    } // public class SpectraToolsTest
}     // namespace Regata.Tests.Cloud
