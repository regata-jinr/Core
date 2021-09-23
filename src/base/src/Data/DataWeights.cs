/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using System.Linq;

namespace Regata.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Data
    {
        public static float? GetSampleSLIWeight(ISample sample)
        {
            var smpl = GetWeightedSample<Sample>(sample);
                return smpl?.SLIWeight;
        }

        public static float? GetSampleLLIInitWeight(ISample sample)
        {
            var smpl = GetWeightedSample<Sample>(sample);
            return smpl?.LLIWeight;
        }

        public static float? GetSampleLLIReWeight(ISample sample)
        {
            var rw = GetWeightedSample<ReweightInfo>(sample);
            return rw?.ARepackWght;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns lli weight after repack. In case it is null returns init lli weight</returns>
        public static float? GetSampleLLIWeight(ISample sample)
        {
            var w = GetSampleLLIReWeight(sample);

            if (w == null)
                return GetSampleLLIInitWeight(sample);

            return w;
        }

        private static T GetWeightedSample<T>(ISample sample)
              where T : class, ISample
        {
            if (sample == null) return null;

            using (var r = new RegataContext())
            {
                return r.Set<T>().Where(s =>
                                            s.CountryCode  == sample.CountryCode &&
                                            s.ClientNumber == sample.ClientNumber &&
                                            s.Year         == sample.Year &&
                                            s.SetNumber    == sample.SetNumber &&
                                            s.SetIndex     == sample.SetIndex &&
                                            s.SampleNumber == sample.SampleNumber)
                                 .FirstOrDefault();
            }
        }


    }  //  public static class Data
}      // namespace Regata.Core
