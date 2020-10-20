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
using System.Windows.Forms;
using System.IO.Ports;

namespace SamplesWeighting
{
    public static class Scales 
    {
        private static SerialPort port;

        static Scales()
        {
            try
            {
                string com = ConfigurationManager.ComPort;
                if (string.IsNullOrEmpty(com))
                {
                    MessageBox.Show("The scales are not found! Please Check the list of available devices.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                port = new SerialPort(com, 9600, Parity.None, 8, StopBits.One);
            }
            catch (UnauthorizedAccessException)
            { MessageBox.Show("The scales in the sleep mode or we be not able to connect to it. Try to enable it.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex)
            { MessageBox.Show($"Exception has occurred in process of getting the data from scales:\n {ex.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static float GetWeight()
        {
            try
            {
                if (port == null) throw new InvalidOperationException("Can't get data from the scales!");
                port.Open();
                float weight = -1.0f;
                port.ReadExisting();
                string weightstr = port.ReadLine().Replace("\r", "");
                if (!float.TryParse(weightstr, out weight))
                {
                    throw new InvalidOperationException("Can't get data from the scales! Try to repeat operation!");
                }
                return weight;
            }
            finally
            {
                if (port != null && port.IsOpen) port.Close();
            }
        }
       
    } // public static class Scales 
}    // namespace SamplesWeighting

