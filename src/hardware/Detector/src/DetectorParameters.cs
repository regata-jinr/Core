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

// Contains methods for getting and setting any parameters by special code.
// See codes here CanberraDeviceAccessLib.ParamCodes. 
// Also some of important parameters wrapped into properties
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process. 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode.
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> Contains methods for connection, disconnection to the device. Reset connection and so on.
// ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring spectra. 
// |                                    E.g. filling information about sample. Save file. 
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> opened
// ├── DetectorProperties.cs       --> Contains description of basics properties, events, enumerations and additional classes
// └── IDetector.cs                --> Interface of the Detector type

using System;
using CanberraDeviceAccessLib;
using System.ComponentModel;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        public T GetParameterValue<T>(ParamCodes parCode)
        {
            try
            {
                return _device.Param[parCode].ToString().Convert<T>();
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_GET_PARAM_UNREG);
                return default(T);
            }
        }

        public void SetParameterValue<T>(ParamCodes parCode, T val)
        {
            try
            {
                if (val == null)
                {
                    Report.Notify(Codes.ERR_DET_SET_NULL_PARAM);
                    return;
                }

                _device.Param[parCode] = val;
                _device.Save("", true);
            }
            catch
            {
                Report.Notify(Codes.ERR_DET_SET_PARAM_UNREG);
            }
        }

        public bool IsHV               => _device.HighVoltage.On;

        public uint PresetRealTime
        {
            get
            {
                
                return GetParameterValue<uint>(ParamCodes.CAM_X_PREAL);
            }
            set
            {
                SetParameterValue(ParamCodes.CAM_X_PREAL, value);
            }
        }

        public uint PresetLiveTime
        {
            get
            {

                return GetParameterValue<uint>(ParamCodes.CAM_X_PLIVE);
            }
            set
            {
                SetParameterValue(ParamCodes.CAM_X_PLIVE, value);
            }
        }

        public float ElapsedRealTime   => GetParameterValue<float>(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL);
        public float ElapsedLiveTime   => GetParameterValue<float>(CanberraDeviceAccessLib.ParamCodes.CAM_X_ELIVE);

        public float DeadTime
        {
            get
            {
                try
                {
                    if (ElapsedRealTime == 0)
                        return 0;
                    else
                        return (float)Math.Round(100 * (1 - (ElapsedLiveTime / ElapsedRealTime)), 2);
                }
                catch
                {
                    Report.Notify(Codes.ERR_DET_GET_DEADT_UNREG);
                    return -1.0f;
                }
            }
        }
    } // public partial class Detector : IDisposable


    public static class StringExtensions
    {
        public static T Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(input);
                }
                Report.Notify(Codes.ERR_DET_SMPL_CNVTR);
                return default(T);
            }
            catch (NotSupportedException)
            {
                Report.Notify(Codes.ERR_DET_SMPL_CNVTR_UNREG);
                return default(T);
            }
        }
    }

}     // namespace Regata.Core.Hardware

