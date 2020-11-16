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

namespace Regata.Core.Notifications
{


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