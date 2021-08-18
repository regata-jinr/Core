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


namespace Regata.Core.UI.WinForms.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLoginFormUser = new System.Windows.Forms.TextBox();
            this.textBoxLoginFormPassword = new System.Windows.Forms.TextBox();
            this.buttonLoginFormCreatePin = new System.Windows.Forms.Button();
            this.buttonLoginFormEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label_login_title";
            this.label1.Size = new System.Drawing.Size(75, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label_pin_title";
            this.label2.Size = new System.Drawing.Size(75, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxLoginFormUser
            // 
            this.textBoxLoginFormUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLoginFormUser.Location = new System.Drawing.Point(101, 24);
            this.textBoxLoginFormUser.Name = "textBoxLoginFormUser";
            this.textBoxLoginFormUser.Size = new System.Drawing.Size(136, 22);
            this.textBoxLoginFormUser.TabIndex = 2;
            this.textBoxLoginFormUser.LostFocus += new System.EventHandler(this.DoesPinExist);
            // 
            // textBoxLoginFormPassword
            // 
            this.textBoxLoginFormPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLoginFormPassword.Location = new System.Drawing.Point(101, 66);
            this.textBoxLoginFormPassword.Name = "textBoxLoginFormPassword";
            this.textBoxLoginFormPassword.PasswordChar = 'x';
            this.textBoxLoginFormPassword.Size = new System.Drawing.Size(136, 22);
            this.textBoxLoginFormPassword.TabIndex = 3;
            // 
            // buttonLoginFormCreatePin
            // 
            this.buttonLoginFormCreatePin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoginFormCreatePin.Location = new System.Drawing.Point(19, 111);
            this.buttonLoginFormCreatePin.Name = "buttonLoginFormCreatePin";
            this.buttonLoginFormCreatePin.Size = new System.Drawing.Size(75, 45);
            this.buttonLoginFormCreatePin.TabIndex = 4;
            this.buttonLoginFormCreatePin.Text = "Создать пин-код";
            this.buttonLoginFormCreatePin.UseVisualStyleBackColor = true;
            this.buttonLoginFormCreatePin.Click += new System.EventHandler(this.ButtonLoginFormCreatePin_Click);
            // 
            // buttonLoginFormEnter
            // 
            this.buttonLoginFormEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoginFormEnter.Location = new System.Drawing.Point(162, 111);
            this.buttonLoginFormEnter.Name = "buttonLoginFormEnter";
            this.buttonLoginFormEnter.Size = new System.Drawing.Size(75, 45);
            this.buttonLoginFormEnter.TabIndex = 4;
            this.buttonLoginFormEnter.Text = "Войти";
            this.buttonLoginFormEnter.UseVisualStyleBackColor = true;
            this.buttonLoginFormEnter.Click += new System.EventHandler(this.ButtonLoginFormEnter_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLoginFormEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 164);
            this.Controls.Add(this.buttonLoginFormEnter);
            this.Controls.Add(this.buttonLoginFormCreatePin);
            this.Controls.Add(this.textBoxLoginFormPassword);
            this.Controls.Add(this.textBoxLoginFormUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLoginFormUser;
        private System.Windows.Forms.TextBox textBoxLoginFormPassword;
        private System.Windows.Forms.Button buttonLoginFormCreatePin;
        private System.Windows.Forms.Button buttonLoginFormEnter;
    }
}