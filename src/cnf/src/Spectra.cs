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
using CanberraDataAccessLib;

namespace Regata.Core.CNF
{
    public class Spectra : IDisposable
    {
        private IDataAccess _spectra;

        public readonly string FileName;
        public readonly float DeadTime;

        public string ErrorMessage;

        public readonly bool ReadSuccess;

        public Spectra(string pathToCnf)
        {
            try
            {
                FileName = pathToCnf;
                _spectra = new DataAccess();
                _spectra.Open(pathToCnf, OpenMode.dReadWrite);

                SpectrumData = new SpectraInfo(_spectra);
                SpectrumData.File = Path.GetFileNameWithoutExtension(FileName);
                float ElapsedLiveTime = 0;

                if (!float.TryParse(_spectra.Param[ParamCodes.CAM_X_ELIVE].ToString(), out ElapsedLiveTime))
                    ErrorMessage += "Can't parse ElapsedLiveTime value; ";

                DeadTime = (SpectrumData.Duration == 0f) ? 0f : (float)Math.Round((100 * (1 - ElapsedLiveTime / SpectrumData.Duration)), 2);
                SpectrumData.DeadTime = DeadTime;

                if (string.IsNullOrEmpty(SpectrumData.ErrorMessage))
                    ReadSuccess = true;
                else
                {
                    ReadSuccess = false;
                    ErrorMessage += SpectrumData.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ReadSuccess = false;
            }
            finally
            {
                if (_spectra != null && _spectra.IsOpen)
                    _spectra.Close();
            }
        }


        public readonly SpectraInfo SpectrumData;

        private bool _isDisposed = false;

        private void Dispose(bool isDisposing)
        {

            if (!_isDisposed)
            {
                if (isDisposing)
                {
                }
                if (_spectra != null && _spectra.IsOpen)
                    _spectra.Close();
            }
            _isDisposed = true;
        }

        ~Spectra()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } //  public class Spectra : IDisposable
} // namespace GSI.Core

