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
using System.Drawing;
using System.ComponentModel;
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
            MainRDGV = new RDataGridView<MainTableModel>(cs);

            ((ISupportInitialize)MainRDGV).BeginInit();

            MainRDGV.Location = new Point(14, 35);
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



    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

