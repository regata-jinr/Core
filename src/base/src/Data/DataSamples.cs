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
using Regata.Core.Messages;
using System;
using System.Linq;

namespace Regata.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Data
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SampleStringId"> This is a stirng of such content "{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}-{SampleNumber}";</param>
        /// <returns></returns>
        public static T GetISample<T>(string SampleStringId)
            where T : class, ISample
        {
            try
            {
                if (string.IsNullOrEmpty(SampleStringId)) return null;

                var ssid_arr = SampleStringId.Split('-');

                if (ssid_arr.Length != 6) return null;

                using (var r = new RegataContext())
                {
                    return r.Set<T>().Where(s =>
                                                s.CountryCode == ssid_arr[0] &&
                                                s.ClientNumber == ssid_arr[1] &&
                                                s.Year == ssid_arr[2] &&
                                                s.SetNumber == ssid_arr[3] &&
                                                s.SetIndex == ssid_arr[4] &&
                                                s.SampleNumber == ssid_arr[5])
                                     .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_DATA_GET_SSID) { DetailedText = string.Join(Environment.NewLine, SampleStringId, ex.Message) } );
                return null;
            }
        }


        public static T GetSrmOrMonitor<T>(string SrmOrMonitorStringId)
           where T : class, ISrmAndMonitor
        {
            try
            {
                if (string.IsNullOrEmpty(SrmOrMonitorStringId)) return null;

                if (!SrmOrMonitorStringId.Contains("s-") && !SrmOrMonitorStringId.Contains("m-")) return null;

                var ssid_arr = SrmOrMonitorStringId.Split('-');

                if (ssid_arr.Length != 6) return null;

                using (var r = new RegataContext())
                {
                    return r.Set<T>().Where(s =>
                                                s.SetName == ssid_arr[3] &&
                                                s.SetNumber == ssid_arr[4] &&
                                                s.Number == ssid_arr[5])
                                     .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_DATA_GET_SRMSID) { DetailedText = string.Join(Environment.NewLine, SrmOrMonitorStringId, ex.Message) });
                return null;
            }
        }

    }  //  public static class Data
}      // namespace Regata.Core
