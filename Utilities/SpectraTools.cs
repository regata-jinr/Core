using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Regata.Utilities
{
    public enum IrradiationType { SLI, LLI1, LLI2};
    public static class SpectraTools 
    {
        // TODO: move to libs
        // TODO: two regimes:
        // TODO:    saving only one file by name
        // TODO:    saving all files of chosen types by set key including srms
        // TODO: fill file list from irrad. tables
        // TODO: prepare context and models
        // TODO: Downaloading of each file sohuld be the chaing of searching and the downloading tasks
        // TODO: add tests

        public static IrradiationType Type;
        public static string SetKey;
        public static string DirPath;

        private static Dictionary<string, string> SpectraPathDict;
        private static Dictionary<string, string> SrmPathDict;

        /// <summary>
        /// Create directory in case of it doesn't exist.
        /// </summary>
        /// <param name="dir">Path to spectra</param>
        private static void CheckDir(string dir)
        {
            var path = dir.Substring(Path.GetPathRoot(dir).Length);
            var nodes = new List<string>();
            var subPath = "";

            foreach (var node in path.Split(Path.DirectorySeparatorChar))
            {
                subPath = $"{subPath}{Path.DirectorySeparatorChar}{node}";
                nodes.Add(subPath);
            }

        }



        public static async Task UploadSpectraAsync(string SourcePath)
        {
            
        }

        private static async Task GetListOfSamplesSpectraAsync(CancellationToken ct)
        {
            
        }

        private static async Task GetListOfSRMsSpectraAsync(CancellationToken ct)
        {

        }

        //private static async Task<string> SearchFileOnTheServer(string spectra, CancellationToken ct)
        //{

        //}

        public static async Task DownloadFilesAsync()
        {
            
        }

        public static async Task DownloadBySpectraNameAsync(string spectra, CancellationToken ct)
        {
            
        }
    }
}
