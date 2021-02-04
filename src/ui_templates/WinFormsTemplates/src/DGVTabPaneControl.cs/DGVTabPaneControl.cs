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
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.TabControl;

namespace Regata.Core.UI.WinForms
{
    public partial class DGVTabPaneControl : UserControl
    {
        public TabPage ActiveTabPage => tabControl.SelectedTab; 

        public TabPageCollection Pages => tabControl.TabPages;

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
                CreateTab($"tabPage{i+1}");
                SetUpDGVs(i);
            }


        }

        private void CreateTab(string name)
        {
            Pages.Add(name,name);
        }

        private void SetUpDGVs(int pageIndex)
        {
            for (var i = 0; i < _dgvsNumberOnPage; ++i)
            {
                var dgv = new DataGridView();
                dgv.Name = $"dgv_{pageIndex+1}_{i + 1}";
                InitDgv(ref dgv);

                int y_margin = 40;

                if (i == _dgvsNumberOnPage - 1)
                {
                    dgv.Size = new Size((int)(this.Size.Width * _bigDgvSizeCoeff) - 10, this.Size.Height - y_margin);
                }
                else
                {
                    dgv.Size = new Size((int)(this.Size.Width * (1 - _bigDgvSizeCoeff)) - 10, this.Size.Height - y_margin);
                }

                Pages[pageIndex].Controls.Add(dgv);

                if (i != 0)
                {
                    dgv.Location = new Point(this[pageIndex, i - 1].Size.Width + 10, y_margin);
                }
                else
                {
                    dgv.Location = new Point(0, y_margin);
                }

                AddDgvLabel(pageIndex, $"label_dgv_{pageIndex+1}_{i + 1}", new Point(dgv.Location.X,  dgv.Location.Y - y_margin / 2));
            }
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

            dgv.Margin = new Padding(4, 3, 4, 3);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AddDgvLabel(int pageIndex, string name, Point position)
        {
            var lbl = new Label();
            lbl.Name = name;
            lbl.Text = name;
            lbl.Location = position;

            Pages[pageIndex].Controls.Add(lbl);
            
        }


        public DataGridView this[int pageIngex, int dgvIndex]
        {
            get
            {
                return Pages[pageIngex].Controls.OfType<DataGridView>().ToArray()[dgvIndex];
            }
        }

        public DataGridView this[int pageIngex, string dgvName]
        {
            get
            {
                return Pages[pageIngex].Controls.OfType<DataGridView>().ToArray().Where(d => d.Name == dgvName).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns selected rows from last dgv on active page
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                var curPageIndex = Pages.IndexOf(ActiveTabPage);
                return this[curPageIndex, Pages[curPageIndex].Controls.OfType<DataGridView>().Count() - 1].SelectedRows;
            }
        }

    } // public partial class DGVTabPaneControl : UserControl
}     // namespace WinFormsTemplates.src.RegataTabPaneControl
