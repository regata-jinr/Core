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
using Regata.Core.Cloud;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Measurements
{
    public partial class MeasurementsRegisterForm
    {

        public ToolStripMenuItem _goToLLI2;
        private ToolStripMenuItem _downloadpectraMenuItem;
        private FolderBrowserDialog _folderDialog;


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

                _folderDialog = new FolderBrowserDialog();

                _downloadpectraMenuItem = new ToolStripMenuItem();
                _downloadpectraMenuItem.Name = "DownloadpectraMenuItem";
                _downloadpectraMenuItem.Click += _downloadpectraMenuItem_Click;

                if (MeasurementsTypeItems.CheckedItem == Core.DataBase.Models.MeasurementsType.lli1)
                    mainForm.MenuStrip.Items.Insert(0, _goToLLI2);
                mainForm.MenuStrip.Items.Insert(0, _downloadpectraMenuItem);
                //mainForm.MenuStrip.Items.Insert(0, VerbosityItems.EnumMenuItem);

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

        private async void _downloadpectraMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainForm.MainRDGV == null || mainForm.MainRDGV.SelectedCells.Count <= 0) return;

                if (_folderDialog.ShowDialog() != DialogResult.OK) return;

                var _cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));

                mainForm.ProgressBar.Value = 0;
                mainForm.ProgressBar.Maximum = mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().Count();

                foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                {
                    var fileS = mainForm.MainRDGV.Rows[i].Cells["FileSpectra"].Value.ToString();

                    if (string.IsNullOrEmpty(fileS))
                        continue;
                    using (var rc = new RegataContext())
                    {
                        var sharedSpectra = rc.SharedSpectra.Where(ss => ss.fileS == fileS).FirstOrDefault();
                        if (sharedSpectra == null)
                            continue;
                        await WebDavClientApi.DownloadFileAsync(sharedSpectra.token, Path.Combine(_folderDialog.SelectedPath, MeasurementsTypeItems.CheckedItem.ToString(), $"{fileS}.cnf"), _cts.Token);
                        mainForm.ProgressBar.Value++;

                    }

                }
            }
            catch (TaskCanceledException)
            { }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_WF_IRR_REG_DWNL_SPECTRA_UNREG) { DetailedText = ex.ToString() });
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
