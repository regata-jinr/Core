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
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {
        private void InitializeRegFormingControls()
        {
            mainForm.buttonAddSelectedSamplesToReg.Click += ButtonAddSelectedSamplesToReg_Click;
            mainForm.buttonRemoveSelectedSamples.Click += ButtonRemoveSelectedSamples_Click;
            //mainForm.buttonClearRegister.Click += ButtonClearRegister_Click;
            mainForm.buttonClearRegister.Visible = false;
            mainForm.buttonAddAllSamples.Click += ButtonAddAllSamples_Click;
        }

        private void AddRecord(Sample smp)
        {
            //mainForm.buttonClearRegister.Enabled = true;
            buttonAssingNowDateTime.Enabled = true;
            mainForm.MainRDGV.ClearSelection();

            try
            {
                Irradiation ir = new Irradiation();

                ir.CountryCode = smp.CountryCode;
                ir.ClientNumber = smp.ClientNumber;
                ir.Year = smp.Year;
                ir.SetNumber = smp.SetNumber;
                ir.SetIndex = smp.SetIndex;
                ir.SampleNumber = smp.SampleNumber;
                ir.Type = (int)_irrType;
                ir.LoadNumber = _loadNumber;


#if NETFRAMEWORK
   
                switch (_irrType)
                {
                    case IrradiationType.sli:
                        ir.Container = null;
                        ir.Channel = 2;
                        break;
                    default:
                        ir.Container = (short?)CheckedContainerArrayControl.SelectedItem;
                        ir.Channel = 1;
                        break;


                };
#else
                ir.Container = _irrType switch
                {
                    IrradiationType.sli => null,
                    _ => (short?)CheckedContainerArrayControl.SelectedItem
                };


                ir.Channel = _irrType switch
                {
                    IrradiationType.sli => 2,
                    _ => 1
                };
#endif


                ir.Duration = (int?)DurationControl.Duration.TotalSeconds;

                var rc = mainForm.MainRDGV.RowCount;

                short? pos = 1;
                if (rc != 0)
                {
                    var lastContainer = (short?)mainForm.MainRDGV.Rows[rc - 1].Cells["Container"].Value;
                    var lastPosition = (short?)mainForm.MainRDGV.Rows[rc - 1].Cells["Position"].Value;
                    pos = lastContainer != CheckedContainerArrayControl.SelectedItem ? 1 : (short?)(lastPosition + 1);
                }

#if NETFRAMEWORK
                switch (_irrType)
                {
                    case IrradiationType.sli:
                        ir.Position = null;
                        break;
                    default:
                        ir.Position = pos;
                        break;
                };
#else
                ir.Position = _irrType switch
                {
                    IrradiationType.sli => null,
                    _ => pos
                };
#endif

                mainForm.MainRDGV.Add(ir);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ADD_REC)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void ButtonAddSelectedSamplesToReg_Click(object sender, EventArgs e)
        {
            var cti = mainForm.TabsPane.SelectedTabIndex;

            foreach (var i in mainForm.TabsPane[cti, 1].SelectedCells.OfType<DataGridViewCell>()
                                                                      .Select(c => c.RowIndex)
                                                                      .Where(c => c >= 0)
                                                                      .Distinct()
                                                                      .OrderBy(c => c))
            {
                Sample smp = null;
#if NETFRAMEWORK
                switch (cti)
                {
                    case 1:
                        smp = Sample.CastSRM(_chosenStandards[i]);
                        break;
                    case 2:
                        smp = Sample.CastMonitor(_chosenMonitors[i]);
                        break;
                    default:
                        smp = _chosenSamples[i];
                        break;
                };
#else
                smp = cti switch
                {
                    1 => Sample.CastSRM(_chosenStandards[i]),
                    2 => Sample.CastMonitor(_chosenMonitors[i]),
                    _ => _chosenSamples[i]
                };

#endif
                AddRecord(smp);
            }
            mainForm.MainRDGV.SaveChanges();
            ColorizeRow(mainForm.MainRDGV.Rows[mainForm.MainRDGV.RowCount - 1]);

        }

        private void ButtonAddAllSamples_Click(object sender, EventArgs e)
        {
            var cti = mainForm.TabsPane.SelectedTabIndex;


            if (cti == 0) // samples tab
            {
                foreach (var smp in _chosenSamples)
                {
                    AddRecord(smp);
                }
            }
            if (cti == 1) // srms tab
            {
                foreach (var srm in _chosenStandards)
                {
                    AddRecord(Sample.CastSRM(srm));
                }

            }
            if (cti == 2) // monitors tab
            {
                foreach (var mon in _chosenMonitors)
                {
                    AddRecord(Sample.CastMonitor(mon));
                }
            }

            mainForm.MainRDGV.SaveChanges();

            ColorizeDGV(mainForm.MainRDGV);

        }


        private void ButtonClearRegister_Click(object sender, EventArgs e)
        {
            ClearCurrentRegister();
            mainForm.buttonClearRegister.Enabled = false;
            buttonAssingNowDateTime.Enabled = false;


        }

        private void ButtonRemoveSelectedSamples_Click(object sender, EventArgs e)
        {
            foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                RemoveRecord((int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value);

            mainForm.MainRDGV.SaveChanges();
        }

        private void RemoveRecord(int id)
        {
            try
            {
                var m = mainForm.MainRDGV.CurrentDbSet.Where(i => i.Id == id).FirstOrDefault();
                if (m == null) return;

                mainForm.MainRDGV.Remove(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_REM_REC)
                {
                    DetailedText = ex.ToString()
                });
            }
        }


        private void ClearCurrentRegister()
        {
            try
            {
                mainForm.MainRDGV.Clear();
                mainForm.MainRDGV.SaveChanges();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CLR_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private static readonly Color[] colors = new Color[] { Color.LightBlue, Color.LightSalmon, Color.LightGreen, Color.LightPink, Color.LightGray, Color.LightYellow, Color.LightCyan, Color.Thistle };

        private void ColorizeRow(DataGridViewRow row)
        {
            if (row == null || row.DataGridView == null || !row.DataGridView.Columns.Contains("Container")) return;

            short? container = (short?)row.Cells["Container"].Value;

            if (!container.HasValue) return;

            row.DefaultCellStyle.BackColor = colors[container.Value];
        }

        private void ColorizeDGV(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
                ColorizeRow(row);
        }

    } //public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
