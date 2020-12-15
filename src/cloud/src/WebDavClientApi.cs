/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2020, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

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

namespace Regata.Core.Cloud
{
    public static class WebDavClientApi
    {
        private static HttpClient _httpClient;
        private const string _hostBase = @"https://disk.jinr.ru";
        private const string _hostWebDavAPI = @"/remote.php/dav/files/regata";
        private const string _hostOCSApi = @"/ocs/v2.php/apps/files_sharing/api/v1/shares";
        private static string _diskJinrTarget;
        public static string GetDownloadLink(string sharedKey) => $"https://disk.jinr.ru/index.php/s/{sharedKey}/download";

        public static string DiskJinrTarget
        {
            get { return _diskJinrTarget; }

            set
            {
                _diskJinrTarget = value;
                var cm = AdysTech.CredentialManager.CredentialManager.GetCredentials(DiskJinrTarget);

                if (cm == null)
                    throw new ArgumentException("Can't load cloud storage credential. Please add it to the windows credential manager");

                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{cm.UserName}:{cm.Password}")));
            }
        }

        public static void Cancel()
        {
            _httpClient.CancelPendingRequests();
        }



        public static async Task<bool> RemoveFile(string path, CancellationToken ct)
        {
            if (_httpClient == null) return false;

            if (!await IsExists(path, ct)) return true;
            var response = await _httpClient.DeleteAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", ct).ConfigureAwait(false);

            return IsSuccessfull(await response.Content.ReadAsStringAsync());
        }

        public static async Task<bool> UploadFile(string path, CancellationToken ct)
        {
            if (_httpClient == null) return false;

            if (!File.Exists(path)) throw new FileNotFoundException($"File '{path}' doesn't exist");
            await CreateFolder(Path.GetDirectoryName(path), ct);
            using (HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(path)))
            {
                var response = await _httpClient.PutAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", bytesContent, ct).ConfigureAwait(false);
                return IsSuccessfull(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<string> MakeShareable(string file, CancellationToken ct)
        {
            if (_httpClient == null) return string.Empty;

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_hostBase}{_hostOCSApi}?path={file.Substring(Path.GetPathRoot(file).Length)}&shareType=3&permissions=3&name={Path.GetFileNameWithoutExtension(file)}"))
            {
                request.Headers.Add("OCS-APIRequest", "true");
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content)) throw new FileNotFoundException($"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in disk.");

                var xe = XElement.Parse(content);

                if (xe.Descendants("statuscode").First().Value == "404") throw new FileNotFoundException($"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in the cloud storage. Upload it before than makes it shareable.");
                return xe.Descendants("token").First().Value;
            }
        }

        public static async Task<bool> IsExists(string path, CancellationToken ct)
        {
            if (_httpClient == null) return false;

            var response = await Send(new Uri($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}"), new HttpMethod("PROPFIND"), ct);
            return IsSuccessfull(await response.Content.ReadAsStringAsync());
        }

        public static async Task CreateFolder(string path, CancellationToken ct)
        {
            if (_httpClient == null) return;


            var dir = path.Substring(Path.GetPathRoot(path).Length);
            var subPath = "";
            foreach (var node in dir.Split(Path.DirectorySeparatorChar))
            {
                subPath = $"{subPath}/{node}";
                if (!await IsExists(subPath, ct))
                {
                    var response = await Send(new Uri($"{_hostBase}{_hostWebDavAPI}{subPath}"), new HttpMethod("MKCOL"), ct);
                    await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static bool IsSuccessfull(string resp)
        {

            if (_httpClient == null) return false;

            if (resp.Contains("exception") || resp.Contains("error"))
                return false;
            return true;
        }

        private static async Task<HttpResponseMessage> Send(
            Uri requestUri,
            HttpMethod method,
            CancellationToken cancellationToken,
            KeyValuePair<string, string>? header = null,
            HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (_httpClient == null) return null;


            using (var request = new HttpRequestMessage(method, requestUri))
            {
                if (header != null)
                    request.Headers.Add(header.Value.Key, header.Value.Value);
                return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task DownloadFile(string shareId, string path, CancellationToken ct)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var request = new HttpRequestMessage(HttpMethod.Get, GetDownloadLink(shareId)))
            {
                using (
                    Stream contentStream = await (await _httpClient.SendAsync(request, ct)).Content.ReadAsStreamAsync(),
                    stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    await contentStream.CopyToAsync(stream, 4096, ct);
                }
            }
        }

    } // public static class WebDavClientApi    
}     // namespace Regata.Core.Cloud
