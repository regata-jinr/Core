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
using Regata.Core.Settings;
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    enum Direction : short { Negative = -1, Positive = 1 }
    public partial class IrradiationRegister
    {
        public ControlsGroupBox controlsIrrParams;
        public ControlsGroupBox controlsTimeChanged;
        public CheckedArrayControl<short?> CheckedContainerArrayControl;
        public Button buttonRehandle;
        public Button buttonAssingNowDateTime;
        public DurationControl DurationControl;
        public DateTimePicker TimePicker;
        public ControlsGroupBox controlsMovingInContainer;
        public Button buttonMoveUpInContainer;
        public Button buttonMoveDownInContainer;

        private void InitializeIrradiationsParamsControls()
        {
            try
            {
                DurationControl = new DurationControl();
                TimePicker = new DateTimePicker();
                TimePicker.Format = DateTimePickerFormat.Time;
                TimePicker.ShowUpDown = true;
                TimePicker.Dock = DockStyle.Fill;
                TimePicker.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
                controlsTimeChanged = new ControlsGroupBox(new Control[] { TimePicker }) { Name = "controlsTimeChanged" };
                //TimePicker.MouseLeave += (s, e) => { mainForm.MainRDGV.Focus(); };
                CheckedContainerArrayControl = new CheckedArrayControl<short?>(new short?[] { 1, 2, 3, 4, 5, 6, 7, 8 }, multiSelection: false) { Name = "CheckedArrayControlContainers" };
                buttonAssingNowDateTime = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonAssingNowDateTime" };
                CheckedContainerArrayControl.SelectItem(1);
                CheckedContainerArrayControl.checkedListBox.ColumnWidth = 70;
                controlsIrrParams = new ControlsGroupBox(new Control[] { DurationControl, controlsTimeChanged, buttonAssingNowDateTime, CheckedContainerArrayControl }) { Name = "controlsIrrParams" };

                controlsIrrParams._tableLayoutPanel.RowStyles[0].Height = 30F;
                controlsIrrParams._tableLayoutPanel.RowStyles[1].Height = 25F;
                controlsIrrParams._tableLayoutPanel.RowStyles[2].Height = 30F;
                //controlsIrrParams._tableLayoutPanel.RowStyles[3].Height = 15F;

                buttonMoveUpInContainer = new Button()   { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonMoveUpInContainer"};
                buttonMoveDownInContainer = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonMoveDownInContainer"};

                buttonMoveUpInContainer.Click += (s, e) => { ChangeIrraditionPositionInContainer(mainForm.MainRDGV.SelectedCells, Direction.Negative); };
                buttonMoveDownInContainer.Click += (s, e) => { ChangeIrraditionPositionInContainer(mainForm.MainRDGV.SelectedCells, Direction.Positive); };

                 controlsMovingInContainer = new ControlsGroupBox(new Control[] { buttonMoveUpInContainer, buttonMoveDownInContainer }) { Name = "controlsMovingInContainer" };


                mainForm.FunctionalLayoutPanel.Controls.Add(controlsIrrParams, 1, 0);
                mainForm.FunctionalLayoutPanel.Controls.Add(controlsMovingInContainer, 2, 0);

                DurationControl.DurationChanged += (s, e) =>
                    {
                        mainForm.MainRDGV.FillDbSetValues("Duration", (int)DurationControl.Duration.TotalSeconds);
                        FillDateTimeFinish();
                    };


                CheckedContainerArrayControl.SelectionChanged += CheckedContainerArrayControl_SelectionChanged;
#if NETFRAMEWORK
                switch (_irrType)
                {
                    case IrradiationType.sli:
                        DurationControl.Duration = TimeSpan.FromSeconds(Settings<IrradiationSettings>.CurrentSettings.DefaultSLITime);
                        break;
                    case IrradiationType.lli:
                        DurationControl.Duration = TimeSpan.FromSeconds(Settings<IrradiationSettings>.CurrentSettings.DefaultLLITime);
                        break;
                    default:
                        DurationControl.Duration = TimeSpan.FromSeconds(0);
                        break;
                };
#else
                DurationControl.Duration = _irrType switch
                {
                    IrradiationType.sli => TimeSpan.FromSeconds(Settings<IrradiationSettings>.CurrentSettings.DefaultSLITime),
                    IrradiationType.lli => TimeSpan.FromSeconds(Settings<IrradiationSettings>.CurrentSettings.DefaultLLITime),
                    _ => TimeSpan.FromSeconds(0),
                };
#endif

                CheckedContainerArrayControl.SelectItem(1);

                // TODO: add rehandler column and mapping from name to int
                buttonAssingNowDateTime.Click += (s, e) =>
                {
                    mainForm.MainRDGV.FillDbSetValues("DateTimeStart", DateTime.Now);
                    FillDateTimes(null);
                    //FillDateTimeFinish();

                };
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MEAS_PARAMS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void CheckedContainerArrayControl_SelectionChanged(object sender, ItemCheckEventArgs e)
        {
            mainForm.MainRDGV.FillDbSetValues("Container", CheckedContainerArrayControl.SelectedItem);
            SetPositionInSelectedContainer();
        }

        private void SetPositionInSelectedContainer()
        {
            try
            {
                short? j = 1;
                foreach (var c in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().OrderBy(c => c))
                {
                    var m = mainForm.MainRDGV.CurrentDbSet.Where(mm => mm.Id == (int)mainForm.MainRDGV.Rows[c].Cells["Id"].Value).FirstOrDefault();
                    if (m == null) continue;
                    m.Position = j;
                    mainForm.MainRDGV.CurrentDbSet.Update(m);
                    j++;
                }
                mainForm.MainRDGV.SaveChanges();
                mainForm.MainRDGV.Refresh();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_DB_VAL) { DetailedText = string.Join(Environment.NewLine, "Positon", ex.Message) });
            }
        }


        private void ChangeIrraditionPositionInContainer(DataGridViewSelectedCellCollection selCells, Direction dir)
        {
            try
            {
                if (selCells.Count <= 0)
                {
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_SWAP_IRR_ROWS) { DetailedText = "You have to choose something in the table" });
                    return;
                }

                var setColumn = selCells[0].ColumnIndex;
                var currentRow    = selCells[0].OwningRow;
                var currentRowNum = selCells[0].RowIndex;


                mainForm.MainRDGV.Rows[currentRowNum].Cells["Position"].Selected = true;
                mainForm.MainRDGV.Rows[currentRowNum].Cells["SetNumber"].Selected = true;
                mainForm.MainRDGV.Rows[currentRowNum].Cells["SetIndex"].Selected = true;
                mainForm.MainRDGV.Rows[currentRowNum].Cells["SampleNumber"].Selected = true;

                var swapIndex = currentRowNum + (short)dir;
                var swapRow   = mainForm.MainRDGV.Rows[swapIndex];

                var currIrr    = mainForm.MainRDGV.CurrentDbSet.Local.Where(cir => cir.Id == (int)currentRow.Cells["Id"].Value).First();
                var swapIrr    = mainForm.MainRDGV.CurrentDbSet.Local.Where(cir => cir.Id == (int)swapRow.Cells["Id"].Value).First();


                if (currIrr.Position == 1 && dir == Direction.Negative)
                    return;

                if (currIrr.Container != swapIrr.Container)
                    return;

                currIrr.Swap(ref swapIrr);

                currIrr.Position = (short?)(currIrr.Position.Value - (short)dir);
                swapIrr.Position = (short?)(currIrr.Position.Value + (short)dir);

                //mainForm.MainRDGV.CurrentDbSet.Update(currIrr);
                //mainForm.MainRDGV.CurrentDbSet.Update(swapIrr);

                //mainForm.MainRDGV.SaveChanges();
                mainForm.MainRDGV.Refresh();
                mainForm.MainRDGV.ClearSelection();
                mainForm.MainRDGV.Rows[swapIndex].Cells["Position"].Selected = true;
                mainForm.MainRDGV.Rows[swapIndex].Cells["SetNumber"].Selected = true;
                mainForm.MainRDGV.Rows[swapIndex].Cells["SetIndex"].Selected = true;
                mainForm.MainRDGV.Rows[swapIndex].Cells["SampleNumber"].Selected = true;

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.WARN_UI_WF_SWAP_IRR_UNREG) { DetailedText = ex.Message });
            }
        }

        private void FillDateTimeFinish()
        {
            foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
            {
                var m = mainForm.MainRDGV.CurrentDbSet.Where(mm => mm.Id == (int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value).FirstOrDefault();
                if (m == null || !m.DateTimeStart.HasValue) continue;
                m.DateTimeFinish = m.DateTimeStart.Value.AddSeconds(DurationControl.Duration.TotalSeconds);
                mainForm.MainRDGV.CurrentDbSet.Update(m);

            }
            mainForm.MainRDGV.Refresh();
            mainForm.MainRDGV.SaveChanges();
        }

        private void FillDateTimes(DateTime? dt)
        {
            if (!dt.HasValue)
                dt = DateTime.Now;
            foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
            {
                var m = mainForm.MainRDGV.CurrentDbSet.Where(mm => mm.Id == (int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value).FirstOrDefault();
                if (m == null || !m.DateTimeStart.HasValue) continue;
                m.DateTimeStart = dt.Value.Date + TimePicker.Value.TimeOfDay;
                m.DateTimeFinish = m.DateTimeStart.Value.AddSeconds(DurationControl.Duration.TotalSeconds);
                mainForm.MainRDGV.CurrentDbSet.Update(m);

            }
            mainForm.MainRDGV.Refresh();
            mainForm.MainRDGV.SaveChanges();
        }

    } //public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
