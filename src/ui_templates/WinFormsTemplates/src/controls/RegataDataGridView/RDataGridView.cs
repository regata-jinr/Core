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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Regata.Core.UI.WinForms.Controls.Settings;
using Regata.Core.DB.MSSQL.Context;
using RCM=Regata.Core.Messages;

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
        where Model : class
    {
        private BindingList<Model> _data = new BindingList<Model>();

        public BindingList<Model> Data
        {
            get { return _data; }

            set 
            {
                if (value == null)
                {
                    _data = new BindingList<Model>();
                    Report.Notify(new RCM.Message(Codes.ERR_UI_WF_RDGV_Null_Data));
                    return;
                }

                if (value.Count == 0)
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_RDGV_Empty_Data));
                
                _data = value;

                DataSource = _data;
            }
        }

        public DbSet<Model> CurrentDbSet;

        private DbContext _rdbc;

        //private bool ModelInRegataModels() => Assembly.GetExecutingAssembly().GetTypes()
        //              .Where(t => t.Namespace == "Regata.Core.DB.MSSQL.Models")
        //              .ToList().Contains(typeof(Model));
        
        // FIXME: not mapped fields from model still visible in binding context.

        public static RDataGridViewSettings RDGV_Set = new RDataGridViewSettings();

        // TODO:  exception or notification here?

        public RDataGridView(RDataGridViewSettings rdgv_set = null) : base()
        {
            //if (!ModelInRegataModels()) throw new TypeAccessException($"This type {typeof(Model).Name} doesn't contains in Regata.Core.DB.MSSQL.Models");

            if (rdgv_set != null)
                RDGV_Set = rdgv_set;
            else
                RDGV_Set = new RDataGridViewSettings();

            _rdbc = new RegataContext();

            CurrentDbSet = _rdbc.Set<Model>();

            RowHeadersVisible = false;
            AutoSizeColumnsMode = RDGV_Set.ColumnSize;


            DataBindingComplete += RDataGridView_DataBindingComplete;

            ColumnHeaderMouseClick += RDataGridView_ColumnHeaderMouseClick;

            HideColumns();
            SetUpReadOnlyColumns();

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

        public void SaveChanges()
        {
            _rdbc.SaveChanges();
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

        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {

           
            
        }


    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
