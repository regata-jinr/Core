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

using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

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
            this.RBV_groupBoxTitle = new System.Windows.Forms.GroupBox();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RBV_groupBoxTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // RBV_groupBoxTitle
            // 
            this.RBV_groupBoxTitle.Controls.Add(flowLayoutPanel);
            this.RBV_groupBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RBV_groupBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.RBV_groupBoxTitle.Name = "RBV_groupBoxTitle";
            this.RBV_groupBoxTitle.Size = new System.Drawing.Size(343, 102);
            this.RBV_groupBoxTitle.TabIndex = 0;
            this.RBV_groupBoxTitle.TabStop = false;
            this.RBV_groupBoxTitle.Text = "RBV_groupBoxTitle";
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(3, 19);
            flowLayoutPanel.Name = "flowLayoutPanel1";
            flowLayoutPanel.TabIndex = 0;
            flowLayoutPanel.AutoScroll = true;
            // 
            // CheckedArrayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RBV_groupBoxTitle);
            this.Name = "CheckedArrayControl";
            this.Size = new System.Drawing.Size(343, 102);
            this.RBV_groupBoxTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private GroupBox RBV_groupBoxTitle;
        private FlowLayoutPanel flowLayoutPanel;
    } // partial class RadioButtonsView
}     // namespace Regata.Core.UI.WinForms.Controls
