using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Regata.Utilities.Tests
{
    [TestClass]
    public class WebDavApiTest
    {
        [TestMethod]
        public async Task UploadFileTest()
        {
            await WebDavClientApi.UploadFile(@"D:\Spectra\2020\01\dji-1\1107798.cnf");
           Assert.IsTrue(await WebDavClientApi.IsFolderExists(@"D:\Spectra\2020\01\dji-1\1107798.cnf"));

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
            Assert.AreEqual("f3TrsxcJqfptET3", await WebDavClientApi.MakeShareable(@"D:\Spectra\2020\01\dji-1\1107798.cnf"));
        }

    }
}
