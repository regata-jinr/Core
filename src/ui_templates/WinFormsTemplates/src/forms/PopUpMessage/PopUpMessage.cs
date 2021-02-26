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

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Regata.Core.DB.MSSQL.Context;
using Regata.Core.DB.MSSQL.Models;
using Regata.Core.Settings;
using RegataCoreMessages = Regata.Core.Messages;

namespace Regata.Core.UI.WinForms.Items
{
    public class PopUpMessage
    {
        private TaskDialogPage _tdp;
        private TaskDialogExpander _tde;
        private TaskDialogFootnote _tdf;
        private TaskDialogProgressBar _tdpbar;

        private Status _status;

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

        public static void Show(RegataCoreMessages.Message msg, ushort autoCloseIntervalSeconds = 15)
        {
            new PopUpMessage(msg, autoCloseIntervalSeconds);
        }

        private PopUpMessage(RegataCoreMessages.Message msg, ushort autoCloseIntervalSeconds)
        {
            _tdp = new TaskDialogPage();
            _tdp.Icon = _statusIcon[msg.Status];
            _tdp.AllowCancel = true;
            _tdp.Caption = msg.Caption;

            FillDefaultElements();

            _tdp.Footnote = _tdf;
            _tdp.Expander = _tde;
                _tde.Text = msg.DetailedText;

            if ((msg.Status == Status.Info || msg.Status == Status.Success) && autoCloseIntervalSeconds != 0)
            {
                _tdpbar = new TaskDialogProgressBar();
                _tdpbar.Maximum = autoCloseIntervalSeconds;
                _tdpbar.State = TaskDialogProgressBarState.Normal;
                _tdp.ProgressBar = _tdpbar;
                _tdp.Created += async (s, ev) =>
                {
                    await foreach (int progressValue in Ticker(TimeSpan.FromSeconds(autoCloseIntervalSeconds)))
                    {
                        _tdpbar.Value = progressValue;
                    }
                    _tdp.BoundDialog?.Close();
                };

            }

            TaskDialog.ShowDialog(_tdp);
        }

        private void FillDefaultElements()
        {
            MessageDefault md;
            using (var rdbc = new RegataContext())
            {
                md = rdbc.MessageDefaults.Where(m => m.Language == GlobalSettings.CurrentLanguage.ToString()).FirstOrDefault();
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

        private static async IAsyncEnumerable<int> Ticker(TimeSpan ts)
        {
            for (int i = 0; i <= ts.TotalSeconds; i += 1)
            {
                yield return i;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

    }  // public class PopUpMessage
}      // namespace Regata.Core.UI.WinForms.Items
