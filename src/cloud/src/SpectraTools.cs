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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
using Regata.Core.DataBase.Models;
using Regata.Core.DataBase;
using Regata.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Regata.Core.Cloud
{
    public enum IrradiationType { SLI, LLI1, LLI2, BCKG }

    /// <summary>
    /// SpectraTools is the high level abstractions for basics operation with cloud storage units.
    /// </summary>
    public static class SpectraTools
    {
        public static IReadOnlyDictionary<IrradiationType, string> IrradiationTypeMap = 
            new Dictionary<IrradiationType, string>()
                {
                    {IrradiationType.SLI, "SLI"},
                    {IrradiationType.LLI1, "LLI-1"},
                    {IrradiationType.LLI2, "LLI-2"},
                    {IrradiationType.BCKG, "BCKG"}
                };

        public static async Task GetBackgroundSpectraForSampleSetAsync(string SetKey, CancellationToken? ct = null)
        {
            throw new NotImplementedException();
        }

#if NETFRAMEWORK
        public static async Task<List<SpectrumSLI>> GetSLISpectraForSampleSetAsync(string SetKey, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext())
            {
                return await  ssc.SLISpectra.FromSqlRaw("exec FormSLIFilesList @countryCode, @clientid, @year, @setnum, @setind",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4])).ToListAsync(ct);
            }
        }

#else
        public static async IAsyncEnumerable<SpectrumSLI> GetSLISpectraForSampleSetAsync(string SetKey, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext())
            {
                var sliSpectras = ssc.SLISpectra.FromSqlRaw("exec FormSLIFilesList @countryCode, @clientid, @year, @setnum, @setind",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4])).AsAsyncEnumerable().WithCancellation(ct);

                await foreach (var spectraInfo in sliSpectras)
                    yield return spectraInfo;
    }
}
#endif

#if NETFRAMEWORK
        public static async Task<List<SpectrumLLI>> GetLLISpectraForSampleSetAsync(string SetKey, IrradiationType type, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext())
            {

                return await ssc.LLISpectra.FromSqlRaw("exec FormLLIFilesList @countryCode, @clientid, @year, @setnum, @setind, @type",
                   new SqlParameter("countrycode", sks[0]),
                   new SqlParameter("clientid",    sks[1]),
                   new SqlParameter("year",        sks[2]),
                   new SqlParameter("setnum",      sks[3]),
                   new SqlParameter("setind",      sks[4]),
                   new SqlParameter("type",        IrradiationTypeMap[type])).ToListAsync(ct);
            }
        }


#else
        public static async IAsyncEnumerable<SpectrumLLI> GetLLISpectraForSampleSetAsync(string SetKey, IrradiationType type, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new RegataContext())
            {

 var lliSpectras = ssc.LLISpectra.FromSqlRaw("exec FormLLIFilesList @countryCode, @clientid, @year, @setnum, @setind, @type",
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

#endif

        public static async Task DownloadSpectraAsync(string spectra, string path, CancellationToken ct)
        {
            try
            {
                using (var ssc = new RegataContext())
                {
                    var sharedSpectra = ssc.SharedSpectra.Where(ss => ss.fileS == spectra).FirstOrDefault();
                    if (sharedSpectra == null)
                    {
                        Report.Notify(new Message(Codes.ERR_CLD_DWLD_FNFND));
                        return;
                    }
                    await WebDavClientApi.DownloadFileAsync(sharedSpectra.token, $"{path}/{spectra}.cnf", ct);
                }
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_CLD_DWLD_UNREG));
            }
        }

        public static async Task<bool> UploadFileToCloudAsync(string file, CancellationToken Token)
        {
            try
            {
                Report.Notify(new Message(Codes.INFO_CLD_UPL_FILE));

                if (Token.IsCancellationRequested)
                    return false;

                var result = false;
                var token = "";
                result = await WebDavClientApi.UploadFileAsync(file, Token);
                if (result)
                    token = await WebDavClientApi.MakeShareableAsync(file, Token);
                else
                {
                    Report.Notify(new Message(Codes.ERR_CLD_UPL_FILE));
                    return false;
                }

                if (string.IsNullOrEmpty(token))
                {
                    Report.Notify(new Message(Codes.ERR_CLD_GEN_TKN));
                    return false;
                }

                var ss = new SharedSpectrum()
                {
                    fileS = Path.GetFileNameWithoutExtension(file),
                    token = token
                };

                using (var ic = new RegataContext())
                {
                    bool IsExists = await ic.SharedSpectra.Where(s => s.fileS == ss.fileS).AnyAsync();

                    if (IsExists)
                    {
                        ic.SharedSpectra.Update(ss);
                    }
                    else
                    {
                        await ic.SharedSpectra.AddAsync(ss, Token);
                    }

                    await ic.SaveChangesAsync(Token);
                    return result;
                }

            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.WRN_CLD_UPL_FILE_CNCL));
                return false;
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_CLD_UPL_UNREG));
                return false;
            }
        }

    } // public static class SpectraTools
}     // namespace Regata.Core.Cloud
