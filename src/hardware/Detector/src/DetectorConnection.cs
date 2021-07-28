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


// Contains methods for connection, disconnection to the device. Reset connection and so on.
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> opened
// ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring spectra. 
// |                                    E.g. filling information about sample. Save file
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by special code
// |                                    See codes here CanberraDeviceAccessLib.ParamCodes 
// |                                    Also some of important parameters wrapped into properties
// ├── DetectorProperties.cs       --> Contains description of basics properties, events, enumerations and additional classes
// ├── G2KUtilities.cs             --> Contains aliases for running utilities from GENIE2K/EXEFILES.
// └── IDetector.cs                --> Interface of the Detector type

using CanberraDeviceAccessLib;
using Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {

        private void ConnectInternal()
        {
            try
            {
                Status = DetectorStatus.off;
                _device.Connect(Name, DetSet.ConnectOption);
                Status = DetectorStatus.ready;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("278e2a"))
                {
                    Status = DetectorStatus.busy;
                    Report.Notify(new DetectorMessage(Codes.WARN_DET_BUSY));
                }
                else
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_INTR_CONN_UNREG) { DetailedText = ex.ToString() });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Connect()
        {
            try
            {
                ConnectInternal();
                if (_device.IsConnected)
                    Report.Notify(new DetectorMessage(Codes.SUCC_DET_CON));
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CONN_UNREG) { DetailedText = ex.ToString() });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            try
            {
                var ct = new CancellationTokenSource(DetSet.ConnectionTimeOut);
                await Task.Run(ConnectInternal, ct.Token);
                if (_device.IsConnected)
                    Report.Notify(new DetectorMessage(Codes.SUCC_DET_CON));
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new DetectorMessage(Codes.WARN_DET_CONN_TIMEOUT));
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CONN_UNREG) { DetailedText = ex.ToString() });
            }
        }



        /// <summary>
        /// Recconects will trying to ressurect connection with detector. In case detector has status error or ready, 
        /// it will do nothing. In case detector is off it will just call connect.
        /// In case status is busy, it will run recursively before 3 attempts with 5sec pausing.
        public async Task Reconnect()
        {
            Report.Notify(new DetectorMessage(Codes.INFO_DET_RECON));
            // fixme: I think here is better to don't use _device object in other thread. It's better to use static functions
            var t1 = Task.Run(() => { while (!_device.IsConnected) { Disconnect(); } });
            var t2 = Task.Delay(DetSet.ConnectionTimeOut);

            var t3 = await Task.WhenAny(t1, t2);

            if (!_device.IsConnected)
                Connect();

            if (t3 == t1 && _device.IsConnected)
                Report.Notify(new DetectorMessage(Codes.SUCC_DET_RECON));

            if (t3 == t2)
                Report.Notify(new DetectorMessage(Codes.WARN_DET_CONN_TIMEOUT));
        }

        public void Disconnect()
        {
            try
            {
                Report.Notify(new DetectorMessage(Codes.INFO_DET_DCON));
                if (_device.IsConnected)
                    _device.Disconnect();

                Report.Notify(new DetectorMessage(Codes.SUCC_DET_DCON));
                Status = DetectorStatus.off;
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_DCON_UNREG) { DetailedText = ex.ToString() });
            }
        }

        public static bool IsDetectorAvailable(string name)
        {
            DeviceAccess dev = null;
            try
            {
                dev = new DeviceAccess();
                dev.Connect(name);
                return true;
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CONN_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
            finally
            {
                if (dev != null && dev.IsConnected)
                    dev.Disconnect();

            }
        }

        public static async Task<bool> IsDetectorAvailableAsync(string name)
        {
            var ct = new CancellationTokenSource(DetSet.ConnectionTimeOut);
            try
            {
                return await Task.Run(() => IsDetectorAvailable(name), ct.Token);
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new DetectorMessage(Codes.WARN_DET_CONN_TIMEOUT));
                return false;
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CONN_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
        }

        public static async Task<string[]> GetAvailableDetectorsAsync()
        {
            var devs = new List<string>(8);

            try
            {
                var _device = new DeviceAccess();
                var detNames = (object[])_device.ListSpectroscopyDevices;
                foreach (var n in detNames)
                {
                    if (await IsDetectorAvailableAsync(n.ToString()))
                        devs.Add(n.ToString());
                }
                return devs.ToArray();
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CONN_UNREG) { DetailedText = ex.ToString() });
                return devs.ToArray();
            }
        }

        public static async IAsyncEnumerable<string> GetAvailableDetectorsAsyncStream()
        {
                var _device = new DeviceAccess();
                var detNames = (object[])_device.ListSpectroscopyDevices;
                foreach (var n in detNames)
                {
                    if (await IsDetectorAvailableAsync(n.ToString()))
                        yield return n.ToString();
                    else
                        yield return "";
                }
        }

    } // public partial class Detector : IDisposable
}     // namespace Regata.Measurements.Devices
