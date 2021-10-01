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

using Microsoft.EntityFrameworkCore;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using RCM=Regata.Core.Messages;
using Regata.Core.UI.WinForms.Controls.Settings;
using System;
using System.ComponentModel;
using System.Windows.Forms;

// TODO: add column filling by value
// TODO: add data validation
// TODO: add changing events (insert, delete, update)
// TODO: add export to excel (via csv)
// TODO: add filtering
// TODO: add colorizing
// TODO: add sorting
// TODO: add tests
// TODO: add incapsulation for bindinglist
// TODO: add autoupdate based on github releases

namespace Regata.Core.UI.WinForms.Controls
{
    /// <summary>
    /// RDataGridView is the class that represents widely used DataGridView functions.
    /// Based on data model it independent from data source (db, files, whatever) and allows user to bind table with data via 
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public partial class RDataGridView<Model> : DataGridView , IDisposable
        where Model : class, IId
    {
        public DbSet<Model> CurrentDbSet;

        private DbContext _rdbc;

        //private bool ModelInRegataModels() => Assembly.GetExecutingAssembly().GetTypes()
        //              .Where(t => t.Namespace == "Regata.Core.DataBase.Models")
        //              .ToList().Contains(typeof(Model));

        // FIXME: not mapped fields from model still visible in binding context.

        public RDataGridViewSettings RDGV_Set;

        // TODO:  exception or notification here?

        public RDataGridView() : base()
        {
            //if (!ModelInRegataModels()) throw new TypeAccessException($"This type {typeof(Model).Name} doesn't contains in Regata.Core.DataBase.Models");

            _rdbc = new RegataContext();

            CurrentDbSet = _rdbc.Set<Model>();

            DataSource = CurrentDbSet.Local.ToBindingList();

            RowHeadersVisible = false;
            AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill;

            DataBindingComplete += RDataGridView_DataBindingComplete;

            ColumnHeaderMouseClick += RDataGridView_ColumnHeaderMouseClick;

            CellValueChanged += RDataGridView_CellValueChanged;

            //HideColumns();
            //SetUpReadOnlyColumns();

        }

        private void RDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SaveChanges();
        }

        private void RDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void RDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }

        public void HideColumns()
        {
            foreach (DataGridViewColumn cl in Columns)
            {
                if (RDGV_Set.HidedColumns.Contains(cl.Name))
                    Columns[cl.Name].Visible = false;
                else
                    Columns[cl.Name].Visible = true;
            }
        }

        public void SetUpReadOnlyColumns()
        {
            foreach (DataGridViewColumn cl in Columns)
            {
                if (RDGV_Set.ReadOnlyColumns.Contains(cl.Name))
                    Columns[cl.Name].ReadOnly = true;
                else
                    Columns[cl.Name].ReadOnly = false;
            }
        }

        public void SetUpWritableColumns()
        {
            foreach (DataGridViewColumn cl in Columns)
            {
                if (RDGV_Set.WritableColumns.Contains(cl.Name))
                    Columns[cl.Name].ReadOnly = false;
                else
                    Columns[cl.Name].ReadOnly = true;
            }
        }

        private bool _isDisposed = false;

        private new void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (!_isDisposed &&  isDisposing)
            {
                _rdbc.Dispose();
            }
            _isDisposed = true;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
