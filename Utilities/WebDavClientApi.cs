using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Regata.Utilities
{
    // TODO: add cancellation exception
    // TODO: generalize output parsing: repeat task in case of response e.g. 497 - to many requests

    public static class WebDavClientApi
    {
        private static HttpClient _httpClient;
        private const string _hostBase = @"https://disk.jinr.ru";
        private const string _hostWebDavAPI = @"/remote.php/dav/files/regata";
        private const string _hostOCSApi = @"/ocs/v2.php/apps/files_sharing/api/v1/shares";

        public static string GetDownloadLink(string sharedKey) => $"https://disk.jinr.ru/index.php/s/{sharedKey}/download";

        public static void Cancel()
        {
            _httpClient.CancelPendingRequests();
        }

        static WebDavClientApi()
        {
            var c = new ConfigManager();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(c.WebDavApiCredentials)));
        }

        private static void InitToken(ref CancellationToken? token)
        {
            if (token == null)
            {
                var cts = new CancellationTokenSource();
                token = cts.Token;
            }
        }

        public static async Task<bool> UploadFile(string path, CancellationToken? cancellationToken = null)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File '{path}' doesn't exist");
            InitToken(ref cancellationToken);
            await CreateFolder(Path.GetDirectoryName(path));
            using (HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(path)))
            {
                var response = await _httpClient.PutAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", bytesContent, cancellationToken.Value).ConfigureAwait(false);
                return IsSuccessfull(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<string> MakeShareable(string file, CancellationToken? cancellationToken = null)
        {
            InitToken(ref cancellationToken);
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_hostBase}{_hostOCSApi}?path={file.Substring(Path.GetPathRoot(file).Length)}&shareType=3&permissions=3&name={Path.GetFileNameWithoutExtension(file)}"))
            {
                request.Headers.Add("OCS-APIRequest", "true");
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken.Value).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content)) throw new FileNotFoundException($"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in disk.");

                var xe = XElement.Parse(content);

                if (xe.Descendants("statuscode").First().Value == "404") throw new FileNotFoundException($"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in the cloud storage. Upload it before than makes it shareable.");
                return xe.Descendants("token").First().Value;
            }
        }

        public static async Task<bool> IsFolderExists(string path, CancellationToken? cancellationToken = null)
        {
            InitToken(ref cancellationToken);
            var response = await Send(new Uri($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}"), new HttpMethod("PROPFIND"), cancellationToken.Value);
            return IsSuccessfull(await response.Content.ReadAsStringAsync());
        }

        public static async Task CreateFolder(string path, CancellationToken? cancellationToken = null)
        {
            InitToken(ref cancellationToken);
            var dir = path.Substring(Path.GetPathRoot(path).Length);
            var subPath = "";
            foreach (var node in dir.Split(Path.DirectorySeparatorChar))
            {
                subPath = $"{subPath}/{node}";
                if (!await IsFolderExists(subPath))
                {
                    var response = await Send(new Uri($"{_hostBase}{_hostWebDavAPI}{subPath}"), new HttpMethod("MKCOL"), cancellationToken.Value);
                    await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static bool IsSuccessfull(string resp)
        {
            if (resp.Contains("exception") || resp.Contains("error"))
                return false;
            return true;
        }

        public static async Task DownloadFile(string shareId, string path, CancellationToken? cancellationToken = null)
        {
            InitToken(ref cancellationToken);

            if (!System.IO.Directory.Exists(Path.GetDirectoryName(path)))
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var request = new HttpRequestMessage(HttpMethod.Get, GetDownloadLink(shareId)))
            {
                using (
                    Stream contentStream = await (await _httpClient.SendAsync(request,cancellationToken.Value)).Content.ReadAsStreamAsync(),
                    stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    await contentStream.CopyToAsync(stream, 4096, cancellationToken.Value);
                }
            }
        }

        private static async Task<HttpResponseMessage> Send(
            Uri requestUri,
            HttpMethod method,
            CancellationToken cancellationToken,
            KeyValuePair<string, string>? header = null,
            HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            using (var request = new HttpRequestMessage(method, requestUri))
            {
                if (header != null)
                    request.Headers.Add(header.Value.Key, header.Value.Value);
                return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
            }
        }
    }

}
