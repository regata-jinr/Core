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

using Regata.Core.DataBase;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class SamplesToDetectorsForm : Form
    {
        private readonly int _loadNumber;

        public readonly int? MaxContNumber;

        private ControlsGroupBox _detsCheckedArray;

        private readonly Dictionary<int, bool> _assignedContainers;

        public SamplesToDetectorsForm(string[] dets, int loadNumber)
        {
            InitializeComponent();

            _loadNumber = loadNumber;

            _assignedContainers = new Dictionary<int, bool>();

            using (var rc = new RegataContext())
            {
                MaxContNumber = rc.Irradiations.Where(ir => ir.LoadNumber == _loadNumber).Select(ir => ir.Container).Max();
            }

            if (!MaxContNumber.HasValue)
                throw new ArgumentNullException($"Irradiation register with loadNumber = {loadNumber} has samples with empty container record.");

            foreach (var ic in Enumerable.Range(1, MaxContNumber.Value))
                _assignedContainers.Add(ic, false);

            FillDetectorsRow(dets);
            FillButtonsRow();
            ResumeLayouts();

            Labels.SetControlsLabels(this);
        }

        private void FillDetectorsRow(string[] dets)
        {
            var chacs = new CheckedArrayControl<int>[dets.Length];
            for (int i = 0; i < chacs.Length; ++i)
            {
                chacs[i] = new CheckedArrayControl<int>(_assignedContainers.Keys.ToArray(), multiSelection: true) 
                { FlowDirection = FlowDirection.TopDown, Name = dets[i], Text = dets[i]};
                chacs[i].SelectionChanged += SamplesToDetectorsForm_SelectionChanged;
            }

            _detsCheckedArray = new ControlsGroupBox(chacs, vertical: false) { Dock = DockStyle.Fill };

            tableLayoutPanelMain.Controls.Add(_detsCheckedArray, 0, 1); 
        }

        private void FillButtonsRow()
        {
            tableLayoutPanelMain.Controls.Add(new ControlsGroupBox(new Control[] { buttonExportToCSV, buttonExportToCSV, buttonFillMeasurementRegister }, vertical: false), 0, 2);
        }

        private CheckedArrayControl<int> CreateArrayCheckBox(string dName)
        {
            return new CheckedArrayControl<int>(Enumerable.Range(1, MaxContNumber.Value).ToArray(), multiSelection: true) { FlowDirection = FlowDirection.TopDown};
        }

        private void SamplesToDetectorsForm_SelectionChanged(CheckedArrayControl<int> sender)
        {
            foreach (var iv in _assignedContainers.Keys)
            {
                var tempBool = false;
                foreach (var dc in _detsCheckedArray._controls.OfType<CheckedArrayControl<int>>())
                {
                    tempBool = tempBool || dc.IsSelected(iv);
                }
                    _assignedContainers[iv] = tempBool;
            }

            foreach (var dc in _detsCheckedArray._controls.OfType<CheckedArrayControl<int>>())
            {
                if (dc == sender)
                    continue;

                foreach (var ac in _assignedContainers.Keys)
                {
                    if (dc.IsSelected(ac)) continue;

                    if (_assignedContainers[ac])
                        dc.Hide(ac);
                    else
                        dc.Show(ac);
                }
            }

        }

        public CheckedArrayControl<int> this[string name] => _detsCheckedArray._controls.OfType<CheckedArrayControl<int>>().Where(c => c.Text == name).FirstOrDefault();

        public Dictionary<string, int[]> DetCont
        {
            get
            {
                var d = new Dictionary<string, int[]>();
                foreach (var dc in _detsCheckedArray._controls.OfType<CheckedArrayControl<int>>())
                {
                    d.Add(dc.Text, dc.SelectedItems);
                }
                return d;
            }
        }
    
    } // public partial class SamplesToDetectorsForm : Form
}     // namespace Regata.Core.UI.WinForms.Forms
