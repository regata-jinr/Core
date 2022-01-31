/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using RCM = Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{

    public partial class RDataGridView<Model> : DataGridView
    {
        public void FillCellsByValue<T>(DataGridViewSelectedCellCollection cells, T value)
        {
            foreach (DataGridViewCell c in cells)
            {
                if (c.ValueType != typeof(T))
                {
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_RDGV_Wrong_Value_Type));
                    continue;
                }
                c.Value = value;
            }
        }

        /// <summary>
        /// Assign values to specified property of Model from local DbSet and propagate changes to DGV.
        /// </summary>
        /// <typeparam name="T">Type of Model property</typeparam>
        /// <param name="prop">Name of properrty of Model or Column from dgv</param>
        /// <param name="val">Value</param>
        public void FillDbSetValues<T>(string prop, T val)
        {
            try
            {
                CellValueChanged -= RDataGridView_CellValueChanged;
                foreach (var i in SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                {
                    var m = CurrentDbSet.Where(mm => mm.Id == (int)Rows[i].Cells["Id"].Value).FirstOrDefault();
                    if (m == null) continue;
                    var setPropValue = m.GetType().GetProperty(prop).GetSetMethod();
                    setPropValue.Invoke(m, new object[] { val });
                    CurrentDbSet.Update(m);
                }
                SaveChanges();
                Refresh();
                CellValueChanged += RDataGridView_CellValueChanged;

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_DB_VAL) { DetailedText = string.Join(Environment.NewLine, prop, ex.Message) });
            }
        }


        public void Add(Model m)
        {
            try
            {
                CurrentDbSet.Add(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_INS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void AddRange(IEnumerable<Model> m)
        {
            try
            {
                CurrentDbSet.AddRange(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_INS_RNG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void Remove(Model m)
        {
            try
            {
                CurrentDbSet.Remove(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_REMOVE)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void RemoveRange(IEnumerable<Model> ms)
        {
            try
            {
                CurrentDbSet.RemoveRange(ms);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_REMOVE_RNG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void Clear()
        {
            try
            {
                CurrentDbSet.Local.Clear();
                SaveChanges();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_CLEAR)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void Update(Model m)
        {
            try
            {
                CurrentDbSet.Update(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_UPD)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void UpdateRange(IEnumerable<Model> ms)
        {
            try
            {
                CurrentDbSet.UpdateRange(ms);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_UPD_RNG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        public void SaveChanges()
        {
            try
            {
                _rdbc.SaveChanges();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_DB_SAVE)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
