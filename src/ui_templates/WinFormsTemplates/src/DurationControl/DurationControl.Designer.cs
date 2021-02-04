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

namespace Regata.Core.UI.WinForms
{
    partial class DurationControl
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
                DurationControlNumericUpDownDays.ValueChanged -= DurationControlNumericUpDownDays_ValueChanged;
                DurationControlNumericUpDownHours.ValueChanged -= DurationControlNumericUpDownHours_ValueChanged;
                DurationControlNumericUpDownMinutes.ValueChanged -= DurationControlNumericUpDownMinutes_ValueChanged;
                DurationControlNumericUpDownSeconds.ValueChanged -= DurationControlNumericUpDownSeconds_ValueChanged;
                components.Dispose();

            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DurationControlLabelDays = new System.Windows.Forms.Label();
            this.DurationControlNumericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.DurationControlNumericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.DurationControlLabelMinutes = new System.Windows.Forms.Label();
            this.DurationControlNumericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.DurationControlLabelSeconds = new System.Windows.Forms.Label();
            this.DurationControlLabelHours = new System.Windows.Forms.Label();
            this.DurationControlNumericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.DurationControlGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownHours)).BeginInit();
            this.DurationControlGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DurationControlLabelDays
            // 
            this.DurationControlLabelDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DurationControlLabelDays.Location = new System.Drawing.Point(4, 19);
            this.DurationControlLabelDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DurationControlLabelDays.Name = "DurationControlLabelDays";
            this.DurationControlLabelDays.Size = new System.Drawing.Size(56, 24);
            this.DurationControlLabelDays.TabIndex = 24;
            this.DurationControlLabelDays.Text = "Дни";
            this.DurationControlLabelDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DurationControlNumericUpDownDays
            // 
            this.DurationControlNumericUpDownDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DurationControlNumericUpDownDays.Location = new System.Drawing.Point(4, 47);
            this.DurationControlNumericUpDownDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DurationControlNumericUpDownDays.Name = "DurationControlNumericUpDownDays";
            this.DurationControlNumericUpDownDays.Size = new System.Drawing.Size(56, 22);
            this.DurationControlNumericUpDownDays.TabIndex = 23;
            // 
            // DurationControlNumericUpDownMinutes
            // 
            this.DurationControlNumericUpDownMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DurationControlNumericUpDownMinutes.Location = new System.Drawing.Point(130, 47);
            this.DurationControlNumericUpDownMinutes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DurationControlNumericUpDownMinutes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.DurationControlNumericUpDownMinutes.Name = "DurationControlNumericUpDownMinutes";
            this.DurationControlNumericUpDownMinutes.Size = new System.Drawing.Size(58, 22);
            this.DurationControlNumericUpDownMinutes.TabIndex = 21;
            // 
            // DurationControlLabelMinutes
            // 
            this.DurationControlLabelMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DurationControlLabelMinutes.Location = new System.Drawing.Point(130, 19);
            this.DurationControlLabelMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DurationControlLabelMinutes.Name = "DurationControlLabelMinutes";
            this.DurationControlLabelMinutes.Size = new System.Drawing.Size(58, 24);
            this.DurationControlLabelMinutes.TabIndex = 22;
            this.DurationControlLabelMinutes.Text = "Мин.";
            this.DurationControlLabelMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DurationControlNumericUpDownSeconds
            // 
            this.DurationControlNumericUpDownSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DurationControlNumericUpDownSeconds.Location = new System.Drawing.Point(195, 47);
            this.DurationControlNumericUpDownSeconds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DurationControlNumericUpDownSeconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DurationControlNumericUpDownSeconds.Name = "DurationControlNumericUpDownSeconds";
            this.DurationControlNumericUpDownSeconds.Size = new System.Drawing.Size(52, 22);
            this.DurationControlNumericUpDownSeconds.TabIndex = 17;
            // 
            // DurationControlLabelSeconds
            // 
            this.DurationControlLabelSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DurationControlLabelSeconds.Location = new System.Drawing.Point(195, 19);
            this.DurationControlLabelSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DurationControlLabelSeconds.Name = "DurationControlLabelSeconds";
            this.DurationControlLabelSeconds.Size = new System.Drawing.Size(52, 24);
            this.DurationControlLabelSeconds.TabIndex = 18;
            this.DurationControlLabelSeconds.Text = "Сек.";
            this.DurationControlLabelSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DurationControlLabelHours
            // 
            this.DurationControlLabelHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DurationControlLabelHours.Location = new System.Drawing.Point(67, 19);
            this.DurationControlLabelHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DurationControlLabelHours.Name = "DurationControlLabelHours";
            this.DurationControlLabelHours.Size = new System.Drawing.Size(56, 24);
            this.DurationControlLabelHours.TabIndex = 20;
            this.DurationControlLabelHours.Text = "Часы";
            this.DurationControlLabelHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DurationControlNumericUpDownHours
            // 
            this.DurationControlNumericUpDownHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DurationControlNumericUpDownHours.Location = new System.Drawing.Point(67, 47);
            this.DurationControlNumericUpDownHours.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DurationControlNumericUpDownHours.Name = "DurationControlNumericUpDownHours";
            this.DurationControlNumericUpDownHours.Size = new System.Drawing.Size(56, 22);
            this.DurationControlNumericUpDownHours.TabIndex = 19;
            // 
            // DurationControlGroupBox
            // 
            this.DurationControlGroupBox.Controls.Add(this.DurationControlLabelMinutes);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlLabelDays);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlNumericUpDownHours);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlNumericUpDownDays);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlLabelHours);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlNumericUpDownMinutes);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlLabelSeconds);
            this.DurationControlGroupBox.Controls.Add(this.DurationControlNumericUpDownSeconds);
            this.DurationControlGroupBox.Location = new System.Drawing.Point(0, 0);
            this.DurationControlGroupBox.Name = "DurationControlGroupBox";
            this.DurationControlGroupBox.Size = new System.Drawing.Size(279, 87);
            this.DurationControlGroupBox.TabIndex = 25;
            this.DurationControlGroupBox.TabStop = false;
            this.DurationControlGroupBox.Text = "Duration";
            // 
            // DurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DurationControlGroupBox);
            this.Name = "DurationControl";
            this.Size = new System.Drawing.Size(285, 90);
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationControlNumericUpDownHours)).EndInit();
            this.DurationControlGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DurationControlLabelDays;
        private System.Windows.Forms.NumericUpDown DurationControlNumericUpDownDays;
        private System.Windows.Forms.NumericUpDown DurationControlNumericUpDownMinutes;
        private System.Windows.Forms.Label DurationControlLabelMinutes;
        private System.Windows.Forms.NumericUpDown DurationControlNumericUpDownSeconds;
        private System.Windows.Forms.Label DurationControlLabelSeconds;
        private System.Windows.Forms.Label DurationControlLabelHours;
        private System.Windows.Forms.NumericUpDown DurationControlNumericUpDownHours;
        private System.Windows.Forms.GroupBox DurationControlGroupBox;
    } // partial class DurationControl
}     // namespace Regata.Core.UI.WinForms
