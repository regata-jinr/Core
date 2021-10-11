/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
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
using Regata.Core.Messages;
using Regata.Core.Settings;

/// <summary>
/// This namespace contains abstractions for interaction with disk.jinr.ru cloud storage.
/// </summary>
namespace Regata.Core.Cloud
{
    /// <summary>
    /// disk.jinr.ru has webdavapi that here is a wrapper to this.
    /// </summary>
    public static class WebDavClientApi
    {
        private static HttpClient _httpClient;
        private const string _hostBase = @"https://disk.jinr.ru";
        private static readonly string _hostWebDavAPI = @"/remote.php/dav/files/";
        private const string _hostOCSApi = @"/ocs/v2.php/apps/files_sharing/api/v1/shares";
        public static string GetDownloadLink(string sharedKey) => $"https://disk.jinr.ru/index.php/s/{sharedKey}/download";

        static WebDavClientApi()
        {
            try
            {
                var cm = AdysTech.CredentialManager.CredentialManager.GetCredentials(GlobalSettings.Targets.DiskJinr);

                    _httpClient = new HttpClient();
                    _hostWebDavAPI += cm.UserName;
                    _httpClient = new HttpClient();
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{cm.UserName}:{cm.Password}")));
                    Report.Notify(new Message(Codes.SUCC_CLD_TRGT));
                    
                
            }
            //catch ()
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_CON_UNREG) { DetailedText = ex.ToString()});
            }
        }


        public static void Cancel()
        {
            _httpClient.CancelPendingRequests();
        }


        public static async Task<bool> RemoveFileAsync(string path, CancellationToken ct)
        {
            try
            {

                if (ct.IsCancellationRequested)
                    return false;

                Report.Notify(new Message(Codes.INFO_CLD_RMV_FILE));
                if (!await IsExistsAsync(path, ct)) return true;
                var response = await _httpClient.DeleteAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", ct).ConfigureAwait(false);

                return IsSuccessfull(await response?.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_RMV_FILE_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
        }

        public static async Task<bool> UploadFileAsync(string path, CancellationToken ct)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    return false;

                Report.Notify(new Message(Codes.INFO_CLD_UPL_FILE));

                if (!File.Exists(path))
                {
                    Report.Notify(new Message(Codes.ERR_CLD_UPL_FILE_NFND)); //($"File '{path}' doesn't exist"));
                    return false;
                }

             

                await CreateFolderAsync(Path.GetDirectoryName(path), ct);
                using (HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(path)))
                {
                    var response = await _httpClient.PutAsync($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}", bytesContent, ct).ConfigureAwait(false);
                    return IsSuccessfull(await response?.Content.ReadAsStringAsync());
                }
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.WRN_CLD_UPL_FILE_CNCL));
                return false;
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_UPL_FILE_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
        }

        public static async Task<string> MakeShareableAsync(string file, CancellationToken ct)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    return string.Empty;
                Report.Notify(new Message(Codes.INFO_CLD_FL_SHRNG));

                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_hostBase}{_hostOCSApi}?path={file.Substring(Path.GetPathRoot(file).Length)}&shareType=3&permissions=3&name={Path.GetFileNameWithoutExtension(file)}"))
                {
                    request.Headers.Add("OCS-APIRequest", "true");
                    var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false);
                    var content = await response?.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(content))
                    {
                        Report.Notify(new Message(Codes.ERR_CLD_FL_SHRNG_FNFND)); //$"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in disk."));
                        return string.Empty;
                    }

                    var xe = XElement.Parse(content);

                    if (xe.Descendants("statuscode").First().Value == "404") throw new FileNotFoundException($"File '{file.Substring(Path.GetPathRoot(file).Length)}' has not found in the cloud storage. Upload it before than makes it shareable.");
                    return xe.Descendants("token").First().Value;
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_FL_SHRNG_UNREG) { DetailedText = ex.ToString() });
                return string.Empty;
            }
        }

        public static async Task<bool> IsExistsAsync(string path, CancellationToken ct)
        {
            try 
            {
                if (ct.IsCancellationRequested)
                    return false;
            Report.Notify(new Message(Codes.INFO_CLD_IS_EXST));

            var response = await SendAsync(new Uri($"{_hostBase}{_hostWebDavAPI}/{path.Substring(Path.GetPathRoot(path).Length)}"), new HttpMethod("PROPFIND"), ct);
            return IsSuccessfull(await response?.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_IS_EXST_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
        }

        public static async Task CreateFolderAsync(string path, CancellationToken ct)
        {
            try
            {
                Report.Notify(new Message(Codes.INFO_CLD_CRT_DIR));

                if (ct.IsCancellationRequested)
                    return;

                var dir = path.Substring(Path.GetPathRoot(path).Length);
                var subPath = "";
                foreach (var node in dir.Split(Path.DirectorySeparatorChar))
                {
                    subPath = $"{subPath}/{node}";
                    if (!await IsExistsAsync(subPath, ct))
                    {
                        var response = await SendAsync(new Uri($"{_hostBase}{_hostWebDavAPI}{subPath}"), new HttpMethod("MKCOL"), ct);
                        await response?.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_CRT_DIR_UNREG) { DetailedText = ex.ToString() });
                return;
            }
        }

        public static async Task DeleteFileOrFolder(string path, CancellationToken ct)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    return;

                Report.Notify(new Message(Codes.INFO_CLD_DEL_FL_DIR));

                var dir = path.Substring(Path.GetPathRoot(path).Length);
                    if (!await IsExistsAsync(dir, ct))
                    {
                        var response = await SendAsync(new Uri($"{_hostBase}{_hostWebDavAPI}{dir}"), new HttpMethod("DELETE"), ct);
                        await response?.Content.ReadAsStringAsync();
                    }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_DEL_FIL_DIR_UNREG) { DetailedText = ex.ToString() });
                return;
            }
        }

        private static bool IsSuccessfull(string resp)
        {
            try
            {
                if (resp == null)
                    return false;
                if (resp.Contains("exception") || resp.Contains("error"))
                {
                    Report.Notify(new Message(Codes.WRN_CLD_BAD_RSPN));
                    return false;
                }
                Report.Notify(new Message(Codes.SUCC_CLD_GOOD_RSPN));
                return true;
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_BAD_RSPN_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
}

        private static async Task<HttpResponseMessage> SendAsync(
            Uri requestUri,
            HttpMethod method,
            CancellationToken cancellationToken,
            KeyValuePair<string, string>? header = null,
            HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                using (var request = new HttpRequestMessage(method, requestUri))
                {
                    if (header != null)
                        request.Headers.Add(header.Value.Key, header.Value.Value);
                    return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_SEND_REQ_UNREG) { DetailedText = ex.ToString() });
                return null;
            }
        }

        public static async Task DownloadFileAsync(string shareId, string path, CancellationToken ct)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var cli = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, GetDownloadLink(shareId)))
                    {
                        using (
                            Stream contentStream = await (await cli.SendAsync(request, ct)).Content.ReadAsStreamAsync(),
                            stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                        {
                            await contentStream.CopyToAsync(stream, 4096, ct);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_CLD_DWNLD_FILE_UNREG) { DetailedText = ex.ToString() });
                return;
            }
        }

    } // public static class WebDavClientApi    
}     // namespace Regata.Core.Cloud
