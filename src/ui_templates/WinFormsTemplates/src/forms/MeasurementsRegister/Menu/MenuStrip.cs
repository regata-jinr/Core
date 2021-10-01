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

using RCM = Regata.Core.Messages;
using Regata.Core.Settings;
using Regata.Core.DataBase;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Measurements
{
    public partial class MeasurementsRegisterForm
    {

        public ToolStripMenuItem _goToLLI2;

        private void InitMenuStrip()
        {
            try
            {
                mainForm.LangItem.CheckedChanged += () => Settings<MeasurementRegisterSettings>.CurrentSettings.CurrentLanguage = mainForm.LangItem.CheckedItem;
                mainForm.LangItem.CheckItem(Settings<MeasurementRegisterSettings>.CurrentSettings.CurrentLanguage);

                VerbosityItems.CheckItem(Settings<MeasurementRegisterSettings>.CurrentSettings.Verbosity);

                VerbosityItems.CheckedChanged += () =>
                {
                    Settings<MeasurementRegisterSettings>.CurrentSettings.Verbosity = VerbosityItems.CheckedItem;
                    Labels.SetControlsLabels(mainForm);
                };

                _goToLLI2 = new ToolStripMenuItem() { Name = "GoToLLI2" };
                _goToLLI2.Click += _goToLLI2_Click;


                mainForm.MenuStrip.Items.Insert(0, _goToLLI2);
                mainForm.MenuStrip.Items.Insert(0, VerbosityItems.EnumMenuItem);

                MeasurementsTypeItems.CheckedChanged += () =>
                {


                    if (MeasurementsTypeItems.CheckedItem == Core.DataBase.Models.MeasurementsType.sli)
                    { 
                        mainForm.MainRDGV.Columns["DateTimeFinish"].Visible = false;
                        mainForm.MainRDGV.Columns["DiskPosition"].Visible = false;

                    }
                    else
                    {
                        mainForm.MainRDGV.Columns["DateTimeFinish"].Visible = true;
                        mainForm.MainRDGV.Columns["DiskPosition"].Visible = true;
                    }


                    Labels.SetControlsLabels(mainForm);

                    mainForm.buttonAddAllSamples.Enabled = true;
                    mainForm.buttonAddSelectedSamplesToReg.Enabled = true;
                    mainForm.buttonRemoveSelectedSamples.Enabled = true;

                };
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MENU) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
        }

        private void _goToLLI2_Click(object sender, EventArgs e)
        {
            using (var r = new RegataContext())
            {
                if (!r.MeasurementsRegisters.Where(mr => mr.IrradiationDate == _irrDate && mr.Type == 2).Any())
                    return;
            }
            var f = new MeasurementsRegisterForm(_irrDate, DataBase.Models.MeasurementsType.lli2);
            f.mainForm.Show();

        }
    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
