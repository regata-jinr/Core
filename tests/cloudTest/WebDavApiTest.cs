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
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Regata.Core.Cloud;
using Regata.Core;

namespace Regata.Tests.Cloud
{
    [TestClass]
    public class WebDavApiTest : IDisposable
    {
        public const string mainDir = @"D:\WebDavApiTest";
        public const string mainDir_Downloaded = @"D:\WebDavApiTest_Downloaded";

        public readonly string NestedDir1_1 = Path.Combine(mainDir, DateTime.Now.Year.ToString(), "01");
        public readonly string NestedDir1_2 = Path.Combine(mainDir, DateTime.Now.Year.ToString(), "11");

        public readonly string NestedDir2_1 = Path.Combine(mainDir, (DateTime.Now.Year - 1).ToString(), "01");
        public readonly string NestedDir2_2 = Path.Combine(mainDir, (DateTime.Now.Year - 1).ToString(), "11");

        private static CancellationTokenSource _cts = new CancellationTokenSource();

        public WebDavApiTest()
        {
            Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "WebDavApiTest");

            if (Directory.Exists(mainDir))
                Directory.Delete(mainDir, true);

            Directory.CreateDirectory(NestedDir1_1);
            Directory.CreateDirectory(NestedDir1_2);
            Directory.CreateDirectory(NestedDir2_1);
            Directory.CreateDirectory(NestedDir2_2);

            foreach (var f in Directory.GetFiles(@"D:\Spectra\2020", "*.cnf", SearchOption.AllDirectories).Take(5).ToArray())
            {
                File.Copy(f, Path.Combine(NestedDir1_1, Path.GetFileName(f)));
                File.Copy(f, Path.Combine(NestedDir1_2, Path.GetFileName(f)));
                File.Copy(f, Path.Combine(NestedDir2_1, Path.GetFileName(f)));
                File.Copy(f, Path.Combine(NestedDir2_2, Path.GetFileName(f)));
            }

            if (WebDavClientApi.IsExistsAsync(mainDir, _cts.Token).Result)
                WebDavClientApi.RemoveFileAsync(mainDir, _cts.Token).Wait();

        }

        public void Dispose()
        {
            if (Directory.Exists(mainDir))
                Directory.Delete(mainDir, true);

            if (Directory.Exists(mainDir_Downloaded))
                Directory.Delete(mainDir_Downloaded, true);

            if (WebDavClientApi.IsExistsAsync(mainDir, _cts.Token).Result)
                WebDavClientApi.RemoveFileAsync(mainDir, _cts.Token).Wait();
        }

        [TestMethod]
        public async Task CreateAndRemoveFolderTest()
        {
            await WebDavClientApi.CreateFolderAsync(NestedDir1_1, _cts.Token);
            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(NestedDir1_1, _cts.Token));
            
            await WebDavClientApi.CreateFolderAsync(NestedDir1_2, _cts.Token);
            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(NestedDir1_2, _cts.Token));

            await WebDavClientApi.CreateFolderAsync(NestedDir2_1, _cts.Token);
            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(NestedDir2_1, _cts.Token));

            await WebDavClientApi.CreateFolderAsync(NestedDir2_1, _cts.Token);
            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(NestedDir2_1, _cts.Token));

            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(mainDir, _cts.Token));
            Assert.IsTrue(await WebDavClientApi.RemoveFileAsync(mainDir, _cts.Token));
            Assert.IsFalse(await WebDavClientApi.IsExistsAsync(mainDir, _cts.Token));
        }

        [TestMethod]
        public async Task UploadAndDownloadFileTest()
        {
            var tokens = new Dictionary<string,string>();
            var files = Directory.GetFiles(mainDir, "*.cnf", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                Assert.IsTrue(await WebDavClientApi.UploadFileAsync(f, _cts.Token));
                Assert.IsTrue(await WebDavClientApi.IsExistsAsync(f, _cts.Token));
                tokens[await WebDavClientApi.MakeShareableAsync(f, _cts.Token)] = f;
            }

            Assert.AreEqual(files.Length, tokens.Count);

            foreach (var t in tokens.Keys)
            {
                await WebDavClientApi.DownloadFileAsync(t, tokens[t].Replace("WebDavApiTest", "WebDavApiTest_Downloaded"), _cts.Token);
                Assert.IsTrue(File.Exists(tokens[t].Replace("WebDavApiTest", "WebDavApiTest_Downloaded")));
            }

            Assert.IsTrue(await WebDavClientApi.IsExistsAsync(mainDir, _cts.Token));
            Assert.IsTrue(await WebDavClientApi.RemoveFileAsync(mainDir, _cts.Token));
            Assert.IsFalse(await WebDavClientApi.IsExistsAsync(mainDir, _cts.Token));

            Assert.AreEqual(files.Length, Directory.GetFiles(mainDir_Downloaded, "*.cnf", SearchOption.AllDirectories).Length);


            Assert.IsTrue(Directory.Exists(mainDir_Downloaded));
            Directory.Delete(mainDir_Downloaded,true);
            Assert.IsFalse(Directory.Exists(mainDir_Downloaded));
        }

        [TestMethod]
        public async Task UploadNonExistedFileTest()
        {
            Assert.IsFalse(await WebDavClientApi.UploadFileAsync(Path.Combine(mainDir, "3110779.cnf"), _cts.Token));
        }

    } // public class WebDavApiTest : IDisposable
}     // namespace Regata.Tests.Cloud.WebDavApi
