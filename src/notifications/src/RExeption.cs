/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Regata.Core.Notifications
{

    // TODO: move to settings class
    #region Move to settings class
    public static class CriticalErrors
    {
        private static ObservableCollection<RErrorCodes> List;

        static CriticalErrors()
        {
            List = new ObservableCollection<RErrorCodes>();
            // TODO: fill from the settings
        }

        public static bool Contains(RErrorCodes ec)
        {
            return List.Contains(ec);
        }

        public static void Add(RErrorCodes ec)
        {
            List.Add(ec);
        }

        public static void Remove(RErrorCodes ec)
        {
            List.Remove(ec);
        }
    }
#endregion

    public class RException : Exception
    {
        public readonly RErrorCodes RErrorCode;

        public RException(RErrorCodes erCode, Exception innerException = null) : base("", innerException)
        {
            RErrorCode = erCode;
            Notify.NotifyError(this);
        }
    }
}