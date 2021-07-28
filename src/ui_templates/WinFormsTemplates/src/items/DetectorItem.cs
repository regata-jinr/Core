/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2018-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Regata.Core.Hardware;

namespace Regata.Core.UI.WinForms.Items
{
    public class DetectorItem
    {
        public ToolStripMenuItem DetectorMenuItem;
        public ToolStripStatusLabel DetectorStatusLabel;
        public RadioButton DetectorRadioButton;

        public event Action<string> Checked;
        public event Action<string> UnChecked;

        public string DetectorName => _det.Name;


        private readonly Detector _det;
        private readonly Dictionary<DetectorStatus, System.Drawing.Color> StatusColor;

        public DetectorItem(ref Detector det)
        {
            _det = det;
            DetectorMenuItem    = new ToolStripMenuItem();
            DetectorStatusLabel = new ToolStripStatusLabel();
            DetectorRadioButton = new RadioButton();

            StatusColor = new Dictionary<DetectorStatus, System.Drawing.Color>() { { DetectorStatus.busy, System.Drawing.Color.Yellow }, { DetectorStatus.off, System.Drawing.Color.Gray }, { DetectorStatus.ready, System.Drawing.Color.Green }, { DetectorStatus.error, System.Drawing.Color.Red } };

            DetectorMenuItem.CheckOnClick = true;
            DetectorMenuItem.Click += CheckHandler;

            DetectorStatusLabel.Name = $"{_det.Name}DetectorStatusLabel";
            DetectorStatusLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            Det_StatusChanged(null, EventArgs.Empty);
            
            det.StatusChanged += Det_StatusChanged;

            DetectorMenuItem.Text = _det.Name;
            DetectorMenuItem.Name = $"{_det.Name}Item";
            DetectorRadioButton.Text = _det.Name;
            DetectorRadioButton.Name = $"{_det.Name}rb";
            DetectorRadioButton.AutoSize = true;

        }

        private void Det_StatusChanged(object sender, EventArgs e)
        {
            DetectorStatusLabel.Text = _det.Name;
            DetectorStatusLabel.ToolTipText = _det.Status.ToString();
            DetectorStatusLabel.BackColor = StatusColor[_det.Status];
        }

        private void CheckHandler(object sender, EventArgs e)
        {
            if (DetectorMenuItem.Checked)
                Checked?.Invoke(_det.Name);
            else
                UnChecked?.Invoke(_det.Name);
        }

    } // public class DetectorItem
}     // namespace Regata.Core.UI.WinForms.Items
