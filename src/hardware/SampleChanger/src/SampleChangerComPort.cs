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

using Microsoft.Win32;
using Regata.Core.Messages;
using System;
using System.Text.RegularExpressions;

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {
        private const string PathToComs = @"System\ControlSet001\Enum\FTDIBUS\VID_0403+PID_6001+";

        private ushort GetComPortByDeviceId(string xemoSN)
        {
            try
            {
                var path = string.Join('+', PathToComs, xemoSN);
                var regexpForCom = new Regex(@"\d{1,2}");

                using (RegistryKey subFoldersList = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (subFoldersList != null)
                    {
                        foreach (var subDir in subFoldersList.GetSubKeyNames())
                        {
                            var key = subFoldersList.OpenSubKey(subDir);
                            return ushort.Parse(regexpForCom.Match(key.GetValue("FriendlyName").ToString()).Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_COM_UNREG) { DetailedText = ex.Message } );
            }
            
            Report.Notify(new Message(Codes.WARN_XM_COM_NOT_FOUND));
            return 0;

        }


    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware
