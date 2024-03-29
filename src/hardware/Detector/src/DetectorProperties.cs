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


// Contains description of basics properties, events, enumerations and additional classes
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process. 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode.
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> Contains methods for connection, disconnection to the device. Reset connection and so on.
// ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring spectra. 
// |                                    E.g. filling information about sample. Save file. 
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by special code.
// |                                    See codes here CanberraDeviceAccessLib.ParamCodes. 
// |                                    Also some of important parameters wrapped into properties
// ├── DetectorProperties.cs       --> opened
// ├── G2KUtilities.cs             --> Contains aliases for running utilities from GENIE2K/EXEFILES.
// └── IDetector.cs                --> Interface of the Detector type


using System;
using System.Linq;
using System.Collections.Generic;
using CanberraDeviceAccessLib;
using Regata.Core.Hardware;
using Regata.Core.Messages;
using Regata.Core.DataBase.Models;

namespace Regata.Core.Hardware
{
    /// <summary>
    ///  Enumeration of possible detector's working statuses
    ///  ready - Detector is enabled and ready for acquiring
    ///  off   - Detector is disabled
    ///  busy  - Detector is acquiring spectrum
    ///  error - Detector has porblems
    /// </summary>
    public enum DetectorStatus { off, ready, busy, error }

    public partial class Detector : IDisposable
    {
        private readonly         DeviceAccess  _device;
        private bool             _isDisposed;
        private DetectorStatus   _status;

        public Measurement CurrentMeasurement   { get; private set; }
        public string      CurrentUser          { get; set; }
        public Irradiation RelatedIrradiation   { get; private set; }
        public string      FullFileSpectraName  { get; private set; }

        private IReadOnlyDictionary<string, int> PairedXemoSN = new Dictionary<string, int>()
        {
            { "D1", 107374 },
            { "D2", 107375 },
            { "D3", 107376 },
            { "D4", 114005 },
        };

        public SampleChanger PairedXemoDevice;
        public bool IsXemoEnabled {get; private set; }

        public DetectorSettings DetSet;
        public SampleInfo Sample;
        public bool   IsConnected => _device.IsConnected;
        public string Name => DetSet.Name;

        public event  EventHandler                     StatusChanged;
        public event  EventHandler<DetectorEventsArgs> AcquiringStatusChanged;
        public event  Action<Detector>                 AcquireDone;
        public event  Action<Detector>                 AcquireStart;
        public event  Action<Detector>                 HardwareError;
        public event  Action<Detector>                 ParamChange;

        private bool DetectorExists(string name)
        {
            try
            {
                var detsList = (IEnumerable<object>)_device.ListSpectroscopyDevices;
                if (detsList.Contains(name))
                {
                    Report.Notify(new DetectorMessage(Codes.INFO_DET_NAME_EXSTS) { DetailedText = Name });
                    return true;
                }
                else
                {
                    Status = DetectorStatus.error;
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_NAME_EXSTS) { DetailedText = Name }) ;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_NAME_EXSTS_UNREG) { DetailedText = ex.ToString() });
                return false;
            }
        }

        /// <summary> Returns status of detector. {ready, off, busy, error}. </summary>
        /// <seealso cref="Enum Status"/>
        public DetectorStatus Status
        {
            get { return _status; }

            private set
            {
                if (_status != value)
                {
                    Report.Notify(new DetectorMessage(Codes.INFO_DET_CHNG_STATUS));
                    _status = value;
                    StatusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Returns error message.
        /// </summary>
        public string ErrorMessage { get; private set; }

    } //  public partial class Detector : IDisposable

    /// <summary>
    /// This class shared information about events occured on the detector between callers.
    /// </summary>
    public class DetectorEventsArgs : EventArgs
    {
        public string Name;
        public DetectorStatus Status;
        public int AcquireMessageParam;
        public string Message;
    }

} // namespace Regata.Measurements.Devices
