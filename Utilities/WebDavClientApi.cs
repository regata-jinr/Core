using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Regata.Utilities
{
    public static class WebDavClientApi
    {
        //private static HttpClient _httpClient;
        //static WebDavClientApi()
        //{
        //    _httpClient = new HttpClient();
        //    _httpClient.BaseAddress = new Uri(@"https://disk.jinr.ru/remote.php/dav");
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", ""); 
        //}

        public static async Task UploadFile(string path)
        { }

        public static async Task CreateFolder(string path)
        { }

        public static async Task DownloadFile(string path)
        { }

        public static async Task<string> SearchFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
