/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

// Contains methods for loading calibration files by energy and height
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process. 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode.
// ├── DetectorCalibration.cs      --> opened
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
using Regata.Core.Messages;
using System.IO;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        private void AddEfficiencyCalibrationFileByHeight(float height)
        {
            try
            {
                var tmpl = "2,5";
                if (height != 2.5)
                        tmpl = height.ToString();

                string effFileName = Path.Combine(DetSet.EffCalFolder, Name, $"{Name}-eff-{tmpl}.CAL");

                if (!File.Exists(effFileName))
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_EFF_H_FILE_NF));
                    return;
                }

                Report.Notify(new DetectorMessage(Codes.INFO_DET_EFF_H_FILE_ADD)); 
                var effFile = new CanberraDataAccessLib.DataAccess();
                effFile.Open(effFileName);
                effFile.CopyBlock(_device, CanberraDataAccessLib.ClassCodes.CAM_CLS_GEOM);
                effFile.Close();
                _device.Save("", true);
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_EFF_H_FILE_UNREG) { DetailedText = ex.ToString() });
            }
        }

        private void AddEfficiencyCalibrationFileByEnergy()
        {
            try
            {
                string effFileName = Path.Combine(DetSet.EffCalFolder, $"{Name}", $"{Name}-energy.CAL");

                if (!File.Exists(effFileName))
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_EFF_ENG_FILE_NF));
                    return;
                }

                Report.Notify(new DetectorMessage(Codes.INFO_DET_EFF_ENG_FILE_ADD));
                var effFile = new CanberraDataAccessLib.DataAccess();
                effFile.Open(effFileName);
                effFile.CopyBlock(_device, CanberraDataAccessLib.ClassCodes.CAM_CLS_SHAPECALRES);
                effFile.CopyBlock(_device, CanberraDataAccessLib.ClassCodes.CAM_CLS_CALRESULTS);
                effFile.Close();
                _device.Save("", true);
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_EFF_ENG_FILE_UNREG) { DetailedText = ex.ToString() });
            }
        }

    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
