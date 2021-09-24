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

using Regata.Core;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regata.Core.UI.WinForms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {
        private List<Monitor> _chosenMonitors;

        private void InitMonitorsRegisters()
        {
            try
            {
                mainForm.TabsPane[2, 0].SuspendLayout();

                mainForm.TabsPane[2, 0].MultiSelect = false;

                mainForm.TabsPane[2, 0].SelectionChanged += async (e, s) =>
                {
                    _chosenMonitors.Clear();
                    _chosenMonitors.Capacity = 499;

                    mainForm.TabsPane[2, 1].DataSource = null;
                    await FillSelectedMonitors();
                };

                mainForm.TabsPane[2, 0].Scroll += async (s, e) =>
                {
                    using (var r = new RegataContext())
                    {
                        if (mainForm.TabsPane[2, 0].RowCount == await r.MonitorSets.AsNoTracking().CountAsync()) return;
                    }

                    if (RowIsVisible(mainForm.TabsPane[2, 0].Rows[mainForm.TabsPane[2, 0].RowCount - 1]))
                        await FillMonitorsRegisters();
                };

                mainForm.TabsPane[2, 0].ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MON_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillMonitorsRegisters()
        {
            try
            {
                using (var r = new RegataContext())
                {
                    if (!_displaySetsParam.Checked || !mainForm.MainRDGV.CurrentDbSet.Local.Any())
                    {
                        mainForm.TabsPane[2, 0].DataSource = await r.MonitorSets
                                                    .AsNoTracking()
                                                    .Select(m => new { m.SetName, m.SetNumber, m.SetWeight })
                                                    .Distinct()
                                                    .OrderBy(m => m.SetName)
                                                    .ThenBy(m => m.SetNumber)
                                                    .Take(mainForm.TabsPane[2, 0].RowCount + 20)
                                                    .ToArrayAsync();
                    }
                    else
                    {
                        mainForm.TabsPane[2, 0].DataSource = mainForm.MainRDGV.CurrentDbSet.Local
                                                        .Where(ss => ss.Year == "m")
                                                        .Select(ss => new { SetName = ss.SetNumber, SetNumber = ss.SetIndex })
                                                        .Distinct()
                                                        .OrderByDescending(ss => ss.SetName)
                                                        .ThenByDescending(ss => ss.SetNumber)
                                                        .ToArray();
                    }
                }

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_MON_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillSelectedMonitors()
        {
            try
            {
                if (mainForm.TabsPane[2, 0].SelectedCells.Count <= 0)
                {
                    mainForm.TabsPane[2, 1].DataSource = null;
                    return;
                }

                var set = mainForm.TabsPane[2, 0].SelectedCells[0].Value as string;
                var set_num = mainForm.TabsPane[2, 0].SelectedCells[1].Value as string;

                using (var r = new RegataContext())
                {
                    var tmp = await r.Monitors.AsNoTracking()
                                                    .Where(m => m.SetName == set &&
                                                           m.SetNumber == set_num)
                                                    .OrderBy(m => m.Number)
                                                    .ToArrayAsync();
                    _chosenMonitors.AddRange(tmp);
                };

                _chosenMonitors.TrimExcess();

                if (_displaySetsParam.Checked)
                {
                    foreach (var ir in mainForm.MainRDGV.CurrentDbSet.Local)
                    {
                        var c = _chosenMonitors.Where(cs => ir.Year == "m" &&
                                                           ir.SetNumber == cs.SetName &&
                                                           ir.SetIndex == cs.SetNumber &&
                                                           ir.SampleNumber == cs.Number).FirstOrDefault();
                        if (c != null)
                            _chosenMonitors.Remove(c);
                    }
                }


                mainForm.TabsPane[2, 1].DataSource = _chosenMonitors;
                HideColumns(mainForm.TabsPane[2, 1], new string[] { "SetName", "SetNumber" });
                Labels.SetControlsLabels(mainForm);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_MON_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
