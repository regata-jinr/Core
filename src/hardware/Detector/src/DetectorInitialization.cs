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

/*
 * 
 * Contains constructor of type, destructor and additional parameters. Like Status enumeration
 * Events arguments and so on
 * Detector class divided by few files:
 * 
 * ├── DetectorAcquisition.cs      --> Contains methods that allow to control spectra acquisition 
 * |                                   process.Start, stop, pause, clear acquisition process and
 * |                                   also specify acquisition mode.
 * ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and 
 * |                                   height
 * ├── DetectorConnection.cs       --> Contains methods for connection, disconnection to the device.
 * |                                   Reset connection and so on.
 * ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring
 * |                                   spectra. 
 * |                                    E.g. filling information about sample. Save file.
 * ├── DetectorInitialization.cs   --> opened
 * ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by
 * |                                   special code.
 * |                                    See codes here CanberraDeviceAccessLib.ParamCodes. 
 * |                                    Also some of important parameters wrapped into properties
 * └── DetectorProperties.cs       --> Contains description of basics properties, events, 
 *                                     enumerations and additional classes
 */

using System;
using System.IO;
using CanberraDeviceAccessLib;
using Regata.Core.DataBase.Models;
using Regata.Core.Messages;

namespace Regata.Core.Hardware
{
    /// <summary>
    /// Detector class take control of real detector and has protection from crashes, resources manager, convinient abstractions of many operations and parameters. You can start, stop and do any basics operations which you have with detector via mvcg.exe. This software based on dlls provided by [Genie2000] (https://www.mirion.com/products/genie-2000-basic-spectroscopy-software) for interactions with [HPGE](https://www.mirion.com/products/standard-high-purity-germanium-detectors) detectors also from [Mirion Tech.](https://www.mirion.com). Personally we are working with [Standard Electrode Coaxial Ge Detectors](https://www.mirion.com/products/sege-standard-electrode-coaxial-ge-detectors)
    /// </summary>
    /// <seealso cref="https://www.mirion.com/products/genie-2000-basic-spectroscopy-software"/>
    public partial class Detector : IDisposable
    {
        /// <summary>Constructor of Detector class.</summary>
        /// <param name="name">Name of detector. Without path.</param>
        /// <param name="currentUser"> Name of current user of the program.</param>
        /// <param name="option">CanberraDeviceAccessLib.ConnectOptions {aReadWrite, aContinue, aNoVerifyLicense, aReadOnly, aTakeControl, aTakeOver}.By default ConnectOptions is ReadWrite.</param>
        public Detector(string name, string currentUser = "")
        {
            try
            {

                DetSet.ConnectOption = ConnectOptions.aReadWrite;
                _isDisposed = false;
                Status = DetectorStatus.off;
                ErrorMessage = "";
                _device = new DeviceAccessClass();
                CurrentMeasurement = new Measurement();
                DetSet.EffCalFolder = @"C:\GENIE2K\CALFILES\Efficiency";

                if (!Directory.Exists(DetSet.EffCalFolder) || Directory.GetFiles(DetSet.EffCalFolder).Length == 0)
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_EFF_DIR_EMPTY));

                if (DetectorExists(name))
                    DetSet.Name = name;
                else
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_NAME_N_EXST));
                    return;
                }

                _device.DeviceMessages += DeviceMessagesHandler;
                DetSet.ConnectionTimeOut = TimeSpan.FromSeconds(10);
                IsPaused = false;

                Connect();

                AcquisitionMode = AcquisitionModes.aCountToRealTime;
                Sample = new SampleInfo(this);

                CurrentUser = currentUser;

                if (string.IsNullOrEmpty(CurrentUser))
                    CurrentUser = Settings.GlobalSettings.User;

            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CTOR_UNREG) { DetailedText = ex.ToString() });
            }
        }

        private void CleanUp(bool isDisposing)
        {
            Report.Notify(new DetectorMessage(Codes.INFO_DET_CLN));

            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    NLog.LogManager.Flush();
                }
                Disconnect();
            }
            _isDisposed = true;
        }

        ~Detector()
        {
            CleanUp(false);
        }

        /// <summary>
        /// Disconnects from detector. Changes status to off. Resets ErrorMessage. Clears the detector.
        /// </summary>
        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Power reset for the detector. 
        /// </summary>
        public void Reset()
        {
            // FIXME: not tested
            try
            {
                Report.Notify(new DetectorMessage(Codes.INFO_DET_RST));
                _device.SendCommand(DeviceCommands.aReset);

                if (Status == DetectorStatus.ready)
                    Report.Notify(new DetectorMessage(Codes.SUCC_DET_RST));
                else
                    Report.Notify(new DetectorMessage(Codes.WARN_DET_RST));
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_RST_UNREG) { DetailedText = ex.ToString() });
            }
        }

        public static bool IsDetectorAvailable(string name)
        {
            try
            {
                var dev = new DeviceAccess();
                dev.Connect(name);
                if (dev.IsConnected)
                {
                    dev.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_AVAIL_UNREG) { DetailedText = ex.ToString() });
            }
            return false;
        }

    } //  public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
