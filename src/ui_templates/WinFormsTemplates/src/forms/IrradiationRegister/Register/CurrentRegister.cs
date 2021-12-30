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

using Microsoft.EntityFrameworkCore;
using Regata.Core.DataBase;
using RCM = Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {

        private void InitCurrentRegister()
        {
            try
            {
                switch (_irrType)
                {
                    case DataBase.Models.IrradiationType.lli:
                mainForm.MainRDGV.CurrentDbSet.Where(ir => ir.DateTimeStart.Value.Date == _irrDateTime.Date && ir.LoadNumber == _loadNumber && ir.Type == (short)_irrType).OrderBy(ir => ir.Container).ThenBy(ir => ir.Position).Load();
                        break;
                    case DataBase.Models.IrradiationType.sli:
                        mainForm.MainRDGV.CurrentDbSet.Where(ir => ir.DateTimeStart.Value.Date == _irrDateTime.Date && ir.Type == (short)_irrType).OrderBy(ir => ir.DateTimeStart).Load();
                        break;

                };
                mainForm.MainRDGV.DataSource = mainForm.MainRDGV.CurrentDbSet.Local.ToBindingList();

                mainForm.MainRDGV.SelectionChanged += (s, e) => 
                {
                    try
                    {
                        var cnt = 0;
                        var rowIndexes = mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().ToArray();
                        if (mainForm.MainRDGV.SelectedCells.Count >= 0)
                            cnt = rowIndexes.Count();
                        _selRowCnt.Text = $"{_selRowCnt.Text.Split(':')[0]}:{cnt}";

                        // NOTE: smell, sorry

                        var operIds = new List<int>(100);
                        var repIds = new List<int>(100);

                        foreach (var i in rowIndexes)
                        {
                            var curOpId = mainForm.MainRDGV.Rows[i].Cells["Assistant"].Value;
                            var curRepId = mainForm.MainRDGV.Rows[i].Cells["Rehandler"].Value;
                            if (curOpId != null) operIds.Add((int)curOpId);
                            if (curRepId != null) repIds.Add((int)curRepId);
                        }
                        operIds.TrimExcess();
                        operIds = operIds.Distinct().ToList();
                        repIds = repIds.Distinct().ToList();

                        var operators = new List<string>();

                        foreach (var oid in operIds)
                        {
                            using (var rc = new RegataContext())
                            {
                                operators.Add(rc.Users.Where(u => u.Id == oid).FirstOrDefault().LastName);
                            }
                        }

                        var repackers = new List<string>(100);

                        foreach (var rid in repIds)
                        {
                            using (var rc = new RegataContext())
                            {
                                repackers.Add(rc.Users.Where(u => u.Id == rid).FirstOrDefault().LastName);
                            }
                        }

                        _selRowOperators.Text = $"{_selRowOperators.Text.Split(':')[0]}:{string.Join(",", operators.Distinct().ToArray())}";
                        _selRowRepackers.Text = $"{_selRowRepackers.Text.Split(':')[0]}:{string.Join(",", repackers.Distinct().ToArray())}";
                    }
                    catch (Exception ex)
                    {
                    }
                };

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INIT_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
