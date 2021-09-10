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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{
    public partial class CheckedArrayControl<T> : UserControl //, IArrayControlSingleSelection<T>, IArrayControlMultiSelection<T>
    {
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
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RBV_groupBoxTitle = new GroupBox();
            checkedListBox = new CheckedListBox();
            
            checkedListBox.SuspendLayout();
            RBV_groupBoxTitle.SuspendLayout();
            SuspendLayout();

            // 
            // checkedListBox
            // 
            checkedListBox.BackColor = SystemColors.Control;
            checkedListBox.CheckOnClick = true;
            checkedListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkedListBox.FormattingEnabled = true;
            checkedListBox.ScrollAlwaysVisible = false;
            checkedListBox.HorizontalScrollbar = false;
            checkedListBox.MultiColumn = true;
            checkedListBox.Name = "checkedListBox";
            checkedListBox.RightToLeft = RightToLeft.No;
            checkedListBox.IntegralHeight = false;
            checkedListBox.Dock = DockStyle.Fill;
            checkedListBox.Sorted = true;
            checkedListBox.TabIndex = 18;
            checkedListBox.UseCompatibleTextRendering = true;
            checkedListBox.SelectionMode = SelectionMode.One;
            
            // 
            // RBV_groupBoxTitle
            // 
            RBV_groupBoxTitle.Controls.Add(checkedListBox);
            RBV_groupBoxTitle.Dock = DockStyle.Fill;
            RBV_groupBoxTitle.Name = "RBV_groupBoxTitle";
            RBV_groupBoxTitle.TabIndex = 0;
            RBV_groupBoxTitle.TabStop = false;
            RBV_groupBoxTitle.Text = "RBV_groupBoxTitle";
            
           
            // 
            // CheckedArrayControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(RBV_groupBoxTitle);
            Name = "CheckedArrayControl";
            Dock = DockStyle.Fill;

        }

        private GroupBox RBV_groupBoxTitle;
        public CheckedListBox checkedListBox;

    } // partial class CheckedArrayControl<T> : UserControl
}     // namespace Regata.Core.UI.WinForms.Controls
