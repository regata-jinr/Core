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

using System.Drawing;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.TabControl;
using System.ComponentModel;

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

            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 10.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;

            dgv.DefaultCellStyle = dataGridViewCellStyle2;

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

    }   // public partial class DGVTabPaneControl : UserControl
}     // namespace WinFormsTemplates.src.RegataTabPaneControl
