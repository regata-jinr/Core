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
        public TableLayoutPanel MainTableLayoutPanel;
        public GroupBox groupBoxMainRDGV;

        public GroupBox groupBoxRegForm;
        public TableLayoutPanel tableLayoutPanelRegForm;
        public Button buttonRemoveSample;
        public Button buttonAddAllSamples;
        public Button buttonAddSampleToReg;
        public Button buttonShowAcqQueue;

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
                //LangItem.CheckedChanged            -= LabelsLanguageItemChanged;
                //GlobalSettings.LanguageChanged     -= LanguageChanged;
                //TabsPane.DataSourceChanged         -= LanguageChanged;
                //ControlAdded                       -= (s, e) => LanguageChanged();
                //MenuStrip.ItemAdded                -= (s, e) => LanguageChanged();
                //StatusStrip.ItemAdded              -= (s, e) => LanguageChanged();
                //FunctionalLayoutPanel.ControlAdded -= (s, e) => LanguageChanged();
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
            groupBoxRegForm                    = new GroupBox();
            tableLayoutPanelRegForm            = new TableLayoutPanel();
            buttonAddAllSamples                = new Button();
            buttonAddSampleToReg               = new Button();
            buttonRemoveSample                 = new Button();
            buttonShowAcqQueue                 = new Button();
            groupBoxMainRDGV                   = new GroupBox();
            MainTableLayoutPanel               = new TableLayoutPanel();
            FunctionalLayoutPanel              = new TableLayoutPanel();
            BottomLayoutPanel                  = new TableLayoutPanel();

            FunctionalLayoutPanel.SuspendLayout();
            BottomLayoutPanel.SuspendLayout();
            MainTableLayoutPanel.SuspendLayout();
            groupBoxMainRDGV.SuspendLayout();
            groupBoxRegForm.SuspendLayout();
            tableLayoutPanelRegForm.SuspendLayout();
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
            // FunctionalLayoutPanel
            // 
            FunctionalLayoutPanel.ColumnCount = 3;
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            FunctionalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            FunctionalLayoutPanel.Name = "FunctionalLayoutPanel";
            FunctionalLayoutPanel.RowCount = 1;
            //FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            //FunctionalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            FunctionalLayoutPanel.TabIndex = 25;
            FunctionalLayoutPanel.Dock = DockStyle.Fill;
            FunctionalLayoutPanel.Controls.Add(groupBoxRegForm, 0, 0);

            // 
            // BottomLayoutPanel
            // 
            BottomLayoutPanel.ColumnCount = 2;
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BottomLayoutPanel.Controls.Add(FunctionalLayoutPanel, 1, 0);
            BottomLayoutPanel.Dock = DockStyle.Fill;
            BottomLayoutPanel.Name = "BottomLayoutPanel";
            BottomLayoutPanel.RowCount = 1;
            BottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent));
            BottomLayoutPanel.TabIndex = 26;
            MainTableLayoutPanel.Controls.Add(BottomLayoutPanel, 0, 1);

            // 
            // groupBoxRegForm
            // 
            groupBoxRegForm.Controls.Add(tableLayoutPanelRegForm);
            groupBoxRegForm.Dock = DockStyle.Fill;
            groupBoxRegForm.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxRegForm.Name = "groupBoxRegForm";
            groupBoxRegForm.TabIndex = 0;
            groupBoxRegForm.TabStop = false;
            groupBoxRegForm.Text = "Формирование журнала";
            // 
            // tableLayoutPanelRegForm
            // 
            tableLayoutPanelRegForm.ColumnCount = 1;
            tableLayoutPanelRegForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRegForm.Controls.Add(buttonShowAcqQueue, 0, 3);
            tableLayoutPanelRegForm.Controls.Add(buttonRemoveSample, 0, 2);
            tableLayoutPanelRegForm.Controls.Add(buttonAddAllSamples, 0, 1);
            tableLayoutPanelRegForm.Controls.Add(buttonAddSampleToReg, 0, 0);
            tableLayoutPanelRegForm.Dock = DockStyle.Fill;
            tableLayoutPanelRegForm.Location = new Point(3, 25);
            tableLayoutPanelRegForm.Name = "tableLayoutPanelRegForm";
            tableLayoutPanelRegForm.RowCount = 4;
            tableLayoutPanelRegForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanelRegForm.RowCount));
            tableLayoutPanelRegForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanelRegForm.RowCount));
            tableLayoutPanelRegForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanelRegForm.RowCount));
            tableLayoutPanelRegForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanelRegForm.RowCount));
            tableLayoutPanelRegForm.Size = new Size(267, 236);
            tableLayoutPanelRegForm.TabIndex = 0;
            tableLayoutPanelRegForm.AutoScroll = true;

            // 
            // buttonAddSampleToReg
            // 
            buttonAddSampleToReg.Dock = DockStyle.Fill;
            buttonAddSampleToReg.Location = new Point(3, 3);
            buttonAddSampleToReg.Name = "buttonAddSampleToReg";
            buttonAddSampleToReg.Size = new Size(261, 72);
            buttonAddSampleToReg.TabIndex = 0;
            buttonAddSampleToReg.UseVisualStyleBackColor = true;
            // 
            // buttonAddAllSamples
            // 
            buttonAddAllSamples.Dock = DockStyle.Fill;
            buttonAddAllSamples.Location = new Point(3, 81);
            buttonAddAllSamples.Name = "buttonAddAllSamples";
            buttonAddAllSamples.Size = new Size(261, 72);
            buttonAddAllSamples.TabIndex = 1;
            buttonAddAllSamples.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveSample
            // 
            buttonRemoveSample.Dock = DockStyle.Fill;
            buttonRemoveSample.Location = new Point(3, 159);
            buttonRemoveSample.Name = "buttonRemoveSample";
            buttonRemoveSample.Size = new Size(261, 74);
            buttonRemoveSample.TabIndex = 2;
            buttonRemoveSample.UseVisualStyleBackColor = true;
            // 
            // buttonShowAcqQueue
            // 
            buttonShowAcqQueue.Dock = DockStyle.Fill;
            buttonShowAcqQueue.Location = new Point(3, 159);
            buttonShowAcqQueue.Name = "buttonShowAcqQueue";
            buttonShowAcqQueue.Size = new Size(261, 74);
            buttonShowAcqQueue.TabIndex = 3;
            buttonShowAcqQueue.UseVisualStyleBackColor = true;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1700, 900);
            MinimumSize = new Size(1500,900);
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

            groupBoxRegForm.ResumeLayout(false);
            groupBoxMainRDGV.ResumeLayout(false);
            tableLayoutPanelRegForm.ResumeLayout(false);
            FunctionalLayoutPanel.ResumeLayout(false);
            BottomLayoutPanel.ResumeLayout(false);
            MainTableLayoutPanel.ResumeLayout(false);

            ResumeLayout(false);
            PerformLayout();

        }

    } // partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms.Forms
