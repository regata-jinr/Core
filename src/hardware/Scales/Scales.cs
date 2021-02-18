/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.IO.Ports;

// TODO: https://github.com/regata-jinr/Core/issues/37

namespace Regata.Core.Hardware
{
    public static class Scales 
    {
        public static float GetWeight(string comName)
        {
            if (string.IsNullOrEmpty(comName))
                Report.Notify(new Message(Codes.ERR_SCL_EMPT_COM));

            float weight = -1.0f;
            try
            {
                using (var port = new SerialPort(comName, 9600, Parity.None, 8, StopBits.One))
                {
                    if (port == null) throw new InvalidOperationException("Can't get data from the scales!");
                    port.Open();
                    port.ReadExisting();
                    string weightstr = port.ReadLine().Replace("\r", "");
                    if (!float.TryParse(weightstr, out weight))
                    {
                        Report.Notify(new Message(Codes.ERR_SCL_GET_WGHT));
                    }
                }
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_SCL_UNREG));
            }
            return weight;
        }
       
    } // public static class Scales 
}     // namespace Regata.Core.Hardware
