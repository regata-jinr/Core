using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Xml.Linq;

namespace Regata.Utilities
{
    public static class WebDavClientApi
    {
        private static WebClient _webClient;
        private static HttpClient _httpClient;
        private const string _hostBase = @"https://disk.jinr.ru";
        private const string _hostWebDavAPI = @"/remote.php/dav/files/regata";
        private const string _hostOCSApi = @"/ocs/v2.php/apps/files_sharing/api/v1/shares";

        public static string GetDownloadLink(string sharedKey) => $"https://disk.jinr.ru/index.php/s/f3TrsxcJqfptET3/{sharedKey}/download";

        private static CancellationTokenSource _cts;

        public static void Cancel()
        {
            _webClient.CancelAsync();
            _httpClient.CancelPendingRequests();
            _cts?.Cancel();
        }

        static WebDavClientApi()
        {
            var c = new ConfigManager();
            _webClient = new WebClient();
            _webClient.Credentials = new NetworkCredential(c.WebDavApiCredentials.Split(':')[0], c.WebDavApiCredentials.Split(':')[1]);

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(c.WebDavApiCredentials)));
            _httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
            
        }

        public static async Task UploadFile(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException($"File '{path}' doesn't exist");
            await CreateFolder(Path.GetDirectoryName(path));
            await _webClient.UploadDataTaskAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", "PUT", File.ReadAllBytes(path));
        }

        public static async Task<string> MakeShareable(string file)
        {
            try
            {
                _cts = new CancellationTokenSource();
                var respons = await _httpClient.PostAsync($"{_hostBase}{_hostOCSApi}?path={file.Substring(Path.GetPathRoot(file).Length)}&shareType=3&permissions=3&name={Path.GetFileNameWithoutExtension(file)}", null, _cts.Token);
                var content = await respons.Content.ReadAsStringAsync();

                var xe = XElement.Parse(content);
                return xe.Descendants("token").First().Value;
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
            }
        }

        public static async Task<bool> IsFolderExists(string path)
        {
            try
            {
                await _webClient.UploadStringTaskAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", "PROPFIND", "");
                return true;
            }
            catch (WebException wex)
            {
                if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    return false;
                else
                    throw wex;
            }
        }

        public static async Task CreateFolder(string path)
        {
            var dir = path.Substring(Path.GetPathRoot(path).Length);
            var subPath = "";

            foreach (var node in dir.Split(Path.DirectorySeparatorChar))
            {
                subPath = $"{subPath}/{node}";
                if (!await IsFolderExists(subPath))
                    await _webClient.UploadStringTaskAsync($"{_hostBase}{_hostWebDavAPI}{subPath}", "MKCOL", "");
            }

        }

        public static async Task DownloadFile(string path)
        {
            throw new NotImplementedException();
        }

        public static async Task<string> SearchFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
