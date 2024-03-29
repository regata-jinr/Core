﻿/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using RegataCoreMessages = Regata.Core.Messages;

namespace Regata.Core.UI.WinForms.Items
{
#if NET5_0_OR_GREATER
    public class PopUpMessage
    {
        private TaskDialogPage _tdp;
        private TaskDialogExpander _tde;
        private TaskDialogFootnote _tdf;
        private TaskDialogProgressBar _tdpbar;

        private Timer _timer;

        //private Status _status;

        private string Caption      => _tdp.Caption;
        private string Heading      => _tdp.Heading;
        private string Text         => _tdp.Text;
        private string Footer       => _tdp.Footnote.Text;
        private string ExpandedText => _tdp.Expander == null ? "" : _tdp.Expander.Text;

        private readonly IReadOnlyDictionary<Status, TaskDialogIcon> _statusIcon = new Dictionary<Status, TaskDialogIcon>()
            {
                {Status.Info,    TaskDialogIcon.Information},
                {Status.Success, TaskDialogIcon.ShieldSuccessGreenBar},
                {Status.Warning, TaskDialogIcon.ShieldWarningYellowBar},
                {Status.Error,   TaskDialogIcon.ShieldErrorRedBar}
            };

        public static void Show(RegataCoreMessages.Message msg, int autoCloseIntervalSeconds = 15)
        {
            new PopUpMessage(msg, autoCloseIntervalSeconds);
        }

        private PopUpMessage(RegataCoreMessages.Message msg, int autoCloseIntervalSeconds)
        {
            _tdp = new TaskDialogPage();
            _tdp.Icon = _statusIcon[msg.Status];
            _tdp.AllowCancel = true;
            _tdp.Caption = msg.Caption;
            _tdp.SizeToContent = true;

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;


            FillDefaultElements();

            _tdp.Heading = msg.Head;
            _tdp.Text = msg.Text;
            _tdp.Footnote = _tdf;
            _tdp.Expander = _tde;
            _tde.Text = msg.DetailedText;
            
            // autoclosing only for info or success
            if ((msg.Status == Status.Info || msg.Status == Status.Success) && autoCloseIntervalSeconds != 0)
            {
                _tdpbar = new TaskDialogProgressBar();
                _tdpbar.Maximum = autoCloseIntervalSeconds;
                _tdpbar.State = TaskDialogProgressBarState.Normal;
                _tdp.ProgressBar = _tdpbar;
                _tdp.Created += (s, ev) =>
                {
                    _timer.Start();
                };

            }

            TaskDialog.ShowDialog(_tdp);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _tdpbar.Value++;
            if (_tdpbar.Value == _tdpbar.Maximum)
            {
                _timer.Stop();
                _tdp.BoundDialog?.Close();
            }

        }

        private void FillDefaultElements()
        {
            MessageDefault md = null;
            try
            {
                using (var rdbc = new RegataContext())
                {
                    md = rdbc.MessageDefaults.Where(m => m.Language == GlobalSettings.CurrentLanguage.ToString()).FirstOrDefault();
                }
            }
            //FIXME: I would like to use it in Login Form, but I have to remove dependency from DB here.
            catch { }

            if (md == null)
            {
                md = new MessageDefault()
                {
                    FooterText = "Show details",
                    ExpandButtonText = "Show details",
                    HideButtonText = "Hide details"
                };
            }

            _tdf = new TaskDialogFootnote(md.FooterText);
            _tde = new TaskDialogExpander();
            _tde.CollapsedButtonText = md.ExpandButtonText;
            _tde.ExpandedButtonText  = md.HideButtonText;
            _tde.Position = TaskDialogExpanderPosition.AfterFootnote;

        }

        public override string ToString()
        {
            return $"{Caption}{Environment.NewLine}{Heading}{Environment.NewLine}{Text}{Environment.NewLine}{ExpandedText}";
        }


    }  // public class PopUpMessage
#endif
}      // namespace Regata.Core.UI.WinForms.Items
