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

// The code in this file determines interaction with acquiring spectra file. 
// E.g. filling information about sample. Save file.
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process. 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode.
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> Contains methods for connection, disconnection to the device. Reset connection and so on.
// ├── DetectorFileInteractions.cs --> opened
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by special code.
// |                                    See codes here CanberraDeviceAccessLib.ParamCodes. 
// |                                    Also some of important parameters wrapped into properties
// ├── DetectorProperties.cs       --> Contains description of basics properties, events, enumerations and additional classes
// └── IDetector.cs                --> Interface of the Detector type

using System;
using System.IO;
using System.Linq;
using CanberraDeviceAccessLib;
using Regata.Core.DB.MSSQL.Models;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        /// <summary>
        /// Save current measurement session on the device.
        /// </summary>
        public void Save(string fileName = "")
        {
            try
            {
                if (!_device.IsConnected || Status == DetectorStatus.off)
                    Report.Notify(Codes.ERR_DET_FSAVE_DCON);

                if (string.IsNullOrEmpty(fileName))
                {
                    var _currentDir = Path.Combine(@"D:\Spectra", 
                                                    DateTime.Now.Year.ToString(),
                                                    DateTime.Now.Month.ToString("D2"),
                                                    CurrentMeasurement.Type.ToLower()
                                                   );

                    Directory.CreateDirectory(_currentDir);
                    FullFileSpectraName = Path.Combine(_currentDir, $"{CurrentMeasurement.FileSpectra}.cnf");
                }

                if (File.Exists(FullFileSpectraName))
                {
                    Report.Notify(Codes.ERR_DET_FSAVE_DUPL);
                    FullFileSpectraName = GetUniqueName(FullFileSpectraName);
                }

                _device.Save(FullFileSpectraName);

                if (File.Exists(FullFileSpectraName))
                    Report.Notify(Codes.SUCC_DET_FILE_SAVED);
                else
                {
                    Report.Notify(Codes.ERR_DET_FILE_NOT_SAVED);
                    return;
                }

            }
            catch
            {
                Report.Notify(Codes.ERR_DET_FILE_SAVE_UNREG);
            }
        }

        /// <summary>
        /// Load information about planning measurement to the device
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="irradiation"></param>        
        public void LoadMeasurementInfoToDevice(Measurement measurement, Irradiation irradiation)
        {
            if (!CheckIrradiation(irradiation) || !CheckMeasurement(measurement))
                return;

            Report.Notify(Codes.INFO_DET_LOAD_SMPL_INFO); //$"Set sample {measurement} to detector");

            try
            {
                CurrentMeasurement = measurement;
                RelatedIrradiation = irradiation;
                Sample.SampleKey   = measurement.SampleKey;                  // title
                Sample.Assistant   = measurement.Assistant;                  // operator's name
                Sample.SampleCode     = measurement.SetKey;                     // sample code
                //_device.Param[ParamCodes.CAM_F_SQUANT]      = (double)irradiation.Weight.Value;       // weight
                Sample.Error   = 0;                                      // err = 0
                Sample.WeightUnits     = "gram";                                 // units = gram
                Sample.BuildType = "IRRAD";
                Sample.DateTimeStart   = irradiation.DateTimeStart.Value;        // irr start date time
                Sample.DateTimeFinish    = irradiation.DateTimeFinish.Value;       // irr finish date time
                Sample.StatError    = 0;                                      // Random sample error (%)
                Sample.SysEror    = 0;                                      // Non-random sample error (%)
                Sample.Type     = measurement.Type;
                Sample.Height   = measurement.Height.Value; // height

                Sample.Note = CurrentMeasurement.Note; //filling description field in file
                Counts = measurement.Duration.Value;
                
                AddEfficiencyCalibrationFileByEnergy();

                _device.Save("", true);
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_LOAD_SMPL_INFO_UNREG);
            }
        }

        /// <summary>
        /// Check requred properties is not null
        /// </summary>
        /// <param name="irradiation"></param>
        /// <returns></returns>
        private bool CheckIrradiation(Irradiation irradiation)
        {
            try
            {
                if (irradiation == null)
                {
                    Report.Notify(Codes.ERR_DET_IRR_EMPTY);
                    return false;
                }

                var type = typeof(Irradiation);
                var neededProps = new string[] { "DateTimeStart", "Duration", "Weight" };

                foreach (var pi in type.GetProperties())
                {
                    if (neededProps.Contains(pi.Name) && pi.GetValue(irradiation) == null)
                    {
                        Report.Notify(Codes.ERR_DET_IRR_EMPTY_FLDS);
                        return false;
                    }
                }

                if (!irradiation.DateTimeFinish.HasValue)
                    irradiation.DateTimeFinish = irradiation.DateTimeStart.Value.AddSeconds(irradiation.Duration.Value);

                if (irradiation.DateTimeFinish.Value.TimeOfDay.TotalSeconds == 0)
                    irradiation.DateTimeFinish = irradiation.DateTimeStart.Value.AddSeconds(irradiation.Duration.Value);
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_CHCK_IRR_UNREG);
                return false;
            }
            return true;
        }

        private bool CheckMeasurement(Measurement measurement)
        {
            try
            {
                if (measurement == null)
                {
                    Report.Notify(Codes.ERR_DET_MEAS_EMPTY);
                    return false;
                }


                var type = typeof(Measurement);
                var neededProps = new string[] { "Type", "Duration", "Height" };

                foreach (var pi in type.GetProperties())
                {
                    if (neededProps.Contains(pi.Name) && pi.GetValue(measurement) == null)
                    {
                        Report.Notify(Codes.ERR_DET_MEAS_EMPTY_FLDS);
                        return false;
                    }

                }

                if (string.IsNullOrEmpty(measurement.Detector))
                    measurement.Detector = Name;

                if (measurement.Detector != Name)
                    Report.Notify(Codes.ERR_DET_MEAS_WRONG_DET);


                if (measurement.Duration.Value == 0)
                    Report.Notify(Codes.ERR_DET_MEAS_ZERO_DUR);

            }
            catch
            {
                Report.Notify(Codes.ERR_DET_CHCK_MEAS_UNREG);
                return false;
            }
            return true;
        }

        private string GetUniqueName(string fullFileName)
        {
            string validatedNameInit = Path.GetFileNameWithoutExtension(fullFileName);
            string validatedName = validatedNameInit;
            string folderPath = Path.GetDirectoryName(fullFileName);
            byte tries = 1;
            while (File.Exists(Path.Combine(folderPath, $"{validatedName}.cnf")))
            {
                validatedName = $"{validatedNameInit}({tries++})";
            }
            return Path.Combine(folderPath, $"{validatedName}.cnf");
        }

        public void SetWeight(float wght)
        {
            _device.Param[ParamCodes.CAM_F_SQUANT]      = wght;       // weight
        }

    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
 