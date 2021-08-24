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

using Regata.Core.Messages;
using System;
using System.Runtime.InteropServices;

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {
        private ErrorHandlerDelegate ErrorHandlerDel { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr ErrorHandlerDelegate(IntPtr ptr);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_ErrorCallBack@4", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern void ML_ErrorCallBackDelegate(ErrorHandlerDelegate edel);

        private IntPtr ErrorHandler(IntPtr errNo)
        {
            Report.Notify(new Message((int)errNo) { Head = $"Getting error from xemo device: {SerialNumber}"});
            ErrorOccurred?.Invoke(SerialNumber, (int)errNo);
            return IntPtr.Zero;
        }

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware
