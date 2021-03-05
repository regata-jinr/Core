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
        public TableLayoutPanel BottomLayoutPanel;

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
                LangItem.CheckedChanged            -= LabelsLanguageItemChanged;
                GlobalSettings.LanguageChanged     -= LanguageChanged;
                TabsPane.DataSourceChanged         -= LanguageChanged;
                ControlAdded                       -= (s, e) => LanguageChanged();
                MenuStrip.ItemAdded                -= (s, e) => LanguageChanged();
                StatusStrip.ItemAdded              -= (s, e) => LanguageChanged();
                FunctionalLayoutPanel.ControlAdded -= (s, e) => LanguageChanged();
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
            MainRDGV.ResumeLayout(false);

        }

        private void InitializeTabControl(uint tabsNum, uint dgvsNum, float BigDgvSizeCoeff)
        {
            SuspendLayout();
            TabsPane = new DGVTabPaneControl(tabsNum, dgvsNum, BigDgvSizeCoeff);

            TabsPane.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
           | AnchorStyles.Right)));
            TabsPane.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            TabsPane.Location = new Point(11, 674);
            TabsPane.Margin = new Padding(4, 3, 4, 3);
            TabsPane.Name = "TabsPane";
            TabsPane.Size = new Size(815, 253);
            TabsPane.TabIndex = 1;
            //Controls.Add(TabsPane);
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
            StatusStrip = new StatusStrip();
            ProgressBar = new ToolStripProgressBar();
            MenuStrip = new MenuStrip();
            LangItem = new EnumItem<Language>(Language.English);

            StatusStrip.SuspendLayout();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            
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
            //StatusStrip.Anchor = AnchorStyles.;
            // 
            // ProgressBar
            // 
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(100, 32);
            // 
            // MenuStrip
            // 
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(1687, 24);
            MenuStrip.TabIndex = 24;
            MenuStrip.Text = "MenuStrip";
            MenuStrip.Dock = DockStyle.Top;
            MenuStrip.Items.Add(LangItem.EnumMenuItem);

            // 
            // FunctionalLayoutPanel
            // 
            FunctionalLayoutPanel = new TableLayoutPanel();
            FunctionalLayoutPanel.SuspendLayout();
            FunctionalLayoutPanel.ColumnCount = 2;
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.Location = new Point(833, 674);
            FunctionalLayoutPanel.Name = "FunctionalLayoutPanel";
            FunctionalLayoutPanel.RowCount = 2;
            FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.Size = new Size(837, 253);
            FunctionalLayoutPanel.TabIndex = 25;
            FunctionalLayoutPanel.Dock = DockStyle.Fill;

            // 
            // BottomLayoutPanel
            // 
            BottomLayoutPanel = new TableLayoutPanel();
            BottomLayoutPanel.SuspendLayout();
            BottomLayoutPanel.ColumnCount = 2;
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.Controls.Add(FunctionalLayoutPanel, 1, 0);
            BottomLayoutPanel.Dock = DockStyle.Bottom;
            BottomLayoutPanel.Location = new Point(0, 668);
            BottomLayoutPanel.Name = "BottomLayoutPanel";
            BottomLayoutPanel.RowCount = 1;
            BottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            BottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            BottomLayoutPanel.Size = new Size(1687, 276);
            BottomLayoutPanel.TabIndex = 26;

            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1687, 966);
            Controls.Add(MenuStrip);
            Controls.Add(BottomLayoutPanel);
            Controls.Add(StatusStrip);
            //Controls.Add(FunctionalLayoutPanel);

            MainMenuStrip = MenuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "RegisterForm";
            Text = "RegisterForm";
          
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            FunctionalLayoutPanel.ResumeLayout(false);
            BottomLayoutPanel.ResumeLayout(false);
            PerformLayout();

        }

    } // partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms.Forms
