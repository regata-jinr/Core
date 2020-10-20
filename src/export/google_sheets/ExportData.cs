using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace Regata.Utilities
{
    public static class ExportData
    {
        // TODO: add tests for parsed data
        /// <summary>
        /// Returns array of the model type T from shared by the link Google Sheet documnet.
        /// </summary>
        /// <typeparam name="T">Model type of imported data</typeparam>
        /// <param name="link">Link to the shared google sheet file</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Array of the model type data contained in Google Sheet</returns>
        public static async Task<T[]> FromGoogleSheet<T>(string link, CancellationToken ct)
        {
            WebClient client = null;
            try
            {
                var file = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Regata\\tmpSet.csv";
                var key = new Uri(link).AbsolutePath.Split('/')[3];
                var performedLink = $"https://docs.google.com/spreadsheets/d/{key}/gviz/tq?tqx=out:csv&sheet=SamplesList";
                client = new WebClient();
                await Task.Run(()=>client.DownloadFile(new Uri(performedLink), file), ct);

                return await Task.Run(() => FromCSVToArray<T>(file), ct);
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Something wrong with provided link. Make sure the link contains the key i.e. file has shared access. For example, try to open it in non default browser in your system.");
            }
            catch (UriFormatException)
            {
                throw new WebException("Something wrong with provided link. Make sure the link contains the key i.e. file has shared access. For example, try to open it in non default browser in your system.");
            }
            catch (WebException)
            {
                throw new WebException("Something wrong with provided link. Make sure the link contains the key i.e. file has shared access. For example, try to open it in non default browser in your system.");
            }
            finally
            {
                client?.Dispose();
            }
        }

        /// <summary>
        /// Returns array of the model type T from shared by the provided path to Excel file.
        /// </summary>
        /// <typeparam name="T">Model type of imported data</typeparam>
        /// <param name="path">Path to excel file with imported data</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Array of the model type data contained in input Excel document</returns>
        public static async Task<T[]> FromExcel<T>(string path, CancellationToken ct)
        {
            var toFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Regata\\tmpSet.csv";

            Application app = new Application();
            Workbook wb = app.Workbooks.Open(path, 
                                            Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing
                                            );

            try
            {
                await Task.Run(() =>
                {
                    wb.SaveAs(toFile, XlFileFormat.xlCSVWindows, Type.Missing,
                                      Type.Missing, false, false, 
                                      XlSaveAsAccessMode.xlExclusive,
                                      XlSaveConflictResolution.xlLocalSessionChanges,
                                      false, Type.Missing, Type.Missing, Type.Missing
                             );

                }, ct);

                return await Task.Run(() => FromCSVToArray<T>(toFile), ct);
            }
            catch (OperationCanceledException oce)
            {
                throw new OperationCanceledException("Operation was canceled");
            }
            finally
            {
                wb?.Close(false, Type.Missing, Type.Missing);
                app?.Quit();
            }
        }

        /// <summary>
        /// This is simple wrap for CSVHelper package.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">Temporary file with exported data</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Exported data array</returns>
        private static T[] FromCSVToArray<T>(string file)
        {
            CsvReader csv = null;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(file);

                csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";
                csv.Configuration.BadDataFound = null;

                return new List<T>(csv.GetRecords<T>()).ToArray();
            }
            catch (CsvHelperException csvprocessing)
            {
                throw new ArgumentException($"Something wrong with data inside provided file.Check you file has shared access. See details in this message:  {csvprocessing.Message}");
            }
            finally
            {
                reader?.Dispose();
                csv?.Dispose();
                File.Delete(file);
            }
        }

    }
}
