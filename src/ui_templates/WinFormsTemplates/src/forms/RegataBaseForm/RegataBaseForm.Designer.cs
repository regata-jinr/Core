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

using Regata.Core.Settings;
using Regata.Core.UI.WinForms.Items;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegataBaseForm
    {
        public StatusStrip StatusStrip;
        public MenuStrip MenuStrip;
        public EnumItem<Language> LangItem;

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
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            StatusStrip                        = new StatusStrip();
            MenuStrip                          = new MenuStrip();
            LangItem                           = new EnumItem<Language>(Language.English);
            
            StatusStrip.SuspendLayout();
            MenuStrip.SuspendLayout();
            SuspendLayout();
           
           
            // 
            // StatusStrip
            // 
            StatusStrip.Location = new Point(0, 944);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(1687, 22);
            StatusStrip.TabIndex = 23;
            StatusStrip.Text = "StatusStrip";
            StatusStrip.Dock = DockStyle.Bottom;
            StatusStrip.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            StatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
           
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
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            MinimumSize = new Size(500,500);
            StartPosition = FormStartPosition.CenterScreen;
            Margin = new Padding(4, 3, 4, 3);
            AutoScroll = true;

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

            ResumeLayout(false);
            PerformLayout();

        }

    } // public partial class RegisterForm<MainTableModel>
}     // namespace Regata.Core.UI.WinForms.Forms
