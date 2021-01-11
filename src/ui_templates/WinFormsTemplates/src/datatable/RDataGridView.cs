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
using Regata.Core.UI.WinForms.Settings;
using Regata.Core.DB.MSSQL.Context;
using Regata.Core.DB.MSSQL.Models;

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

namespace Regata.Core.UI.WinForms
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

        public DbSet<Model> CurrentDbSet;

        private DbContext _rdbc;

        //private bool ModelInRegataModels() => Assembly.GetExecutingAssembly().GetTypes()
        //              .Where(t => t.Namespace == "Regata.Core.DB.MSSQL.Models")
        //              .ToList().Contains(typeof(Model));
        
        // FIXME: not mapped fields from model still visible in binding context.

        public static RDataGridViewSettings RDGV_Set = new RDataGridViewSettings();

        // TODO:  exception or notification here?

        public RDataGridView(string csTarget, RDataGridViewSettings rdgv_set = null) : base()
        {
            if (string.IsNullOrEmpty(csTarget))
                throw new ArgumentNullException("RDataGridView requires connections string for correct initialization.");

            //if (!ModelInRegataModels()) throw new TypeAccessException($"This type {typeof(Model).Name} doesn't contains in Regata.Core.DB.MSSQL.Models");

            if (rdgv_set != null)
                RDGV_Set = rdgv_set;
            else
                RDGV_Set = new RDataGridViewSettings();

            _rdbc = new RegataContext(csTarget);

            CurrentDbSet = _rdbc.Set<Model>();

            RowHeadersVisible = false;
            AutoSizeColumnsMode = RDGV_Set.ColumnSize;


            HideColumns();
            SetUpReadOnlyColumns();
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



    } // public abstract partial class RDataGridView<Model> : DataGridView
}     // namespace Regata.Core.UI.WinForms
