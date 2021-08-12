/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Windows.Forms;
using System.Drawing;

namespace Regata.Core.UI.WinForms.Forms
{
    partial class SamplesToDetectorsForm
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainLabelDesc = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // MainLabelDesc
            // 
            MainLabelDesc.Font = new Font("Segoe UI", 14.25F);
            MainLabelDesc.Location = new Point(0, 11);
            MainLabelDesc.Name = "labelExpl";
            MainLabelDesc.Dock = DockStyle.Fill;
            MainLabelDesc.TabIndex = 5;
            MainLabelDesc.TextAlign = ContentAlignment.MiddleLeft;
            MainLabelDesc.UseCompatibleTextRendering = true;


            buttonExportToCSV = new Button() { Name = "buttonExportToCSV",  UseVisualStyleBackColor = true };
            buttonExportToExcel = new Button() { Name = "buttonExportToExcel", UseVisualStyleBackColor = true };
            buttonFillMeasurementRegister = new Button() { Name = "buttonFillMeasurementRegister", UseVisualStyleBackColor = true };


            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            tableLayoutPanelMain.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanelMain.Location = new Point(13, 578);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelMain.Controls.Add(MainLabelDesc, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.TabIndex = 9;
            // 
            // SamplesToDetectors
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(342, 724);
            Controls.Add(tableLayoutPanelMain);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SamplesToDetectorsForm";

        }

        private void ResumeLayouts()
        {
            tableLayoutPanelMain.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private TableLayoutPanel tableLayoutPanelMain;
        private Label MainLabelDesc;
        public Button buttonExportToCSV;
        public Button buttonExportToExcel;
        public Button buttonFillMeasurementRegister;
    }
}