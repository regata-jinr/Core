using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Regata.Utilities.Tests
{
    [TestClass]
    public class ExportDataTest
    {
        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public async Task EmptyLinkParsingTest()
        {
            var data = await ExportData.FromGoogleSheet<SamplesSetModel>("", new System.Threading.CancellationToken());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task WrongLinkParsingTest()
        {
            var data = await ExportData.FromGoogleSheet<SamplesSetModel>("https://regata.jinr.ru", new System.Threading.CancellationToken());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task LinkWoSharedAccessParsingTest()
        {
            var data = await ExportData.FromGoogleSheet<SamplesSetModel>("https://docs.google.com/spreadsheets/d/1c7c-mlEbANl0Fszxj1ITaTwexJb-Z9KbAWChX_1aCk8/edit#gid=0", new System.Threading.CancellationToken());
        }

      
        [TestMethod]
        public void ExportedDataTest()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(TaskCanceledException))]
        public async Task CancellingTest()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(10);
            var data = await ExportData.FromGoogleSheet<SamplesSetModel>("https://docs.google.com/spreadsheets/d/1c7c-mlEbANl0Fszxj1ITaTwexJb-Z9KbAWChX_1aCk8/edit#gid=0", cts.Token);
        }
    }
}
