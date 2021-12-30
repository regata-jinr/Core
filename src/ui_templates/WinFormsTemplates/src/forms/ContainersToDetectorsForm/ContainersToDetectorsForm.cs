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
using Regata.Core.DataBase.Models;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RCM = Regata.Core.Messages;

#if NETFRAMEWORK
using Excel = Microsoft.Office.Interop.Excel;
#endif

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class ContainersToDetectorsForm : Form
    {
        private readonly int _loadNumber;

        public readonly int? MaxContNumber;

        private ControlsGroupBox _detsCheckedArray;

        private readonly Dictionary<int, bool> _assignedContainers;

        public ContainersToDetectorsForm(string[] dets, int loadNumber)
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
                {  Name = dets[i], Text = dets[i]};
                chacs[i].SelectionChanged += SamplesToDetectorsForm_SelectionChanged;
                chacs[i].checkedListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            _detsCheckedArray = new ControlsGroupBox(chacs, vertical: false) { Dock = DockStyle.Fill, Name = "ContToDetsGroupBox" };

            tableLayoutPanelMain.Controls.Add(_detsCheckedArray, 0, 1); 
        }

        private void FillButtonsRow()
        {
            tableLayoutPanelMain.Controls.Add(new ControlsGroupBox(new Control[] { buttonExportToCSV, buttonExportToExcel, buttonFillMeasurementRegister }, vertical: false) { Name = "ContToDetsButtonsGroupBox" }, 0, 2);
        }

        private CheckedArrayControl<int> CreateArrayCheckBox(string dName)
        {
            return new CheckedArrayControl<int>(Enumerable.Range(1, MaxContNumber.Value).ToArray(), multiSelection: true);
        }

        private void SamplesToDetectorsForm_SelectionChanged(object sender, ItemCheckEventArgs e)
        {
            var currentListBox = sender as CheckedListBox;

            if (currentListBox == null) return; // never?

            var currentItem = currentListBox.Items[e.Index];
            foreach (var cac in _detsCheckedArray._controls.OfType<CheckedArrayControl<int>>())
            {
                var dlb = cac.checkedListBox;
                if (dlb == currentListBox)
                    continue;
                if (e.NewValue == CheckState.Checked)
                {
                    dlb.Items.Remove(currentItem);
                }
                else
                {
                    dlb.Items.Add(currentItem);
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

#if NETFRAMEWORK
        public void ExportToExcel(IEnumerable<Irradiation> irrs)
        {
            try
            {
                if (DetCont.Count == 0 || !irrs.Any())
                    return;

                if (irrs.Where(ir => !ir.Container.HasValue).Any() || irrs.Where(ir => !ir.Position.HasValue).Any())
                    return;

                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = true;

                // Create a new, empty workbook and add it to the collection returned
                // by property Workbooks. The new workbook becomes the active workbook.
                // Add has an optional parameter for specifying a praticular template.
                // Because no argument is sent in this example, Add creates a new workbook.
                excelApp.Workbooks.Add();

                // This example uses a single workSheet. The explicit type casting is
                // removed in a later procedure.
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                workSheet.Cells.NumberFormat = "@";
                float width = 2;
                Action<string> set_width = (string col) =>
                {
                    Excel.Range exCol = workSheet.get_Range($"{col}:{col}", Type.Missing);
                    exCol.EntireColumn.ColumnWidth = width;
                };

                (int col1, int col2) cols = (3, 47);
                Action<string> set_borders = (string col) =>
                {
                    Excel.Range cell = workSheet.get_Range($"{col}{cols.col1}:{col}{cols.col2}", Type.Missing);
                    Excel.Borders border = cell.Borders;
                    border.LineStyle = Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;
                    border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                };

                Action<string> set_bottom_borders = (string col) =>
                {
                    Excel.Range cell = workSheet.get_Range($"{col}{cols.col1}:{col}{cols.col2}", Type.Missing);
                    Excel.Borders border = cell.Borders;
                    border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    border[Excel.XlBordersIndex.xlEdgeBottom].Weight = 4d;
                };

                new List<string> { "A", "D", "G", "K", "J" }.ForEach(set_width);

                var detColsExcel = new Dictionary<string, (string Column1, string Column2)>
                {
                    { "D1",  (Column1:"B", Column2: "C")},
                    { "D2",  (Column1:"E", Column2: "F")},
                    { "D3",  (Column1:"H", Column2: "I")},
                    { "D4",  (Column1:"K", Column2: "L")}
                };

                Func<short, short> max_cont_position = (short cont) =>
                {
                    var m = irrs.Where(irr => irr.Container.Value == cont).Select(ir => ir.Position).Max();
                    return m.HasValue ? m.Value : (short)0;
                };

                width = 5;
                detColsExcel.Values.Select(d => d.Column1).ToList().ForEach(set_width);
                width = 14;
                detColsExcel.Values.Select(d => d.Column2).ToList().ForEach(set_width);

                detColsExcel.Values.Select(d => d.Column1).ToList().ForEach(set_borders);
                detColsExcel.Values.Select(d => d.Column2).ToList().ForEach(set_borders);

                foreach (var det in DetCont.Keys)
                {
                    workSheet.Cells[2, detColsExcel[det].Column1] = string.Join("-", DetCont[det]);
                    var oRng = workSheet.get_Range($"{detColsExcel[det].Column1}2", $"{detColsExcel[det].Column2}2");
                    oRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oRng.Merge(null);
                    var bold_pos = 2;
                    foreach (var cont in DetCont[det])
                    {
                        bold_pos += max_cont_position((short)cont);
                        cols = (bold_pos, bold_pos);
                        set_bottom_borders(detColsExcel[det].Column1);
                        set_bottom_borders(detColsExcel[det].Column2);
                    }

                    var irForDet = irrs.Where(irr => DetCont[det].Contains((int)irr.Container)).ToArray();
                    for (var i = 0; i < 45; ++i)
                    {
                        workSheet.Cells[i + 3, detColsExcel[det].Column1] = i + 1;
                        if (irForDet.Length <= i)
                        {
                            workSheet.Cells[i + 3, detColsExcel[det].Column2] = string.Empty;
                        }
                        else
                        {
                            if (irForDet[i].Year == "s" || irForDet[i].Year == "m")
                                workSheet.Cells[i + 3, detColsExcel[det].Column2] = irForDet[i].SetKey;
                            else
                                workSheet.Cells[i + 3, detColsExcel[det].Column2] = irForDet[i].SampleKey;
                        }
                    }

                }

                var info_col = "N";
                var info_num = 2;
                workSheet.Cells[info_num, info_col] = _loadNumber;
                width = 15;
                set_width(info_col);
                foreach (var set_key in irrs.Where(ir => ir.Year != "s").Select(ir => ir.SetKey).Distinct())
                {
                    info_num++;
                    workSheet.Cells[info_num, info_col] = set_key;
                }
                workSheet.Cells[47, info_col] = irrs.Count();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_EXPORT_TO_EXCEL_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }
#endif

    } // public partial class ContainersToDetectorsForm : Form
}     // namespace Regata.Core.UI.WinForms.Forms
