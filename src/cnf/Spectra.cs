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
using System.IO;
using CanberraDataAccessLib;

namespace GSI.Core
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

                Sample = new SampleInfo(_spectra);
                float ElapsedLiveTime = 0;

                if (!float.TryParse(_spectra.Param[ParamCodes.CAM_X_ELIVE].ToString(), out ElapsedLiveTime))
                    ErrorMessage += "Can't parse ElapsedLiveTime value; ";
                
                DeadTime = (Sample.Duration == 0f) ? 0f : (float)Math.Round((100 * (1 - ElapsedLiveTime / Sample.Duration)), 2);

                if (string.IsNullOrEmpty(Sample.ErrorMessage))
                    ReadSuccess = true;
                else
                {
                    ReadSuccess = false;
                    ErrorMessage += Sample.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ReadSuccess = false;
            }
            finally
            {
                Dispose(true);
            }

        }

        // FIXME: it smells...
        public override string ToString()
        {
            if (ReadSuccess)
                return String.Format($"{Path.GetFileNameWithoutExtension(FileName),7}{Sample.Id,-15}|{Sample.Title,-5}|{Sample.CollectorName,-15}|{Sample.Type,-5}|{Sample.IrrBeginDate,-20}|{Sample.IrrEndDate,-20}|{Sample.AcqStartDate,-20}|{Sample.Quantity,-7}|{Sample.Uncertainty,-3}|{Sample.Units,-5}|{Sample.Geometry,-4}|{Sample.Description}|{ReadSuccess,5}");

            return String.Format($"{Path.GetFileNameWithoutExtension(FileName),7}{Sample.Id,-15}|{Sample.Title,-5}|{Sample.CollectorName,-15}|{Sample.Type,-5}|{Sample.IrrBeginDate,-20}|{Sample.IrrEndDate,-20}|{Sample.AcqStartDate,-20}|{Sample.Quantity,-7}|{Sample.Uncertainty,-3}|{Sample.Units,-5}|{Sample.Geometry,-4}|{Sample.Description}|{ReadSuccess,5}|{ErrorMessage}");
        }

        public ViewModel viewModel
        {
            get
            {
                return new ViewModel
                {
                    File          = Path.GetFileNameWithoutExtension(this.FileName),
                    Id            = this.Sample.Id,
                    Title         = this.Sample.Title,
                    CollectorName = this.Sample.CollectorName,
                    Type          = this.Sample.Type,
                    AcqMod       = this.Sample.AcqMod,
                    Quantity      = this.Sample.Quantity,
                    Uncertainty   = this.Sample.Uncertainty,
                    Units         = this.Sample.Units,
                    Geometry      = this.Sample.Geometry,
                    Duration      = this.Sample.Duration,
                    DeadTime      = DeadTime,
                    BuildUpType   = this.Sample.BuildUpType,
                    IrrBeginDate  = this.Sample.IrrBeginDate,
                    IrrEndDate    = this.Sample.IrrEndDate,
                    AcqStartDate  = this.Sample.AcqStartDate,
                    Description   = this.Sample.Description,
                    ReadSuccess   = this.ReadSuccess,
                    ErrorMessage  = this.ErrorMessage,
                };
            }
        }

        public readonly SampleInfo Sample;

        private bool _isDisposed = false;

        private void Dispose(bool isDisposing)
        {

            if (!_isDisposed)
            {
                if (isDisposing)
                {
                }
                if (_spectra.IsOpen)
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

