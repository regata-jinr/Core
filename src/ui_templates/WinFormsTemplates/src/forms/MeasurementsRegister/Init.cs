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

using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Measurements
{
    public partial class MeasurementsRegisterForm : IDisposable
    {
        public  RegisterForm<Measurement> mainForm;
        private EnumItem<MeasurementsType> MeasurementsTypeItems;
        private EnumItem<Status> VerbosityItems;

        private DateTime _irrDate;
        private MeasurementsType _mType;
        
        public MeasurementsRegisterForm(DateTime irrDate, MeasurementsType mType)
        {
            _irrDate = irrDate;
            _mType = mType;
            Settings<MeasurementRegisterSettings>.AssemblyName = "MeasurementsRegister";

            mainForm = new RegisterForm<Measurement>() { Name = "MeasurementsRegisterForm", Text = "MeasurementsRegisterForm" };

            MeasurementsTypeItems = new EnumItem<MeasurementsType>();
            MeasurementsTypeItems.CheckItem(mType);
            VerbosityItems = new EnumItem<Status>();
            mainForm.BottomLayoutPanel.Visible = false;
            mainForm.MainTableLayoutPanel.RowStyles[0].Height = 100F;
            mainForm.MainTableLayoutPanel.Controls.RemoveAt(1);

            mainForm.MainRDGV.RDGV_Set = Settings<MeasurementRegisterSettings>.CurrentSettings.MainTableSettings;

            Settings<MeasurementRegisterSettings>.CurrentSettings.PropertyChanged += (s, e) =>
            {
                Labels.SetControlsLabels(mainForm);
            };

            Report.NotificationEvent += Report_NotificationEvent;

            InitMenuStrip();
            InitStatusStrip();

            using (var r = new DataBase.RegataContext())
            { 
                InitCurrentRegister(r.MeasurementsRegisters.Where(mr => mr.IrradiationDate == irrDate && mr.Type == (int)_mType).FirstOrDefault().Id);
            }

            Labels.SetControlsLabels(mainForm);

            mainForm.Load += MainForm_Load;

            Settings<MeasurementRegisterSettings>.Save();


        }

        private void Report_NotificationEvent(Core.Messages.Message msg)
        {
#if NETFRAMEWORK
            var dict = new Dictionary<Status, MessageBoxIcon>()
            {
                { Status.Error, MessageBoxIcon.Error },
                { Status.Info,  MessageBoxIcon.Information },
                { Status.Success, MessageBoxIcon.Information },
                { Status.Warning, MessageBoxIcon.Warning },
            };
            MessageBox.Show(text: $"{msg.Head}{Environment.NewLine}{msg.Text}", caption: msg.Caption, icon: dict[msg.Status], buttons: MessageBoxButtons.OK);
#else
            PopUpMessage.Show(msg, Settings<MeasurementRegisterSettings>.CurrentSettings.DefaultPopUpMessageTimeoutSeconds);
#endif
        }

        private bool _isDisposed;

        ~MeasurementsRegisterForm()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                mainForm.Dispose();

            }

            _isDisposed = true;
            Settings<MeasurementRegisterSettings>.Save();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Labels.SetControlsLabels(mainForm);
            mainForm.MainRDGV.HideColumns();
            //mainForm.MainRDGV.SetUpReadOnlyColumns();
            mainForm.MainRDGV.SetUpWritableColumns();


            mainForm.MainRDGV.Sorted += MainRDGV_Sorted;

            mainForm.MainRDGV.Columns["DateTimeStart"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            mainForm.MainRDGV.Columns["DateTimeFinish"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";

            var w = mainForm.MainRDGV.Width;
            var typW = 0.05;

            mainForm.MainRDGV.Columns["CountryCode"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["ClientNumber"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["Year"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["SetNumber"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["SetIndex"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["SampleNumber"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["DiskPosition"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["DateTimeStart"].Width = (int)(w * 0.1);
            mainForm.MainRDGV.Columns["Duration"].Width = (int)(w * 0.1);
            mainForm.MainRDGV.Columns["FileSpectra"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["Height"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["Detector"].Width = (int)(w * typW);
            mainForm.MainRDGV.Columns["DeadTime"].Width = (int)(w * typW);

        }

        private void MainRDGV_Sorted(object sender, EventArgs e)
        {
            ColorizeRDGV();
        }

        private void ColorizeRDGVRow(Measurement m, Color clr)
        {
            try
            {
                DataGridViewRow r = mainForm.MainRDGV.Rows.OfType<DataGridViewRow>().Where(rr => (int)rr.Cells["Id"].Value == m.Id).FirstOrDefault();

                if (r == null) return;

                r.DefaultCellStyle.BackColor = clr;
            }
            catch
            {

            }
        }

        private void ColorizeRDGV()
        {
            foreach (var m in mainForm.MainRDGV.CurrentDbSet.Local)
            {
                if (m.FileSpectra != null)
                {
                    ColorizeRDGVRow(m, Color.LightGreen);
                    continue;
                }

                if (m.DateTimeStart.HasValue && !m.DateTimeFinish.HasValue)
                {
                    ColorizeRDGVRow(m, Color.LightYellow);
                    continue;
                }

            }
        }


    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
