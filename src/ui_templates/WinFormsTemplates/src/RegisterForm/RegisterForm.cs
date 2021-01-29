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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NewForms.Models;

namespace Regata.Core.UI.WinForms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class
    {
        public RegisterForm(string connectionString)
        {
            InitializeComponent();
            InitializeMainTable(connectionString);
        }


        private void InitializeMainTable(string cs)
        {
            this.MainRDGV = new RDataGridView<MainTableModel>(cs);

            ((System.ComponentModel.ISupportInitialize)(this.MainRDGV)).BeginInit();

            MainRDGV.Location = new System.Drawing.Point(14, 35);
            MainRDGV.Margin = new Padding(4, 3, 4, 3);
            MainRDGV.Name = "MainRDGV";
            MainRDGV.RowHeadersVisible = false;
            MainRDGV.SelectionMode = DataGridViewSelectionMode.CellSelect;
            MainRDGV.Size = new System.Drawing.Size(1660, 627);
            MainRDGV.TabIndex = 0;

            MainRDGV.AllowUserToAddRows = false;
            MainRDGV.AllowUserToDeleteRows = false;
            MainRDGV.AllowUserToResizeRows = false;
            MainRDGV.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            MainRDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MainRDGV.BackgroundColor = System.Drawing.Color.White;
            MainRDGV.BorderStyle = BorderStyle.None;
            MainRDGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            var rdgvCellStyle1 = new DataGridViewCellStyle();
            rdgvCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rdgvCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            rdgvCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            rdgvCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            rdgvCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            rdgvCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            rdgvCellStyle1.WrapMode = DataGridViewTriState.True;
            MainRDGV.ColumnHeadersDefaultCellStyle = rdgvCellStyle1;
            MainRDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            var rdgvCellStyle2 = new DataGridViewCellStyle();
            rdgvCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rdgvCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            rdgvCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rdgvCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            rdgvCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            rdgvCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            rdgvCellStyle2.WrapMode = DataGridViewTriState.False;
            MainRDGV.DefaultCellStyle = rdgvCellStyle2;

         
            this.Controls.Add(this.MainRDGV);
            ((System.ComponentModel.ISupportInitialize)(this.MainRDGV)).EndInit();
        }



    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

