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

namespace Regata.Core.UI.WinForms.Forms
{
    partial class RegisterForm<MainTableModel>
        where MainTableModel : class
    {

        public RDataGridView<MainTableModel> MainRDGV;
        public DGVTabPaneControl TabsPane;
        public StatusStrip statusStrip1;
        public MenuStrip menuStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
        public ToolStripProgressBar toolStripProgressBar1;

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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
           
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 944);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1687, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1687, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1687, 966);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
          
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RegisterForm";
            this.Text = "Form1";
          
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    } // partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms
