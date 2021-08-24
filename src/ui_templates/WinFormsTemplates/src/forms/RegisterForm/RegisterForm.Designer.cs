/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms.Controls;
using Regata.Core.UI.WinForms.Items;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegisterForm<MainTableModel>
        where MainTableModel : class, IId
    {
        public RDataGridView<MainTableModel> MainRDGV;
        public DGVTabPaneControl TabsPane;
        public StatusStrip StatusStrip;
        public MenuStrip MenuStrip;
        public ToolStripProgressBar ProgressBar;
        public EnumItem<Language> LangItem;
        public TableLayoutPanel MainTableLayoutPanel;
        public TableLayoutPanel BottomLayoutPanel;

        public GroupBox groupBoxMainRDGV;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                MainRDGV.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeMainTable()
        {

            MainRDGV = new RDataGridView<MainTableModel>();

            ((ISupportInitialize)MainRDGV).BeginInit();
            MainRDGV.SuspendLayout();
            MainRDGV.Location = new Point(10, 35);
            MainRDGV.Margin = new Padding(4, 3, 4, 3);
            MainRDGV.Name = "MainRDGV";
            MainRDGV.RowHeadersVisible = false;
            MainRDGV.SelectionMode = DataGridViewSelectionMode.CellSelect;
            MainRDGV.Size = new Size(1660, 627);
            MainRDGV.TabIndex = 0;

            MainRDGV.AllowUserToAddRows = false;
            MainRDGV.AllowUserToDeleteRows = false;
            MainRDGV.AllowUserToResizeRows = false;
            MainRDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MainRDGV.BackgroundColor = Color.White;
            MainRDGV.BorderStyle = BorderStyle.None;
            MainRDGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            MainRDGV.Dock = DockStyle.Fill;

            var rdgvCellStyle1 = new DataGridViewCellStyle();
            rdgvCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rdgvCellStyle1.BackColor = SystemColors.Control;
            rdgvCellStyle1.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            rdgvCellStyle1.ForeColor = SystemColors.WindowText;
            rdgvCellStyle1.SelectionBackColor = SystemColors.Highlight;
            rdgvCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            rdgvCellStyle1.WrapMode = DataGridViewTriState.True;
            MainRDGV.ColumnHeadersDefaultCellStyle = rdgvCellStyle1;
            MainRDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            var rdgvCellStyle2 = new DataGridViewCellStyle();
            rdgvCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rdgvCellStyle2.BackColor = SystemColors.Window;
            rdgvCellStyle2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdgvCellStyle2.ForeColor = SystemColors.ControlText;
            rdgvCellStyle2.SelectionBackColor = SystemColors.Highlight;
            rdgvCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            rdgvCellStyle2.WrapMode = DataGridViewTriState.False;
            MainRDGV.DefaultCellStyle = rdgvCellStyle2;

            ((ISupportInitialize)MainRDGV).EndInit();
            MainRDGV.ResumeLayout(false);

        }

        private void InitializeTabControl(uint tabsNum, uint dgvsNum, float BigDgvSizeCoeff)
        {
            SuspendLayout();
            TabsPane = new DGVTabPaneControl(tabsNum, dgvsNum, BigDgvSizeCoeff);

            TabsPane.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
           | AnchorStyles.Right)));
            TabsPane.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            TabsPane.Margin = new Padding(4, 3, 4, 3);
            TabsPane.Name = "TabsPane";
            TabsPane.TabIndex = 1;
            TabsPane.Dock = DockStyle.Fill;
            BottomLayoutPanel.Controls.Add(TabsPane, 0, 0);
            ResumeLayout(false);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RegisterForm<MainTableModel>));

            InitializeMainTable();

            StatusStrip                        = new StatusStrip();
            ProgressBar                        = new ToolStripProgressBar();
            MenuStrip                          = new MenuStrip();
            LangItem                           = new EnumItem<Language>(Language.English);
            groupBoxMainRDGV                   = new GroupBox();
            MainTableLayoutPanel               = new TableLayoutPanel();
            BottomLayoutPanel                  = new TableLayoutPanel();

            MainTableLayoutPanel.SuspendLayout();
            BottomLayoutPanel.SuspendLayout();
            groupBoxMainRDGV.SuspendLayout();
            StatusStrip.SuspendLayout();
            MenuStrip.SuspendLayout();
            SuspendLayout();
           
            // 
            // MainTableLayoutPanel
            // 
            MainTableLayoutPanel.ColumnCount = 1;
            MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            MainTableLayoutPanel.RowCount = 2;
            MainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            MainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            MainTableLayoutPanel.TabIndex = 25;
            MainTableLayoutPanel.Dock = DockStyle.Fill;
            MainTableLayoutPanel.Controls.Add(groupBoxMainRDGV, 0, 0);
            MainTableLayoutPanel.AutoScroll = true;


            // 
            // groupBoxMainRDGV
            // 
            groupBoxMainRDGV.Dock = DockStyle.Fill;
            groupBoxMainRDGV.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxMainRDGV.Name = "groupBoxMainRDGV";
            groupBoxMainRDGV.TabIndex = 0;
            groupBoxMainRDGV.TabStop = false;
            groupBoxMainRDGV.Text = "groupBoxMainRDGV";
            groupBoxMainRDGV.Controls.Add(MainRDGV);

            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new ToolStripItem[] {
            ProgressBar});
            StatusStrip.Location = new Point(0, 944);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(1687, 22);
            StatusStrip.TabIndex = 23;
            StatusStrip.Text = "StatusStrip";
            StatusStrip.Dock = DockStyle.Bottom;
            StatusStrip.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            StatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            //StatusStrip.Anchor = AnchorStyles.;
            // 
            // ProgressBar
            // 
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(150, 22);
            ProgressBar.Alignment = ToolStripItemAlignment.Right;
            ProgressBar.Minimum = 0;
            ProgressBar.Step = 1;
            // 
            // MenuStrip
            // 
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.AutoSize = true;
            MenuStrip.TabIndex = 24;
            MenuStrip.Text = "MenuStrip";
            MenuStrip.Dock = DockStyle.Top;
            MenuStrip.Items.Add(LangItem.EnumMenuItem);

            // 
            // BottomLayoutPanel
            // 
            BottomLayoutPanel.ColumnCount = 2;
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.Dock = DockStyle.Fill;
            BottomLayoutPanel.Name = "BottomLayoutPanel";
            BottomLayoutPanel.RowCount = 1;
            BottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent));
            BottomLayoutPanel.TabIndex = 26;
            MainTableLayoutPanel.Controls.Add(BottomLayoutPanel, 0, 1);

          
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            //MinimumSize = new Size(2300,1300);
            StartPosition = FormStartPosition.CenterScreen;
            Margin = new Padding(4, 3, 4, 3);
            AutoScroll = true;

            Controls.Add(MainTableLayoutPanel);
            Controls.Add(StatusStrip);
            Controls.Add(MenuStrip);

            MainMenuStrip = MenuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "RegisterForm";
            Text = "RegisterForm";

            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();

            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();

            groupBoxMainRDGV.ResumeLayout(false);
            MainTableLayoutPanel.ResumeLayout(false);
            BottomLayoutPanel.ResumeLayout(false);

            ResumeLayout(false);
            PerformLayout();

        }

    } // public partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms.Forms
