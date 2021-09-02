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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ErrorHandlerDelegate(IntPtr ptr);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_ErrorCallBack@4", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ML_ErrorCallBackDelegate(ErrorHandlerDelegate edel);

        private IntPtr ErrorHandler(IntPtr errNo)
        {
            try
            { 
            if (errNo == IntPtr.Zero)
                return IntPtr.Zero;
            // NOTE: xemo error codes begin from 1, our SC error codes start with 3630, first three position already taken, so we have to add 3633 to xemo error code to convert it into our format
            Report.Notify(new Message((int)errNo+3633) { DetailedText = $"Xemo device sn: {SerialNumber}. Xemo error code {(int)errNo}"});
            AutoEmergency((int)errNo + 3633);
            ErrorOccurred?.Invoke(SerialNumber, (int)errNo + 3633);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_ERR_HNDL_UNREG) { DetailedText = ex.Message });
            }
            return errNo;
        }

        /// <summary>
        /// The method reacts to the given error
        /// </summary>
        /// <param name="code">Error code in Regata format</param>
        private void AutoEmergency(int code)
        {
            switch (code)
            {
                
                case Codes.ERR_XM_37:
                    Stop(_activeAxis);
                    if (IsNegativeSwitcher(_activeAxis))
                        Move(_activeAxis, GetAxisPosition(_activeAxis) + 1000);
                    if (IsPositiveSwitcher(_activeAxis))
                        Move(_activeAxis, GetAxisPosition(_activeAxis) - 1000);
                    break;

                default:
                    break;
            }
        }

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware
