/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

namespace Regata.UITemplates
{
    public abstract partial class DataTableForm<Model> : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableForm<Model>));

            this.MenuStrip               = new System.Windows.Forms.MenuStrip();
            this.MenuItemMenu            = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMenuLang        = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMenuLangRus     = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMenuLangEng     = new System.Windows.Forms.ToolStripMenuItem();
            this.DataGridView            = new System.Windows.Forms.DataGridView();
            this.FooterStatusStrip       = new System.Windows.Forms.StatusStrip();
            this.FooterStatusLabel       = new System.Windows.Forms.ToolStripStatusLabel();
            this.FooterStatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MenuItemView            = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemViewShowColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonsLayoutPanel      = new System.Windows.Forms.TableLayoutPanel();

            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.FooterStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMenu,
            this.MenuItemView,
            this.MenuItemMenuLang});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MenuStrip.Size = new System.Drawing.Size(1200, 30);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // MenuItemMenu
            // 
            //this.MenuItemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.MenuItemMenuLang});
            this.MenuItemMenu.Name = "MenuItemMenu";
            this.MenuItemMenu.Size = new System.Drawing.Size(63, 24);
            this.MenuItemMenu.Text = "Меню";
            // 
            // ToolStripMenuItemLang
            // 
            this.MenuItemMenuLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMenuLangRus,
            this.MenuItemMenuLangEng});
            this.MenuItemMenuLang.Name = "MenuItemMenuLang";
            this.MenuItemMenuLang.Size = new System.Drawing.Size(255, 24);
            this.MenuItemMenuLang.Text = "Язык";
            // 
            // MenuItemMenuLangRus
            // 
            this.MenuItemMenuLangRus.Name = "MenuItemMenuLangRus";
            this.MenuItemMenuLangRus.Size = new System.Drawing.Size(161, 24);
            this.MenuItemMenuLangRus.Text = "Русский";
            this.MenuItemMenuLangRus.CheckOnClick = true;
            // 
            // MenuItemMenuLangEng
            // 
            this.MenuItemMenuLangEng.Name = "MenuItemMenuLangEng";
            this.MenuItemMenuLangEng.Size = new System.Drawing.Size(161, 24);
            this.MenuItemMenuLangEng.Text = "Английский";
            this.MenuItemMenuLangEng.CheckOnClick = true;
            // 
            // MenuItemView
            // 
            this.MenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemViewShowColumns});
            this.MenuItemView.Name = "MenuItemView";
            this.MenuItemView.Size = new System.Drawing.Size(47, 24);
            this.MenuItemView.Text = "Вид";
            // 
            // MenuItemViewShowColumns
            // 
            this.MenuItemViewShowColumns.Name = "MenuItemViewShowColumns";
            this.MenuItemViewShowColumns.Size = new System.Drawing.Size(224, 24);
            this.MenuItemViewShowColumns.Text = "Показывать столбцы";
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(18, 105);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.Size = new System.Drawing.Size(1164, 553);
            this.DataGridView.TabIndex = 1;
            // 
            // FooterStatusStrip
            // 
            this.FooterStatusStrip.AllowMerge = false;
            this.FooterStatusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FooterStatusStrip.AutoSize = false;
            this.FooterStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FooterStatusStrip.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FooterStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FooterStatusProgressBar,
            this.FooterStatusLabel});
            this.FooterStatusStrip.Location = new System.Drawing.Point(0, 663);
            this.FooterStatusStrip.Name = "FooterStatusStrip";
            this.FooterStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FooterStatusStrip.Size = new System.Drawing.Size(1200, 29);
            this.FooterStatusStrip.TabIndex = 3;
            this.FooterStatusStrip.Text = "FooterStatusStrip";
            // 
            // FooterStatusLabel
            // 
            this.FooterStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FooterStatusLabel.Name = "FooterStatusLabel";
            this.FooterStatusLabel.Size = new System.Drawing.Size(151, 32);
            this.FooterStatusLabel.Text = "";
            this.FooterStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FooterStatusProgressBar
            // 
            this.FooterStatusProgressBar.ForeColor = System.Drawing.Color.ForestGreen;
            this.FooterStatusProgressBar.Name = "FooterStatusProgressBar";
            this.FooterStatusProgressBar.Size = new System.Drawing.Size(200, 31);
            // 
            // ButtonsLayoutPanel
            // 
            this.ButtonsLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonsLayoutPanel.AutoScroll = true;
            this.ButtonsLayoutPanel.ColumnCount = 1;
            this.ButtonsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonsLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.ButtonsLayoutPanel.Location = new System.Drawing.Point(18, 36);
            this.ButtonsLayoutPanel.Name = "ButtonsLayoutPanel";
            this.ButtonsLayoutPanel.RowCount = 1;
            this.ButtonsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonsLayoutPanel.Size = new System.Drawing.Size(1164, 61);
            this.ButtonsLayoutPanel.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.ButtonsLayoutPanel);
            this.Controls.Add(this.FooterStatusStrip);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FaceForm";
            this.Text = "RegataBasicFormTemplate";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.FooterStatusStrip.ResumeLayout(false);
            this.FooterStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.MenuStrip MenuStrip;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemMenu;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemMenuLang;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemMenuLangRus;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemMenuLangEng;
        protected System.Windows.Forms.DataGridView DataGridView;
        protected System.Windows.Forms.StatusStrip FooterStatusStrip;
        protected System.Windows.Forms.ToolStripStatusLabel FooterStatusLabel;
        protected System.Windows.Forms.ToolStripProgressBar FooterStatusProgressBar;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemView;
        protected System.Windows.Forms.ToolStripMenuItem MenuItemViewShowColumns;
        protected System.Windows.Forms.TableLayoutPanel ButtonsLayoutPanel;
    }
}

