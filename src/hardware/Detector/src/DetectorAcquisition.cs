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

// This file contains methods that allow to manage of spectra acquisition process. 
// Start, stop, pause, clear acquisition process and also specify acquisition mode.
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> opened
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> Contains methods for connection, disconnection to the device. Reset connection and so on.
// ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring spectra. 
// |                                    E.g. filling information about sample. Save file.
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by special code.
// |                                    See codes here CanberraDeviceAccessLib.ParamCodes. 
// |                                    Also some of important parameters wrapped into properties
// ├── DetectorProperties.cs       --> Contains description of basics properties, events, enumerations and additional classes
// └── IDetector.cs                --> Interface of the Detector type

using System;
using CanberraDeviceAccessLib;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        /// <summary>
        ///  Starts acquiring with specified aCountToLiveTime, before this the device will be cleared.
        /// </summary>
        /// <param name="time"></param>
        public void Start()
        {
            try
            {
                if (!IsPaused)
                    _device.Clear();

                IsPaused = false;

                if (Status != DetectorStatus.ready)
                {
                    Report.Notify(Codes.ERR_DET_NOT_READY);
                    return;
                }

                _device.SpectroscopyAcquireSetup(AcquisitionModes.aCountToRealTime, CurrentMeasurement.Duration.Value);
                _device.AcquireStart(); // already async
                Status = DetectorStatus.busy;
                CurrentMeasurement.DateTimeStart = DateTime.Now;
                CurrentMeasurement.Detector = Name;
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_ACQ_START_UNREG);
            }

        }

        /// <summary>
        /// Set acquiring on pause.
        /// </summary>
        public void Pause()
        {
            try
            {
                if (Status == DetectorStatus.ready)
                    return;

                Report.Notify(Codes.INFO_DET_ACQ_PAUSE); //$"Attempt to set pause for the acquiring");
                _device.AcquirePause();
                IsPaused = true;
                Status = DetectorStatus.ready;
                Report.Notify(Codes.SUCC_DET_ACQ_PAUSE); //$"Paused was successful. Detector ready to continue acquire process");
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_ACQ_PAUSE_UNREG);
            }

        }

        /// <summary>
        /// Stops acquiring. Means pause and clear and **generate acquire done event.**
        /// </summary>
        public void Stop()
        {
            try
            {
                if (Status == DetectorStatus.ready)
                    return;
                Report.Notify(Codes.INFO_DET_ACQ_STOP); //$"Attempt to stop the acquiring");
                _device.AcquireStop(StopOptions.aNormalStop);
                //_device.SendCommand(DeviceCommands.aStop); // use command sending because in this case it will generate AcquireDone message
                IsPaused = false;
                Status = DetectorStatus.ready;
                Report.Notify(Codes.SUCC_DET_ACQ_STOP); //$"Stop was successful. Acquire done event will be generate. Detector ready to acquire again");
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_ACQ_STOP_UNREG); //e, NotificationLevel.Error, AppManager.Sender);
            }

        }

        /// <summary>
        /// Clears current acquiring status.
        /// </summary>
        public void Clear()
        {
            try
            {
                Report.Notify(Codes.INFO_DET_ACQ_CLR); //$"Clearing the detector");
                _device.Clear();
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_ACQ_CLR_UNREG);
            }
        }

        private AcquisitionModes _acquisitionModes;
        public AcquisitionModes AcquisitionMode
        {
            get { return _acquisitionModes; }
            set
            {
                _acquisitionModes = value;
                Report.Notify(Codes.INFO_DET_ACQ_MODE_CHNG); //$"Detector has got Acquisition mode - '{value}' and number of counts - '{Counts}'");
                _device.SpectroscopyAcquireSetup(value, Counts);
            }
        }

        private uint _counts;
        public uint Counts
        {
            get { return _counts; }
            set
            {
                _counts = value;
                Report.Notify(Codes.INFO_DET_ACQ_COUNTS_CHNG); //$"Detector has got Acquisition mode - '{AcquisitionMode}' and number of counts - '{value}'");
                _device.SpectroscopyAcquireSetup(AcquisitionMode, value);
            }
        }

        /// <summary>
        /// The reason of this field that stop method generates acquire done event, this means
        /// that we should distinguish stop and pause. That's why this field exist
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        ///
        ///  |Advise Mask        |Description                                        |int value(lParam)|
        ///  |:-----------------:|:-------------------------------------------------:|:---------------:|
        ///  |DisplaySetting     | Display settings have changed                     |  1              |
        ///  |ExternalStart      | Acquisition has been started externall            |  1048608        |
        ///  |CalibrationChange  | A calibration parameter has changed               |  4              |
        ///  |AcquireStart       | Acquisition has been started                      |  134217728      |
        ///  |AcquireDone        | Acquisition has been stopped                      | -2147483648     |
        ///  |DataChange         | Data has been changes (occurs after AcquireClear) |  67108864       |
        ///  |HardwareError      | Hardware error                                    |  2097152        |
        ///  |HardwareChange     | Hardware setting has changed                      |  268435456      |
        ///  |HardwareAttention  | Hardware is requesting attention                  |  16777216       |
        ///  |DeviceUpdate       | Device settings have been updated                 |  8388608        |
        ///  |SampleChangerSet   | Sample changer set                                |  1073741824     |
        ///  |SampleChangeAdvance| Sample changer advanced                           |  4194304        |

        /// </summary>
        /// <param name="message">DeviceMessages type from CanberraDeviceAccessLib</param>
        /// <param name="wParam">The first parameter of information associated with the message.</param>
        /// <param name="lParam">The second parameter of information associated with the message</param>
        private void DeviceMessagesHandler(int message, int wParam, int lParam)
        {
            string response = "";
            bool isForCalling = false;
            try
            {
                if ((int)AdviseMessageMasks.amAcquireDone == lParam && !IsPaused)
                {
                    Report.Notify(Codes.INFO_DET_ACQ_DONE); //$"Has got message AcquireDone.");
                    response = "Acquire has done";
                    Status = DetectorStatus.ready;
                    CurrentMeasurement.DateTimeFinish = DateTime.Now;
                    isForCalling = true;
                }

                if ((int)AdviseMessageMasks.amAcquireStart == lParam)
                {
                    Report.Notify(Codes.INFO_DET_ACQ_START); //$"Has got message amAcquireStart.");
                    response = "Acquire has start";
                    Status = DetectorStatus.busy;
                    isForCalling = true;
                }

                if ((int)AdviseMessageMasks.amHardwareError == lParam)
                {
                    Status = DetectorStatus.error;
                    ErrorMessage = $"{_device.Message((MessageCodes)lParam)}";
                    response = ErrorMessage;
                    isForCalling = true;
                    Report.Notify(Codes.ERR_DET_ACQ_HRDW);
                }
                if ((int)AdviseMessageMasks.amAcquisitionParamChange == lParam)
                {
                    response = "Device ready to use!";
                }

                if (isForCalling)
                    AcquiringStatusChanged?.Invoke(this, new DetectorEventsArgs { Message = response, AcquireMessageParam = lParam, Name = this.Name, Status = this.Status });
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_MSG_UNREG);
            }
        }

    } //  public partial class Detector : IDisposable
} // namespace Regata.Measurements.Devices
