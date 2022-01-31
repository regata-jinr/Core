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
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.TabControl;

namespace Regata.Core.UI.WinForms.Controls
{
    public partial class DGVTabPaneControl : UserControl
    {
        public TabPage ActiveTabPage => tabControl.SelectedTab;

        public TabPageCollection Pages => tabControl.TabPages;

        public event Action DataSourceChanged;

        private float _bigDgvSizeCoeff;
        private uint _dgvsNumberOnPage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabsNumber">Number of tab pages</param>
        /// <param name="dgvsNumberOnPage">Number of DGVs on each tab page</param>
        /// <param name="BigDgvSizeCoeff">Specified what part from the tab page size should take biggest dgv. In case of 1 the equal parts for each dgv.</param>
        public DGVTabPaneControl(uint tabsNumber = 1, uint dgvsNumberOnPage = 1, float BigDgvSizeCoeff = 1.0f)
        {
            _bigDgvSizeCoeff = BigDgvSizeCoeff;
            _dgvsNumberOnPage = dgvsNumberOnPage;

            InitializeComponent();

            for (int i = 0; i < tabsNumber; ++i)
            {
                var pg = CreateTabPage(i);
                pg.Controls.Add(CreateTableLayoutPanel(i));
                Pages.Add(pg);
            }

            this.ResumeLayout(false);
        }

        private TabPage CreateTabPage(int page_ind)
        {
            var name = $"tabPage{page_ind + 1}";
            var pg = new TabPage(name) { Name = name, AutoScroll = true };
            return pg;
        }

        private TableLayoutPanel CreateTableLayoutPanel(int page_ind)
        {
            var tlp = new TableLayoutPanel();
            tlp.ColumnCount = (int)_dgvsNumberOnPage;
            tlp.AutoSize = true;
            tlp.Dock = DockStyle.Fill;
            tlp.Name = $"tlp_{page_ind + 1}";

            tlp.RowCount = 2;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));

            for (int i = 0; i < _dgvsNumberOnPage; ++i)
            {
                tlp.Controls.Add(new Label() { Name = $"label_dgv_{page_ind + 1}_{i}", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, i, 0);

                if (_dgvsNumberOnPage == 2)
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, Math.Abs(Math.Abs((i - 1) * 100) - 100 * _bigDgvSizeCoeff))); // first will have size 1 - BigDgvSizeCoeff, second BigDgvSizeCoeff
                else
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _dgvsNumberOnPage));

                tlp.Controls.Add(CreateDataGridView(page_ind, i), i, 1);
            }

            return tlp;
        }

        private DataGridView CreateDataGridView(int pageIndex, int dgv_ind)
        {
            var dgv = new DataGridView();
            ((ISupportInitialize)dgv).BeginInit();
            dgv.Name = $"dgv_{pageIndex + 1}_{dgv_ind + 1}";
            dgv.Dock = DockStyle.Fill;
            InitDgv(ref dgv);
            ((ISupportInitialize)dgv).EndInit();
            return dgv;
        }

        private void Dgv_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged?.Invoke();
        }

        private void InitDgv(ref DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgv.ReadOnly = true;
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSize = true;
            dgv.DataSourceChanged += Dgv_DataSourceChanged;

            var dataGridViewHeaderStyle = new DataGridViewCellStyle();
            dataGridViewHeaderStyle.BackColor = SystemColors.Control;
            dataGridViewHeaderStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewHeaderStyle.ForeColor = SystemColors.WindowText;
            dataGridViewHeaderStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewHeaderStyle.SelectionForeColor = SystemColors.HighlightText;
            //dataGridViewHeaderStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            //dataGridViewHeaderStyle.WrapMode = DataGridViewTriState.True;

            dgv.ColumnHeadersDefaultCellStyle = dataGridViewHeaderStyle;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            var dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle.BackColor = SystemColors.Window;
            dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.DefaultCellStyle = dataGridViewCellStyle;

            dgv.Margin = new Padding(5);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public DataGridView this[int pageIngex, int dgvIndex]
        {
            get
            {
                return Pages[pageIngex].Controls[0].Controls.OfType<DataGridView>().ToArray()[dgvIndex];
            }
        }

        public DataGridView this[int pageIngex, string dgvName]
        {
            get
            {
                return Pages[pageIngex].Controls[0].Controls.OfType<DataGridView>().ToArray().Where(d => d.Name == dgvName).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns selected rows from last dgv on active page
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRowsLastDGV
        {
            get
            {
                var curPageIndex = Pages.IndexOf(ActiveTabPage);
                return this[curPageIndex, Pages[curPageIndex].Controls[0].Controls.OfType<DataGridView>().Count() - 1].SelectedRows;
            }
        }

        public DataGridViewSelectedRowCollection SelectedRowsFirstDGV
        {
            get
            {
                var curPageIndex = Pages.IndexOf(ActiveTabPage);
                return this[curPageIndex, 0].SelectedRows;
            }
        }

        public int SelectedTabIndex => tabControl.SelectedIndex;

        private bool RowIsVisible(DataGridViewRow row)
        {
            DataGridView dgv = row.DataGridView;
            int firstVisibleRowIndex = dgv.FirstDisplayedCell.RowIndex;
            int lastVisibleRowIndex = firstVisibleRowIndex + dgv.DisplayedRowCount(false) - 1;
            return row.Index >= firstVisibleRowIndex && row.Index <= lastVisibleRowIndex;
        }

        public void InitTabTables<T1, T2>(DataGridView dgv1, DataGridView dgv2, IQueryable<T1> query1, IQueryable<T2> query, bool[] predicatesArray, IEnumerable<string> columnNames)
        {
            try
            {
                dgv1.SuspendLayout();

                dgv1.MultiSelect = false;

                dgv1.SelectionChanged += async (e, s) =>
                {

                    await FillAdditionalTable(dgv2, query, predicatesArray, columnNames);
                };

                dgv1.Scroll += async (s, e) =>
                {
                    if (RowIsVisible(dgv1.Rows[dgv1.RowCount - 1]))
                        await FillMainTable(dgv1, query1);
                };

                dgv1.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_TAB_TABLS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillMainTable<T1>(DataGridView dgv1, IQueryable<T1> query1)
        {
            try
            {
                using (var r = new RegataContext())
                {
                    dgv1.DataSource = await query1.ToArrayAsync();
                }


                dgv1.FirstDisplayedScrollingRowIndex = dgv1.RowCount - 20; ;

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_TBL1_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillAdditionalTable<T>(DataGridView dgv2, IQueryable<T> query2, bool[] predicatesArray, IEnumerable<string> columnNames)
        {
            try
            {

                if (predicatesArray[0]) return;

                var _chosenEntities = new List<T>();

                _chosenEntities.Clear();
                _chosenEntities.Capacity = 499;

                dgv2.DataSource = null;

                using (var r = new RegataContext())
                {
                    _chosenEntities.AddRange(await query2.ToArrayAsync());
                };

                _chosenEntities.TrimExcess();
                dgv2.DataSource = _chosenEntities;
                HideTable2RedundantColumns(columnNames);
                Labels.SetControlsLabels(this);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_TBLS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void HideTable2RedundantColumns(IEnumerable<string> columnNames)
        {
            try
            {
                if (this[1, 1].Columns.Count <= 0) return;

                foreach (var col in columnNames)
                    this[1, 1].Columns[col].Visible = false;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_TAB_HIDE_COLS_UNREG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    }   // public partial class DGVTabPaneControl : UserControl
}       // namespace Regata.Core.UI.WinForms.Controls
