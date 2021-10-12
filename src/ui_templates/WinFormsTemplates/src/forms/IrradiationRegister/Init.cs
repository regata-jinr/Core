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

using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms.Items;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister : IDisposable
    {
        // private Timer _timer;
        public RegisterForm<Irradiation> mainForm;
        private EnumItem<IrradiationType> IrradiationTypeItems;
        private EnumItem<Status> VerbosityItems;
        private IrradiationType _irrType;
        private DateTime _irrDateTime;
        private int? _loadNumber;
        private int _uid;
        private ToolStripStatusLabel _userLabel;



        public IrradiationRegister(DateTime dateTime,  IrradiationType irrType, int? loadNumber = null)
        {
            //Settings<IrradiationSettings>.AssemblyName = "IrradiationRegister";

            _loadNumber = loadNumber;
            _irrType = irrType;
            _irrDateTime = dateTime;
            mainForm = new RegisterForm<Irradiation>(tabsNum: 3) { Name = "IrradiationRegister", Text = "IrradiationRegister" };

            // mainForm.Icon = Properties.Resources.MeasurementsLogoCircle2;
            IrradiationTypeItems = new EnumItem<IrradiationType>();
            IrradiationTypeItems.CheckItem(irrType);
            VerbosityItems = new EnumItem<Status>();
            _chosenSamples = new List<Sample>();
            _chosenStandards = new List<Standard>();
            _chosenMonitors = new List<Monitor>();

            Settings<IrradiationSettings>.CurrentSettings.PropertyChanged += (s, e) =>
            {
                Labels.SetControlsLabels(mainForm);
            };

            var u = User.GetUserByLogin(RegataContext.UserLogin);
            if (u == null)
            {
                _uid = 0;
                _userLabel = new ToolStripStatusLabel() { Name = "UnregisteredUser" };
            }
            else
            {
                _uid = u.Id;
                _userLabel = new ToolStripStatusLabel() { Name = u.ToString() };
            }
            mainForm.MainRDGV.RDGV_Set = Settings<IrradiationSettings>.CurrentSettings.MainTableSettings;

            Report.NotificationEvent += Report_NotificationEvent;

            InitMenuStrip();
            InitStatusStrip();
            InitCurrentRegister();
            InitSamplesRegisters();
            InitStandardsRegisters();
            InitMonitorsRegisters();
            InitializeRegFormingControls();
            InitializeIrradiationsParamsControls();

            mainForm.Load += MainForm_Load;

            mainForm.MainRDGV.CurrentDbSet.Local.CollectionChanged += Local_CollectionChanged;

           

            Settings<IrradiationSettings>.Save();
        }

        private async void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_displaySetsParam.Checked)
            {
                await FillSamplesRegisters();
                await FillStandardsRegisters();
                await FillMonitorsRegisters();
                await FillSelectedSamples();
                await FillSelectedStandards();
                await FillSelectedMonitors();
            }
        }

        private void Report_NotificationEvent(Messages.Message msg)
        {
#if NETFRAMEWORK
            var dict = new Dictionary<Status, MessageBoxIcon>() 
            { 
                { Status.Error, MessageBoxIcon.Error },
                { Status.Info,  MessageBoxIcon.Information },
                { Status.Success, MessageBoxIcon.Information },
                { Status.Warning, MessageBoxIcon.Warning }, 
            };
            MessageBox.Show(text: $"{msg.Head}{Environment.NewLine}{msg.Text}", caption: msg.Caption, icon: dict[msg.Status], buttons: MessageBoxButtons.OK );
#else
            PopUpMessage.Show(msg, Settings<IrradiationSettings>.CurrentSettings.DefaultPopUpMessageTimeoutSeconds);
#endif
        }

        private bool _isDisposed;

        ~IrradiationRegister()
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
            Settings<IrradiationSettings>.Save();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await FillSamplesRegisters();
            await FillStandardsRegisters();
            await FillMonitorsRegisters();

            mainForm.buttonAddAllSamples.Enabled = true;
            mainForm.buttonAddSelectedSamplesToReg.Enabled = true;
            mainForm.buttonRemoveSelectedSamples.Enabled = true;
            mainForm.MainRDGV.HideColumns();
            //mainForm.MainRDGV.SetUpReadOnlyColumns();
            mainForm.MainRDGV.SetUpWritableColumns();
            if (_irrType == IrradiationType.sli)
            {
                mainForm.MainRDGV.Columns["DateTimeStart"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
                mainForm.MainRDGV.Columns["DateTimeFinish"].Visible                = false;
                mainForm.MainRDGV.Columns["Container"].Visible                     = false;
                mainForm.MainRDGV.Columns["Position"].Visible                      = false;
                CheckedContainerArrayControl.Visible                               = false;
                controlsMovingInContainer.Visible                                  = false;
            }
            // FIXME: doesn't work properly
            ColorizeDGV(mainForm.MainRDGV);


            Labels.SetControlsLabels(mainForm);
#if !DEBUG
            SetView();
#endif
        }

        private void SetView()
        {
            using (var r = new RegataContext())
            {
                var roles = r.UserRoles;

                if (!roles.Contains("operator") && !roles.Contains("rehandler") && !roles.Contains("db_owner"))
                    mainForm.BottomLayoutPanel.Visible = false;
                }
        }

    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
