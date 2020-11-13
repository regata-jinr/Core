﻿/***************************************************************************
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

    // TODO: move to settings class
    #region Move to settings class
  
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