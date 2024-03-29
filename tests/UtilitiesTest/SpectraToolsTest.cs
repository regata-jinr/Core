﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Regata.Utilities.Tests
{
    [TestClass]
    public class SpectraToolsTest
    {
        private static CancellationTokenSource _cts = new CancellationTokenSource();

        private const string cons = @"Data Source=RUMLAB\REGATALOCAL;Initial Catalog=NAA_DB_TEST;Integrated Security=True;";
        [TestMethod]
        public async Task DownloadFileByNameTest()
        {
            //var spctr = "1107816";
            var spctr = "1006019";
            Settings.ConnectionString = cons;
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
            await SpectraTools.DownloadSpectraAsync($"{spctr}", $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", _cts.Token);
            Assert.IsTrue(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
            File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf");
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{spctr}.cnf"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public async Task DownloadNonExistedFileTest()
        {
            var spctr = "11078166";
            Settings.ConnectionString = cons;
            await SpectraTools.DownloadSpectraAsync($"{spctr}", $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", _cts.Token);
        }

        [TestMethod]
        public async Task SLIFilesListTest()
        {
            var SetKey = "VN-02-18-56-d";
            Settings.ConnectionString = cons;
            await foreach (var ssi in SpectraTools.SLISetSpectrasAsync(SetKey, _cts.Token))
                Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SetKey, "SLI", ssi.SampleType, $"{ssi.SampleSpectra}.cnf")));
        }

        [TestMethod]
        public async Task LLI1FilesListTest()
        {
            var SetKey = "VN-02-18-56-d";
            Settings.ConnectionString = cons;
            await foreach (var ssi in SpectraTools.LLISetSpectrasAsync(SetKey, IrradiationType.LLI1, _cts.Token))
                Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SetKey, SpectraTools.IrradiationTypeMap[IrradiationType.LLI1],ssi.LoadNumber.ToString(),$"c-{ssi.Container}",ssi.SampleType,$"{ssi.SampleSpectra}.cnf")));
        }

        [TestMethod]
        public async Task LLI2FilesListTest()
        {
            var SetKey = "VN-02-18-56-d";
            Settings.ConnectionString = cons;
            await foreach (var ssi in SpectraTools.LLISetSpectrasAsync(SetKey, IrradiationType.LLI2, _cts.Token))
                Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SetKey, SpectraTools.IrradiationTypeMap[IrradiationType.LLI2], ssi.LoadNumber.ToString(), $"c-{ssi.Container}", ssi.SampleType, $"{ssi.SampleSpectra}.cnf")));
        }

    }
}
