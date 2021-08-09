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
using System.Threading.Tasks;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Messages;

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
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_FSAVE_DCON));
                    return;
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    var _currentDir = Path.Combine(@"D:\Spectra", 
                                                    DateTime.Now.Year.ToString(),
                                                    DateTime.Now.Month.ToString("D2"),
                                                    Measurement.TypeToString[(MeasurementsType)CurrentMeasurement.Type].ToLower()
                                                   );

                    Directory.CreateDirectory(_currentDir);
                    if (string.IsNullOrEmpty(CurrentMeasurement.FileSpectra))
                        FullFileSpectraName = Path.Combine(_currentDir, $"{CurrentMeasurement}.cnf");
                    else 
                        FullFileSpectraName = Path.Combine(_currentDir, $"{CurrentMeasurement.FileSpectra}.cnf");
                }

                if (File.Exists(FullFileSpectraName))
                {
                    Report.Notify(new DetectorMessage(Codes.WARN_DET_FSAVE_DUPL));
                    FullFileSpectraName = GetUniqueName(FullFileSpectraName);
                }

                _device.Save(FullFileSpectraName);

                if (File.Exists(FullFileSpectraName))
                {
                    Report.Notify(new DetectorMessage(Codes.SUCC_DET_FILE_SAVED));
                }
                else
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_FILE_NOT_SAVED));
                    return;
                }

            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_FILE_SAVE_UNREG) { DetailedText = ex.ToString() });
            }
        }

        /// <summary>
        /// Load information about planning measurement to the device
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="irradiation"></param>        
        public void LoadMeasurementInfoToDevice(Measurement measurement, Irradiation irradiation)
        {
            if (!CheckIrradiation(irradiation) && !CheckMeasurement(measurement))
                return;

            Report.Notify(new DetectorMessage(Codes.INFO_DET_LOAD_SMPL_INFO)); //$"Set sample {measurement} to detector"));

            Sample.Clear();

            try
            {
                CurrentMeasurement = measurement;
                RelatedIrradiation = irradiation;
                Sample.SampleKey = measurement.SampleKey;                  // title
                Sample.Assistant = CurrentUser; // operator's name. for operators we have separate logon system despite on windows login
                Sample.SampleCode = measurement.SetKey;                     // sample code
                Sample.Error = 0;                                      // err = 0
                Sample.WeightUnits = "gram";                                 // units = gram
                Sample.BuildType = "IRRAD";
                Sample.DateTimeStart = irradiation.DateTimeStart.Value;        // irr start date time
                Sample.DateTimeFinish = irradiation.DateTimeFinish.Value;       // irr finish date time
                Sample.StatError = 0;                                      // Random sample error (%)
                Sample.SysEror = 0;                                      // Non-random sample error (%)
                Sample.Type = Measurement.TypeToString[(MeasurementsType)measurement.Type];
                Sample.Height = measurement.Height.Value; // height
                Sample.Note = CurrentMeasurement.Note; //filling description field in file

                if (measurement.Year == "s")
                {
                    Sample.Weight = measurement.Type switch
                    {
                        0 => Data.GetSrmOrMonitor<Standard>(measurement.ToString()).SLIWeight ?? 0,
                        _ => Data.GetSrmOrMonitor<Standard>(measurement.ToString()).LLIWeight ?? 0
                    };
                }
                else if (measurement.Year == "m")
                {
                    Sample.Weight = measurement.Type switch
                    {
                        0 => Data.GetSrmOrMonitor<Monitor>(measurement.ToString()).SLIWeight ?? 0,
                        _ => Data.GetSrmOrMonitor<Monitor>(measurement.ToString()).LLIWeight ?? 0
                    };
                }
                else
                {
                    Sample.Weight = measurement.Type switch
                    {
                        0 => Data.GetSampleSLIWeight(measurement) ?? 0,
                        _ => Data.GetSampleLLIWeight(measurement) ?? 0
                    };
                }
                Counts = measurement.Duration.Value;

                AddEfficiencyCalibrationFileByEnergy();

            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_LOAD_SMPL_INFO_UNREG) { DetailedText = ex.ToString() });
            }
            finally
            {
                _device.Save("", true);
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
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_IRR_EMPTY));
                    return false;
                }

                var type = typeof(Irradiation);
                var neededProps = new string[] { "DateTimeStart", "Duration" };

                foreach (var pi in type.GetProperties())
                {
                    if (neededProps.Contains(pi.Name) && pi.GetValue(irradiation) == null)
                    {
                        Report.Notify(new DetectorMessage(Codes.ERR_DET_IRR_EMPTY_FLDS));
                        return false;
                    }
                }

                if (!irradiation.DateTimeFinish.HasValue)
                    irradiation.DateTimeFinish = irradiation.DateTimeStart.Value.AddSeconds(irradiation.Duration.Value);

                if (irradiation.DateTimeFinish.Value.TimeOfDay.TotalSeconds == 0)
                    irradiation.DateTimeFinish = irradiation.DateTimeStart.Value.AddSeconds(irradiation.Duration.Value);
            }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CHCK_IRR_UNREG) { DetailedText = ex.ToString() });
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
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_MEAS_EMPTY));
                    return false;
                }


                var type = typeof(Measurement);
                var neededProps = new string[] { "Type", "Duration", "Height" };

                foreach (var pi in type.GetProperties())
                {
                    if (neededProps.Contains(pi.Name) && pi.GetValue(measurement) == null)
                    {
                        Report.Notify(new DetectorMessage(Codes.ERR_DET_MEAS_EMPTY_FLDS));
                        return false;
                    }

                }

                if (string.IsNullOrEmpty(measurement.Detector))
                    measurement.Detector = Name;

                if (measurement.Detector != Name)
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_MEAS_WRONG_DET));
                    return false;
                }


                if (measurement.Duration.Value == 0)
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_MEAS_ZERO_DUR));
                    return false;
                }

                }
            catch (Exception ex)
            {
                Report.Notify(new DetectorMessage(Codes.ERR_DET_CHCK_MEAS_UNREG) { DetailedText = ex.ToString() });
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

        /// <summary>
        /// Generator of unique name of file spectra
        /// Name of file spectra should be unique and it has constraint in data base
        /// There is an algorithm:
        /// For the specified type it determines maximum of spectra number from the numbers that might be converted to integer number
        /// Then it choose the max number and convert it to string using next code:
        /// First digit of name spectra is the digit from the name of detector
        /// Second digit is number of type - {SLI - 0} {LLI-1 - 1} {LLI-2 - 2}
        /// The next five digits is number of spectra
        /// Typical name of spectra file: 1006261 means
        /// The spectra was acquried on detector 'D1' it was SLI type and it has a number 6261.
        /// **Pay attention that beside FileSpectra filed in MeasurementInfo**
        /// **each Detector has a property with FullName that included path on local storage**
        /// <see cref="Detector.FullFileSpectraName"/>
        /// </summary>
        /// <param name="detName">Name of detector which save acquiring session to file</param>
        /// <returns>Name of spectra file</returns>

        public static async Task<string> GenerateSpectraFileNameFromDBAsync(string dName, int type)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (var r = new RegataContext())
                    {
                        var spectras = r.Measurements.Where(m => (
                                                                   m.FileSpectra.Length == 7 &&
                                                                   m.Type == type &&
                                                                   m.FileSpectra.Substring(0, 1) == dName.Substring(1, 1)
                                                                )
                                                        )
                                                  .Select(m => new
                                                  {
                                                      FileNumber = m.FileSpectra.Substring(2, 5)
                                                  }
                                                         )
                                                  .ToArray();

                        int maxNumber = spectras.Where(s => int.TryParse(s.FileNumber, out _)).Select(s => int.Parse(s.FileNumber)).Max();


                        return $"{dName.Substring(1, 1)}{type}{(++maxNumber).ToString("D5")}";
                    }
                }
                catch (Exception ex)
                {
                    Report.Notify(new DetectorMessage(Codes.WARN_DET_FSAVE_NOT_UNIQ_DB) { DetailedText = ex.ToString() });
                    return await GenerateSpectraFileNameFromLocalStorageAsync(dName, type);
                }
            });
        }

        private static bool IsNumber(string str)
        {
            return int.TryParse(str, out _);
        }


        public static async Task<string> GenerateSpectraFileNameFromLocalStorageAsync(string dName, int type)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(@"D:\Spectra"))
                        throw new Exception("Spectra Directory doesn't exist");

                    var dir = new DirectoryInfo(@"D:\Spectra");
                    var files = dir.GetFiles("*.cnf", SearchOption.AllDirectories).Where(f => f.CreationTime >= DateTime.Now.AddDays(-30)).ToList();

                    int maxNumber = files.Where(f =>
                                                f.Name.Length == 11 &&
                                                f.Name.Substring(1, 1) == type.ToString() &&
                                                IsNumber(Path.GetFileNameWithoutExtension(f.Name)) &&
                                                f.Name.Substring(0, 1) == dName.Substring(1, 1)
                                           ).
                                      Select(f => new
                                      {
                                          FileNumber = int.Parse(f.Name.Substring(2, 5))
                                      }
                                            ).
                                      Max(f => f.FileNumber);

                    return $"{dName.Substring(1, 1)}{type}{(++maxNumber).ToString("D5")}";
                }
                catch (Exception ex)
                {
                    Report.Notify(new DetectorMessage(Codes.WARN_DET_FSAVE_NOT_UNIQ_LCL) { DetailedText = ex.ToString() });
                    return "";
                }
            });
        }


    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
 