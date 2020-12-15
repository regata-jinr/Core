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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
using Regata.Core.DB.MSSQL.Models;
using Regata.Core.DB.MSSQL.Context;
using Microsoft.EntityFrameworkCore;

namespace Regata.Core.Cloud
{

    public enum IrradiationType { SLI, LLI1, LLI2, BCKG }

    public static class SpectraTools
    {
        // TODO: implement GetBackgroundSpectra
        public static IReadOnlyDictionary<IrradiationType, string> IrradiationTypeMap = new Dictionary<IrradiationType, string>()
                                                                                {
                                                                                    {IrradiationType.SLI, "SLI"},
                                                                                    {IrradiationType.LLI1, "LLI-1"},
                                                                                    {IrradiationType.LLI2, "LLI-2"},
                                                                                    {IrradiationType.BCKG, "BCKG"}
                                                                                 };

        public static async Task GetBackgroundSpectra(string SetKey, CancellationToken? ct = null)
        {
            throw new NotImplementedException();
        }

        public static async IAsyncEnumerable<SetSpectraSLI> SLISetSpectrasAsync(string SetKey, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext(""))
            {
                var sliSpectras = ssc.SLISpectras.FromSqlRaw("exec FormSLIFilesList @countryCode, @clientid, @year, @setnum, @setind",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4])).AsAsyncEnumerable().WithCancellation(ct);
                await foreach (var spectraInfo in sliSpectras)
                    yield return spectraInfo;
            }
        }

        public static async IAsyncEnumerable<SetSpectraLLI> LLISetSpectrasAsync(string SetKey, IrradiationType type, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext(""))
            {
                var lliSpectras = ssc.LLISpectras.FromSqlRaw("exec FormLLIFilesList @countryCode, @clientid, @year, @setnum, @setind, @type",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4]),
                    new SqlParameter("type", IrradiationTypeMap[type])).AsAsyncEnumerable().WithCancellation(ct);

                await foreach (var spectraInfo in lliSpectras)
                    yield return spectraInfo;
            }
        }

        public static async Task DownloadSpectraAsync(string spectra, string path, CancellationToken ct)
        {
            using (var ssc = new RegataContext(""))
            {
                var sharedSpectra = ssc.SharedSpectras.Where(ss => ss.fileS == spectra).FirstOrDefault();
                if (sharedSpectra == null) throw new FileNotFoundException($"Spectra '{spectra}' was not found");
                await WebDavClientApi.DownloadFile(sharedSpectra.token, $"{path}/{spectra}.cnf", ct);
            }
        }

        public static async Task<bool> UploadFileToCloud(string file, CancellationToken Token)
        {
            var result = false;
            var token = "";
            result = await WebDavClientApi.UploadFile(file, Token);
            if (result)
                token = await WebDavClientApi.MakeShareable(file, Token);
            else
                throw new InvalidOperationException("Can't upload file");

            if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("File hasn't got token!");

            var ss = new SharedSpectra()
            {
                fileS = Path.GetFileNameWithoutExtension(file),
                token = token
            };

            using (var ic = new RegataContext(""))
            {
                bool IsExists = ic.SharedSpectras.Where(s => s.fileS == ss.fileS).Any();

                if (IsExists)
                {
                    ic.SharedSpectras.Update(ss);
                }
                else
                {
                    await ic.SharedSpectras.AddAsync(ss, Token);
                }

                await ic.SaveChangesAsync(Token);
                return result;
            }
        }
    } // public static class SpectraTools
}     // namespace Regata.Core.Cloud
