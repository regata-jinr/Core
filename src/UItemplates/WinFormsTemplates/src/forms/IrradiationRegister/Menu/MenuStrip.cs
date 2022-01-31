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
using System;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {
        private ToolStripMenuItem _changeRegisterDate;
        private ToolStripMenuItem _registerParametersMenu;
        private ToolStripMenuItem _displaySetsParam;
        private ToolStripMenuItem _channelMenuStrip;
        public ToolStripMenuItem _samplesToDetectors;

        private void InitMenuStrip()
        {
            try
            {
                mainForm.LangItem.CheckedChanged += () => Settings<IrradiationSettings>.CurrentSettings.CurrentLanguage = mainForm.LangItem.CheckedItem;
                mainForm.LangItem.CheckItem(Settings<IrradiationSettings>.CurrentSettings.CurrentLanguage);

                VerbosityItems.CheckItem(Settings<IrradiationSettings>.CurrentSettings.Verbosity);

                VerbosityItems.CheckedChanged += () =>
                {
                    Settings<IrradiationSettings>.CurrentSettings.Verbosity = VerbosityItems.CheckedItem;
                };

                _changeRegisterDate = new ToolStripMenuItem();
                _changeRegisterDate.Name = "ChangeDateMenu";
                _changeRegisterDate.Click += _changeRegisterDate_Click;

                _samplesToDetectors = new ToolStripMenuItem();
                _samplesToDetectors.Name = "SamplesToDetectors";
                _samplesToDetectors.Click += _samplesToDetectors_Click; ;

                _channelMenuStrip = new ToolStripMenuItem();
                _channelMenuStrip.Name = "ChannelMenuStrip";

                var ch1 = new ToolStripMenuItem() { Name = "1" };
                var ch2 = new ToolStripMenuItem() { Name = "2" };

                ch1.Click += (s, e) => mainForm.MainRDGV.FillDbSetValues("Channel", short.Parse(ch1.Name));
                ch2.Click += (s, e) => mainForm.MainRDGV.FillDbSetValues("Channel", short.Parse(ch2.Name));


                _channelMenuStrip.DropDownItems.Add(ch1);
                _channelMenuStrip.DropDownItems.Add(ch2);

                _registerParametersMenu = new ToolStripMenuItem();
                _registerParametersMenu.Name = "RegisterParametersMenu";

                _displaySetsParam = new ToolStripMenuItem() { Name = "DisplaySetsParam", CheckOnClick = true };

                _displaySetsParam.CheckedChanged += DisplaySetsParam_CheckedChanged;

                _registerParametersMenu.DropDownItems.Add(_displaySetsParam);

                //mainForm.MenuStrip.Items.Insert(0, VerbosityItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Insert(0, _channelMenuStrip);
                mainForm.MenuStrip.Items.Insert(0, _samplesToDetectors);
                mainForm.MenuStrip.Items.Insert(0, _registerParametersMenu);
                mainForm.MenuStrip.Items.Insert(0, _changeRegisterDate);
              
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MENU) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
        }

        private void _samplesToDetectors_Click(object sender, EventArgs e)
        {
            var ln = _loadNumber;
            if (!ln.HasValue) return;
            var sf = new ContainersToDetectorsForm(new string[] { "D1", "D2", "D3", "D4" }, ln.Value);
            sf.Show();
            sf.buttonExportToCSV.Visible = false;
            sf.buttonFillMeasurementRegister.Visible = false;
            sf.buttonExportToExcel.Visible = false;
#if NETFRAMEWORK
            sf.buttonExportToExcel.Visible = true;
            sf.buttonExportToExcel.Click += (s, ee) => { sf.ExportToExcel(mainForm.MainRDGV.CurrentDbSet.Local); };
#endif
        }

        private async void DisplaySetsParam_CheckedChanged(object sender, EventArgs e)
        {
            await FillSamplesRegisters();
            await FillStandardsRegisters();
            await FillMonitorsRegisters();
        }

        private void _changeRegisterDate_Click(object sender, EventArgs e)
        {
            var form = new RegataBaseForm();
           
           var  _calendar = new MonthCalendar();

            _calendar.MaxSelectionCount = 1;

            _calendar.DateSelected += (s, ee) =>
            {
                foreach (var i in mainForm.MainRDGV.Rows.OfType<DataGridViewRow>().Select(c => c.Index).Where(c => c >= 0).Distinct())
                {
                    var m = mainForm.MainRDGV.CurrentDbSet.Where(mm => mm.Id == (int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value).FirstOrDefault();
                    if (m == null) continue;
                    if (!m.DateTimeStart.HasValue)
                        m.DateTimeStart = _calendar.SelectionStart.Date;
                    else
                        m.DateTimeStart = _calendar.SelectionStart.Date + m.DateTimeStart.Value.TimeOfDay;
                    if (m.Duration.HasValue)
                        m.DateTimeFinish = m.DateTimeStart.Value.AddSeconds(m.Duration.Value);
                    mainForm.MainRDGV.CurrentDbSet.Update(m);

                }
                mainForm.MainRDGV.Refresh();
                mainForm.MainRDGV.SaveChanges();
            };
            form.Controls.Add(_calendar);
            form.MinimumSize = new System.Drawing.Size(_calendar.Size.Width, _calendar.Size.Height  + 50);
            form.Size = form.MinimumSize;
            _calendar.Dock = DockStyle.Fill;

            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MenuStrip.Visible = false;
            form.StatusStrip.Visible = false;
            form.Show();
            form.Location = new System.Drawing.Point(mainForm.Location.X, mainForm.Location.Y + mainForm.MenuStrip.Height);

        }
    } // public partial class IrradiationRegister
}     // namespace Regata.Desktop.WinForms.Irradiation
