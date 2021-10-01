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
        /// <summary>
        /// List of Irradiations from selected irradiation register in TabPane
        /// </summary>
        private List<Standard> _chosenStandards;

        private void InitStandardsRegisters()
        {
            try
            {
                mainForm.TabsPane[1, 0].SuspendLayout();
                mainForm.TabsPane[1, 0].MultiSelect = false;

                mainForm.TabsPane[1, 0].SelectionChanged += async (e, s) =>
                {
                    await FillSelectedStandards();
                };


                mainForm.TabsPane[1, 0].Scroll += async (s, e) =>
                {
                    using (var r = new RegataContext())
                    {
                        if (mainForm.TabsPane[1, 0].RowCount == await r.StandardSets.AsNoTracking().CountAsync()) return;
                    }
                    
                    if (RowIsVisible(mainForm.TabsPane[1, 0].Rows[mainForm.TabsPane[1, 0].RowCount - 1]))
                        await FillStandardsRegisters();
                };

                mainForm.TabsPane[1, 0].ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_STD_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillStandardsRegisters()
        {
            try
            {
                using (var r = new RegataContext())
                {
                    if (!_displaySetsParam.Checked || !mainForm.MainRDGV.CurrentDbSet.Local.Any())
                    {
                        mainForm.TabsPane[1, 0].DataSource = await r.StandardSets
                                                    .AsNoTracking()
                                                    .Select(ss => new { ss.SetName, ss.SetNumber })
                                                    .Distinct()
                                                    .OrderBy(ss => ss.SetName)
                                                    .ThenBy(ss => ss.SetNumber)
                                                    .Take(mainForm.TabsPane[1, 0].RowCount + 20)
                                                    .ToArrayAsync();

                    mainForm.TabsPane[1, 0].FirstDisplayedScrollingRowIndex = mainForm.TabsPane[1, 0].RowCount - 20;
                    }
                    else
                    {
                        mainForm.TabsPane[1, 0].DataSource = mainForm.MainRDGV.CurrentDbSet.Local
                                                        .Where(ss => ss.Year == "s")
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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_STD_REGS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillSelectedStandards()
        {
            try
            {
                if (mainForm.TabsPane[1, 0].SelectedCells.Count <= 0)
                {
                    mainForm.TabsPane[1, 1].DataSource = null;
                    return;
                }

                mainForm.TabsPane[1, 1].DataSource = null;
                _chosenStandards.Clear();
                _chosenStandards.Capacity = 199;

                var set = mainForm.TabsPane[1, 0].SelectedCells[0].Value as string;
                var set_num = mainForm.TabsPane[1, 0].SelectedCells[1].Value as string;

                using (var r = new RegataContext())
                {
                    _chosenStandards.AddRange(await r.Standards.AsNoTracking()
                                         .Where(s => s.SetName == set &&
                                                s.SetNumber == set_num)
                                         .OrderBy(s => s.Number)
                                         .ToArrayAsync());

                }
                _chosenStandards.TrimExcess();

                if (_displaySetsParam.Checked)
                {
                    foreach (var ir in mainForm.MainRDGV.CurrentDbSet.Local)
                    {
                        var c = _chosenStandards.Where(cs => ir.Year == "s" &&
                                                           ir.SetNumber == cs.SetName &&
                                                           ir.SetIndex == cs.SetNumber &&
                                                           ir.SampleNumber == cs.Number).FirstOrDefault();
                        if (c != null)
                            _chosenStandards.Remove(c);
                    }
                }


                mainForm.TabsPane[1, 1].DataSource = _chosenStandards;
                HideColumns(mainForm.TabsPane[1, 1], new string[] { "SetName", "SetNumber" });
                Labels.SetControlsLabels(mainForm);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_STD_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
