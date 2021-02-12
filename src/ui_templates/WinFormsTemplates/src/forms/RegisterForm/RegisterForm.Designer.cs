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

using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Regata.Core.UI.WinForms.Controls;
using Regata.Core.UI.WinForms.Items;
using Regata.Core.Settings;

namespace Regata.Core.UI.WinForms.Forms
{
    partial class RegisterForm<MainTableModel>
        where MainTableModel : class
    {

        public RDataGridView<MainTableModel> MainRDGV;
        public DGVTabPaneControl TabsPane;
        public StatusStrip StatusStrip;
        public MenuStrip MenuStrip;
        public ToolStripProgressBar ProgressBar;
        public EnumItem<Language> LangItem;
        public TableLayoutPanel FunctionalLayoutPanel;


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            base.Dispose(disposing);
        }

        private void InitializeMainTable()
        {
            MainRDGV = new RDataGridView<MainTableModel>();

            ((ISupportInitialize)MainRDGV).BeginInit();

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
            MainRDGV.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            MainRDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MainRDGV.BackgroundColor = Color.White;
            MainRDGV.BorderStyle = BorderStyle.None;
            MainRDGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

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

            Controls.Add(MainRDGV);
            ((ISupportInitialize)MainRDGV).EndInit();
        }


        private void InitializeTabControl(uint tabsNum, uint dgvsNum, float BigDgvSizeCoeff)
        {
            TabsPane = new DGVTabPaneControl(tabsNum, dgvsNum, BigDgvSizeCoeff);

            TabsPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
            TabsPane.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            TabsPane.Location = new System.Drawing.Point(11, 674);
            TabsPane.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabsPane.Name = "TabsPane";
            TabsPane.Size = new System.Drawing.Size(918, 253);
            TabsPane.TabIndex = 1;
            this.Controls.Add(TabsPane);

        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm<MainTableModel>));
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            LangItem = new EnumItem<Language>(Language.English);


            this.StatusStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 944);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1687, 22);
            this.StatusStrip.TabIndex = 23;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1687, 24);
            this.MenuStrip.TabIndex = 24;
            this.MenuStrip.Text = "MenuStrip";
            MenuStrip.Items.Add(LangItem.EnumMenuItem);

            //
            // LangItem
            //

            // 
            // FunctionalLayoutPanel
            // 
            FunctionalLayoutPanel = new TableLayoutPanel();
            FunctionalLayoutPanel.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
            | AnchorStyles.Right)));
            FunctionalLayoutPanel.ColumnCount = 2;
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.Location = new System.Drawing.Point(833, 674);
            FunctionalLayoutPanel.Name = "FunctionalLayoutPanel";
            FunctionalLayoutPanel.RowCount = 2;
            FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.Size = new Size(837, 253);
            FunctionalLayoutPanel.TabIndex = 25;

            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1687, 966);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.FunctionalLayoutPanel);

            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RegisterForm";
            this.Text = "Form1";
          
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    } // partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms
