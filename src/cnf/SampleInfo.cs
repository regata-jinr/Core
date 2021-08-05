/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using CanberraDataAccessLib;

namespace GSI.Core
{
    public struct SampleInfo
    {
        public SampleInfo(IDataAccess spectra)
        {
            ErrorMessage  = "";
            Title         = spectra.Param[ParamCodes.CAM_T_STITLE].ToString();
            CollectorName = spectra.Param[ParamCodes.CAM_T_SCOLLNAME].ToString();
            Id            = spectra.Param[ParamCodes.CAM_T_SIDENT].ToString();
            Quantity      = float.NaN;
            Uncertainty   = float.NaN;
            Units         = spectra.Param[ParamCodes.CAM_T_SUNITS].ToString();
            BuildUpType   = spectra.Param[ParamCodes.CAM_T_BUILDUPTYPE].ToString();
            IrrBeginDate  = DateTime.MinValue;
            IrrEndDate    = DateTime.MinValue;
            AcqStartDate  = DateTime.MinValue;
            Type          = spectra.Param[ParamCodes.CAM_T_STYPE].ToString();
            AcqMod        = spectra.Param[ParamCodes.CAM_T_ACQMOD].ToString();
            Geometry      = float.NaN;
            
            Description   = $"{spectra.Param[ParamCodes.CAM_T_SDESC1]} ";
            Description   += $"{spectra.Param[ParamCodes.CAM_T_SDESC2]} ";
            Description   += $"{spectra.Param[ParamCodes.CAM_T_SDESC3]} ";
            Description   += spectra.Param[ParamCodes.CAM_T_SDESC4];

            if (!float.TryParse(spectra.Param[ParamCodes.CAM_T_SGEOMTRY].ToString(), System.Globalization.NumberStyles.Any, null, out Geometry))
                ErrorMessage += "Can't parse geometry value; ";
            if (!DateTime.TryParse(spectra.Param[ParamCodes.CAM_X_STIME].ToString(), out IrrEndDate))
                ErrorMessage += "Can't parse end of irradiation date value; ";
            if (!DateTime.TryParse(spectra.Param[ParamCodes.CAM_X_SDEPOSIT].ToString(), out IrrBeginDate))
                ErrorMessage += "Can't parse begin of irradiation date value; ";
            if (!DateTime.TryParse(spectra.Param[ParamCodes.CAM_X_ASTIME].ToString(), out AcqStartDate))
                ErrorMessage += "Can't parse start of acquisition date value; ";
            if (!float.TryParse(spectra.Param[ParamCodes.CAM_F_SQUANTERR].ToString(), out Uncertainty))
                ErrorMessage += "Can't parse uncertainty value; ";
            if (!float.TryParse(spectra.Param[ParamCodes.CAM_F_SQUANT].ToString(), out Quantity))
                ErrorMessage += "Can't parse quantity value;";
            if (!float.TryParse(spectra.Param[ParamCodes.CAM_X_EREAL].ToString(), out Duration))
                ErrorMessage += "Can't parse Duration value; ";
        }

        public readonly string   Id;
        public readonly string   Title;
        public readonly string   CollectorName;
        public readonly string   Type;
        public readonly string   AcqMod;
        public readonly string   Description;
        public readonly string   ErrorMessage;
        public readonly float    Quantity;
        public readonly float    Uncertainty;
        public readonly float    Duration;
        public readonly string   Units;
        public readonly float    Geometry;
        public readonly string   BuildUpType;
        public readonly DateTime IrrBeginDate;
        public readonly DateTime IrrEndDate;
        public readonly DateTime AcqStartDate;

        public override string ToString()
        {
            return String.Format($"{Id,-15}|{Title,-5}|{CollectorName,-15}|{Type,-5}|{IrrBeginDate,-20}|{IrrEndDate, -20}|{AcqStartDate,-20}|{Quantity,-7}|{Uncertainty,-3}|{Units,-5}|{Geometry,-4}|{Description}");
        }

    }
}
