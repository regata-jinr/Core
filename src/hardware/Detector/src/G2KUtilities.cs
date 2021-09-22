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

using System;
using System.Threading.Tasks;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        public static async Task<ProcessResult> RunMvcgAsync()
        {
            return await ExecuteShellCommandAsync("putview.exe", @"/CXCY=-100,-100 /NO_DATASRC");
        }

        public static async Task<ProcessResult> CloseMvcgAsync()
        {
            return await ExecuteShellCommandAsync("endview.exe");
        }

        public static async Task<ProcessResult> ShowDetectorInMvcgAsync(string det)
        {
            return await ExecuteShellCommandAsync("pvopen.exe", $"DET:{det} /READ_ONLY");
        }

        public static async Task<ProcessResult> SelectDetectorAsync(string det)
        {
            return await ExecuteShellCommandAsync("pvselect.exe", $"DET:{det}");
        }

        public static async Task<ProcessResult> CloseDetectorAsync(string det)
        {
            return await ExecuteShellCommandAsync("pvclose.exe", $"DET:{det}");
        }

        private static async Task<ProcessResult> ExecuteShellCommandAsync(string command, string arguments = "")
        {
            return await Shell.ExecuteCommandAsync(command, arguments, @"C:\GENIE2K\EXEFILES");
        }

    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
