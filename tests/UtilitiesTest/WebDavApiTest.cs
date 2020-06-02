using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

namespace Regata.Utilities.Tests
{
    [TestClass]
    public class WebDavApiTest
    {
        [TestMethod]
        public async Task UploadFileTest()
        {
           Assert.IsTrue(await WebDavClientApi.UploadFile(@"D:\Spectra\2020\01\dji-1\1107815.cnf"));
           Assert.IsTrue(await WebDavClientApi.IsFolderExists(@"D:\Spectra\2020\01\dji-1\1107815.cnf"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public async Task UploadNonExistedFileTest()
        {
            await WebDavClientApi.UploadFile(@"D:\Spectra\2020\01\dji-1\110779.cnf");
        }

        [TestMethod]
        public async Task CreateFolderTest()
        {
           await WebDavClientApi.CreateFolder(@"D:\Spectra\2020\01\dji-1");
           Assert.IsTrue(await WebDavClientApi.IsFolderExists(@"D:\Spectra\2020\01\dji-1"));
        }

        [TestMethod]
        public async Task IsFolderExistsTest()
        {
           Assert.IsTrue(await WebDavClientApi.IsFolderExists(@"D:\Spectra\2020\01\dji-1"));
           Assert.IsFalse(await WebDavClientApi.IsFolderExists(@"D:\Spectra\2020\01\dji-11"));
        }

        [TestMethod]
        public async Task MakeFileSharedTest()
        {
            Assert.AreEqual("qb7cqjj2LcaQpNW", await WebDavClientApi.MakeShareable(@"D:\Spectra\2020\01\dji-1\1107798.cnf"));
        }

        [TestMethod]
        public async Task DownloadFileTest()
        {
            await WebDavClientApi.DownloadFile("qb7cqjj2LcaQpNW", $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf");
            Assert.IsTrue(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf"));
            File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf");
            Assert.IsFalse(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\1107798.cnf"));
        }

    }
}
