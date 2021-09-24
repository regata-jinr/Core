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
using RCM = Regata.Core.Messages;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {
        /// <summary>
        /// List of samples from selected sample set in first page of the TabsPane
        /// </summary>
        private List<Sample> _chosenSamples;

        private void InitSamplesRegisters()
        {
            try
            {
                mainForm.TabsPane[0, 0].SuspendLayout();
                mainForm.TabsPane[0, 0].MultiSelect = false;

                mainForm.TabsPane[0, 0].SelectionChanged += async (e, s) =>
                {
                    await FillSelectedSamples();
                };


                mainForm.TabsPane[0, 0].Scroll += async (s, e) =>
                {
                    using (var r = new RegataContext())
                    {
                        if (mainForm.TabsPane[0, 0].RowCount == await r.SamplesSets.AsNoTracking().CountAsync()) return;
                    }
                    if (RowIsVisible(mainForm.TabsPane[0, 0].Rows[mainForm.TabsPane[0, 0].RowCount - 1]))
                        await FillSamplesRegisters();
                };

                mainForm.TabsPane[0, 0].ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_SMP_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private bool RowIsVisible(DataGridViewRow row)
        {
            if (row.DataGridView == null) return false;
            DataGridView dgv = row.DataGridView;
            int firstVisibleRowIndex = dgv.FirstDisplayedCell.RowIndex;
            int lastVisibleRowIndex = firstVisibleRowIndex + dgv.DisplayedRowCount(false) - 1;
            return row.Index >= firstVisibleRowIndex && row.Index <= lastVisibleRowIndex;
        }

        private async Task FillSamplesRegisters()
        {
            try
            {
                using (var r = new RegataContext())
                {
                    if (!_displaySetsParam.Checked || !mainForm.MainRDGV.CurrentDbSet.Local.Any())
                    {
                        mainForm.TabsPane[0, 0].DataSource = await r.SamplesSets
                                                        .AsNoTracking()
                                                        .Select(ss => new { ss.CountryCode, ss.ClientNumber, ss.Year, ss.SetNumber, ss.SetIndex })
                                                        .Distinct()
                                                        .OrderByDescending(ss => ss.Year)
                                                        .ThenByDescending(ss => ss.SetNumber)
                                                        .ThenByDescending(ss => ss.SetIndex)
                                                        .Take(mainForm.TabsPane[0, 0].RowCount + 20)
                                                        .ToArrayAsync();
                        mainForm.TabsPane[0, 0].FirstDisplayedScrollingRowIndex = mainForm.TabsPane[0, 0].RowCount - 20;

                    }
                    else
                    {
                        mainForm.TabsPane[0, 0].DataSource = mainForm.MainRDGV.CurrentDbSet.Local
                                                        .Where(ss => ss.Year != "s" && ss.Year != "m")
                                                        .Select(ss => new { ss.CountryCode, ss.ClientNumber, ss.Year, ss.SetNumber, ss.SetIndex })
                                                        .Distinct()
                                                        .OrderByDescending(ss => ss.Year)
                                                        .ThenByDescending(ss => ss.SetNumber)
                                                        .ThenByDescending(ss => ss.SetIndex)
                                                        .ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SMP_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillSelectedSamples()
        {
            try
            {
                if (mainForm.TabsPane[0, 0].SelectedCells.Count <= 0)
                {
                    mainForm.TabsPane[0, 1].DataSource = null;
                    return;
                }

                mainForm.TabsPane[0, 1].DataSource = null;
                _chosenSamples.Clear();
                _chosenSamples.Capacity = 199;

                var country = mainForm.TabsPane[0, 0].SelectedCells[0].Value as string;
                var client = mainForm.TabsPane[0, 0].SelectedCells[1].Value as string;
                var year = mainForm.TabsPane[0, 0].SelectedCells[2].Value as string;
                var set = mainForm.TabsPane[0, 0].SelectedCells[3].Value as string;
                var set_index = mainForm.TabsPane[0, 0].SelectedCells[4].Value as string;

                using (var r = new RegataContext())
                {
                    var query = r.Samples.AsNoTracking()
                                         .Where(s => s.CountryCode == country &&
                                                s.ClientNumber == client &&
                                                s.Year == year &&
                                                s.SetNumber == set &&
                                                s.SetIndex == set_index
                                               );
                    Sample[] _tmpList = null;
#if NETFRAMEWORK
                    switch (_irrType)
                    {
                        case IrradiationType.sli:
                            _tmpList = await query.OrderBy(s => s.SampleNumber).ToArrayAsync();
                            break;
                        default:
                            _tmpList = await query.OrderBy(s => s.SampleNumber).ToArrayAsync();
                            break;
                    };
#else
                    _tmpList = _irrType switch
                    {
                        IrradiationType.sli => await query.OrderBy(s => s.SampleNumber).ToArrayAsync(),
                        _ => await query.OrderBy(s => s.SampleNumber).ToArrayAsync()
                    };
#endif

                    _chosenSamples.AddRange(_tmpList);
                }
                _chosenSamples.TrimExcess();

                if (_displaySetsParam.Checked)
                {
                    foreach (var ir in mainForm.MainRDGV.CurrentDbSet.Local)
                    {
                        var c = _chosenSamples.Where(cs => ir.CountryCode == cs.CountryCode &&
                                                           ir.ClientNumber == cs.ClientNumber &&
                                                           ir.Year == cs.Year &&
                                                           ir.SetNumber == cs.SetNumber &&
                                                           ir.SetIndex == cs.SetIndex &&
                                                           ir.SampleNumber == cs.SampleNumber).FirstOrDefault();
                        if (c != null)
                            _chosenSamples.Remove(c);
                    }
                }

                mainForm.TabsPane[0, 1].DataSource = _chosenSamples;
                HideAllColumns(mainForm.TabsPane[0, 1]);
                ShowColumns(mainForm.TabsPane[0, 1], new string[] { "CountryCode", "ClientNumber", "Year", "SetNumber", "SetIndex", "SampleNumber", "LLIWeight", "SLIWeight" });
                Labels.SetControlsLabels(mainForm);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_SMP_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void HideColumns(DataGridView dgv, IEnumerable<string> columnNames)
        {
            try
            {
                if (dgv.Columns.Count <= 0) return;
                foreach (var col in columnNames)
                    dgv.Columns[col].Visible = false;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_HIDE_IRR_COLS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void HideAllColumns(DataGridView dgv)
        {
            if (dgv == null) return;
                if (mainForm.TabsPane[0, 1].Columns.Count <= 0) return;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.Visible = false;
            }

        private void ShowColumns(DataGridView dgv, IEnumerable<string> columnNames)
        {
            try
            {
                if (dgv == null) return;
                if (dgv.Columns.Count <= 0) return;
                foreach (var col in columnNames)
                    dgv.Columns[col].Visible = true;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_HIDE_IRR_COLS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class IrradiationRegister
}     // namespace Regata.Core.UI.WinForms.Forms.Irradiations
