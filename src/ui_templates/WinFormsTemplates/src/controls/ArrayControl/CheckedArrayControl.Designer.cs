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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RBV_groupBoxTitle.SuspendLayout();
            flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(337, 80);
            this.flowLayoutPanel.TabIndex = 0;

            // 
            // RBV_groupBoxTitle
            // 
            this.RBV_groupBoxTitle.Controls.Add(this.flowLayoutPanel);
            this.RBV_groupBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RBV_groupBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.RBV_groupBoxTitle.Name = "RBV_groupBoxTitle";
            this.RBV_groupBoxTitle.Size = new System.Drawing.Size(343, 102);
            this.RBV_groupBoxTitle.TabIndex = 0;
            this.RBV_groupBoxTitle.TabStop = false;
            this.RBV_groupBoxTitle.Text = "RBV_groupBoxTitle";
           
            // 
            // CheckedArrayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RBV_groupBoxTitle);
            this.Name = "CheckedArrayControl";
            this.Size = new System.Drawing.Size(343, 102);

        }

        public GroupBox RBV_groupBoxTitle;
        public FlowLayoutPanel flowLayoutPanel;

    } // partial class CheckedArrayControl<T> : UserControl
}     // namespace Regata.Core.UI.WinForms.Controls
