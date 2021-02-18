/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2020, REGATA Experiment at FLNP|JINR                  *
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
// └── IDetector.cs                --> Interface of the Detector type

using System;
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
            catch (Exception e)
            {
                if (e.Message.Contains("278e2a"))
                {
                    Status = DetectorStatus.busy;
                    Report.Notify(new Message(Codes.WARN_DET_BUSY));
                }
                else
                    Report.Notify(new Message(Codes.ERR_DET_INTR_CONN_UNREG));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// TODO: find out what happenes in case of long connection. How to add timeoutlimit, because device.Connect already async. 
        public void Connect()
        {
            try
            {
                ConnectInternal();
                if (_device.IsConnected)
                    Report.Notify(new Message(Codes.SUCC_DET_CON));
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_DET_CONN_UNREG));
            }
        }



        /// <summary>
        /// Recconects will trying to ressurect connection with detector. In case detector has status error or ready, 
        /// it will do nothing. In case detector is off it will just call connect.
        /// In case status is busy, it will run recursively before 3 attempts with 5sec pausing.
        public async Task Reconnect()
        {
            Report.Notify(new Message(Codes.INFO_DET_RECON));

            var t1 = Task.Run(() => { while (!_device.IsConnected) { Disconnect(); } });
            var t2 = Task.Delay(DetSet.ConnectionTimeOut);

            var t3 = await Task.WhenAny(t1, t2);

            if (!_device.IsConnected)
                Connect();

            if (t3 == t1 && _device.IsConnected)
                Report.Notify(new Message(Codes.SUCC_DET_RECON));

            if (t3 == t2)
                Report.Notify(new Message(Codes.WARN_DET_CONN_TIMEOUT));
        }

        public void Disconnect()
        {
            try
            {
                Report.Notify(new Message(Codes.INFO_DET_DCON));
                if (_device.IsConnected)
                    _device.Disconnect();

                Report.Notify(new Message(Codes.SUCC_DET_DCON));
                Status = DetectorStatus.off;
                ErrorMessage = "";
            }
            catch
            {
                Report.Notify(new Message(Codes.ERR_DET_DCON_UNREG));
            }
        }

        public bool IsConnected => _device.IsConnected;



    } //  public partial class Detector : IDisposable
} // namespace Regata.Measurements.Devices
