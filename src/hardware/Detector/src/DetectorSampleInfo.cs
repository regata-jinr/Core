/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using CanberraDeviceAccessLib;
using Regata.Core.Messages;
using System;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        public class SampleInfo
        {
            private Detector _det;

            internal SampleInfo(Detector det)
            {
                _det = det;
            }

            public string SampleKey
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_STITLE);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_STITLE, value);
                }
            }

            public string Assistant
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_SCOLLNAME);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_SCOLLNAME, value);
                }
            }

            public string SampleCode
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_SIDENT);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_SIDENT, value);
                }
            }

            public float Weight
            {
                get
                {
                    return _det.GetParameterValue<float>(ParamCodes.CAM_F_SQUANT);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_F_SQUANT, value);
                }
            }

            public float Error
            {
                get
                {
                    return _det.GetParameterValue<float>(ParamCodes.CAM_F_SQUANTERR);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_F_SQUANTERR, value);
                }
            }

            public string WeightUnits
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_SUNITS);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_SUNITS, value);
                }
            }


            public string BuildType
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_BUILDUPTYPE);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_BUILDUPTYPE, value);
                }
            }

            public DateTime DateTimeStart
            {
                get
                {
                    return _det.GetParameterValue<DateTime>(ParamCodes.CAM_X_SDEPOSIT);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_X_SDEPOSIT, value);
                }
            }

            public DateTime DateTimeFinish
            {
                get
                {
                    return _det.GetParameterValue<DateTime>(ParamCodes.CAM_X_STIME);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_X_STIME, value);
                }
            }

            public float StatError
            {
                get
                {
                    return _det.GetParameterValue<float>(ParamCodes.CAM_F_SSYSERR);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_F_SSYSERR, value);
                }
            }

            public float SysEror
            {
                get
                {
                    return _det.GetParameterValue<float>(ParamCodes.CAM_F_SSYSTERR);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_F_SSYSTERR, value);
                }
            }

            public string Type
            {
                get
                {
                    return _det.GetParameterValue<string>(ParamCodes.CAM_T_STYPE);
                }
                set
                {
                    _det.SetParameterValue(ParamCodes.CAM_T_STYPE, value);
                }
            }

            public float Height
            {
                get
                {
                    return _det.GetParameterValue<float>(ParamCodes.CAM_T_SGEOMTRY);
                }
                set
                {
                    _det.CurrentMeasurement.Height = value;
                    _det.SetParameterValue(ParamCodes.CAM_T_SGEOMTRY, value);
                    _det.AddEfficiencyCalibrationFileByHeight(value);
                }
            }


            /// <summary>
            /// Spectra file has four row for notes, Each row allows to keep 64 charatcers.
            /// This method divide a big string to these rows.
            /// </summary>
            /// <param name="value">input string</param>
            public string Note
            {

                get
                {
                    return string.Join(" ",
                                            _det.GetParameterValue<string>(ParamCodes.CAM_T_SDESC1),
                                            _det.GetParameterValue<string>(ParamCodes.CAM_T_SDESC2),
                                            _det.GetParameterValue<string>(ParamCodes.CAM_T_SDESC3),
                                            _det.GetParameterValue<string>(ParamCodes.CAM_T_SDESC4)
                                        );
                }

                set
                {
                    if (string.IsNullOrEmpty(value)) return;
                    int descriptionsCount = value.Length / 65;

                    switch (descriptionsCount)
                    {
                        case 0:
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, value);
                            break;

                        case 1:
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, value.Substring(0, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC2, value.Substring(66));
                            break;

                        case 2:
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, value.Substring(0, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC2, value.Substring(66, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC3, value.Substring(132));
                            break;

                        case 3:
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, value.Substring(0, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC2, value.Substring(66, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC3, value.Substring(132, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC4, value.Substring(198));
                            break;

                        default:

                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, value.Substring(0, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC2, value.Substring(66, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC3, value.Substring(132, 65));
                            _det.SetParameterValue(ParamCodes.CAM_T_SDESC4, value.Substring(198, 65));
                            break;
                    }
                }
            } // public string Note

            //NOTE There is an another solution - create empty spectra (save file after new detector added) and make emptyFile.CopyBlock(det, CanberraDataAccessLib.ClassCodes.CAM_CLS_SAMP);
            /// <summary>
            /// Method clears detector sample info block
            /// </summary>
            public void Clear()
            {
                try
                {
                    SampleKey = string.Empty;
                    SampleCode = string.Empty;
                    Assistant = string.Empty;
                    _det.SetParameterValue(ParamCodes.CAM_T_SDESC1, string.Empty);
                    _det.SetParameterValue(ParamCodes.CAM_T_SDESC2, string.Empty);
                    _det.SetParameterValue(ParamCodes.CAM_T_SDESC3, string.Empty);
                    _det.SetParameterValue(ParamCodes.CAM_T_SDESC4, string.Empty);
                    Type = string.Empty;
                    WeightUnits = string.Empty;
                    BuildType = string.Empty;

                    _det.SetParameterValue(ParamCodes.CAM_T_SGEOMTRY, string.Empty);

                    SysEror = 0;
                    StatError = 0;
                    Error = 0;
                    Weight = 0;

                    // TODO how to add empty date?
                    //d.Param[ParamCodes.CAM_X_SDEPOSIT] = string.Empty; // Error: ece99d7c. Windows System Error: Cannot convert BSTR to CSTR.
                    //d.Param[ParamCodes.CAM_X_STIME] = string.Empty; // Error: ece99d7c. Windows System Error: Cannot convert BSTR to CSTR.

                    Report.Notify(new DetectorMessage(Codes.SUCC_DET_CLR_SMPL_INFO));

                }
                catch (Exception ex)
                {
                    Report.Notify(new DetectorMessage(Codes.ERR_DET_CLR_SMPL_INFO_UNREG) {Name = _det.Name, DetailedText =  ex.Message} );
                }
                finally
                {
                    _det._device.Save("", true);
                }

            }

        } // public class SampleInfo

    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
