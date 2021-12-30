/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.IO;
using System.ComponentModel;
using CanberraDataAccessLib;

namespace Regata.Core.CNF
{
    public class SpectraInfo
    {
        public SpectraInfo()
        { }
        private IDataAccess _spectra;
        public SpectraInfo(IDataAccess spectra)
        {
            _spectra = spectra;
            ErrorMessage = "";
            Title = spectra.Param[ParamCodes.CAM_T_STITLE].ToString();
            CollectorName = spectra.Param[ParamCodes.CAM_T_SCOLLNAME].ToString();
            Id = spectra.Param[ParamCodes.CAM_T_SIDENT].ToString();
            Units = spectra.Param[ParamCodes.CAM_T_SUNITS].ToString();
            BuildUpType = spectra.Param[ParamCodes.CAM_T_BUILDUPTYPE].ToString();
            Type = spectra.Param[ParamCodes.CAM_T_STYPE].ToString();
            AcqMod = spectra.Param[ParamCodes.CAM_T_ACQMOD].ToString();

            Description = $"{spectra.Param[ParamCodes.CAM_T_SDESC1]} ";
            Description += $"{spectra.Param[ParamCodes.CAM_T_SDESC2]} ";
            Description += $"{spectra.Param[ParamCodes.CAM_T_SDESC3]} ";
            Description += spectra.Param[ParamCodes.CAM_T_SDESC4];

            Geometry = GetParameterValue<float>(ParamCodes.CAM_T_SGEOMTRY);
            IrrBeginDate = GetParameterValue<DateTime?>(ParamCodes.CAM_X_SDEPOSIT);
            IrrEndDate = GetParameterValue<DateTime?>(ParamCodes.CAM_X_STIME);
            AcqStartDate = GetParameterValue<DateTime?>(ParamCodes.CAM_X_ASTIME);
            Uncertainty = GetParameterValue<float>(ParamCodes.CAM_F_SQUANTERR);
            Quantity = GetParameterValue<float>(ParamCodes.CAM_F_SQUANT);
            Duration = GetParameterValue<float>(ParamCodes.CAM_X_EREAL);

        }

        public T GetParameterValue<T>(ParamCodes parCode)
        {
            try
            {
                if (_spectra.IsOpen)
                    return _spectra.Param[parCode].ToString().Convert<T>();
            }
            catch (Exception)
            {
                ErrorMessage += $"Can't convert a parameter '{Enum.GetName(parCode)}'";
            }
            return default(T);
        }

        public string     File            { get; set; }
        public string     Id              { get; private set; }
        public string     Title           { get; private set; }
        public string     CollectorName   { get; private set; }
        public string     Type            { get; private set; }
        public string     AcqMod          { get; private set; }
        public string     BuildUpType     { get; private set; }
        public DateTime?  IrrBeginDate    { get; private set; }
        public DateTime?  IrrEndDate      { get; private set; }
        public DateTime?  AcqStartDate    { get; private set; }
        public float      Duration        { get; private set; }
        public float      DeadTime        { get; set; }
        public float      Geometry        { get; private set; }
        public float      Quantity        { get; private set; }
        public string     Units           { get; private set; }
        public float      Uncertainty     { get; private set; }
        public string     Description     { get; private set; }
        public string     ErrorMessage    { get; private set; }

        public override string ToString()
        {
            return string.Format($"{Id,-15}|{Title,-5}|{CollectorName,-15}|{Type,-5}|{IrrBeginDate,-20}|{IrrEndDate, -20}|{AcqStartDate,-20}|{Quantity,-7}|{Uncertainty,-3}|{Units,-5}|{Geometry,-4}|{Description}");
        }

    }

    public static class StringExtensions
    {
        public static T Convert<T>(this string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFromString(input);
            }
            return default(T);
        }
    }
}
