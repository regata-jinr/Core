/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Regata.Utilities;

namespace Regata.UITemplates
{
    // TODO: add sort by any column
    // TODO: add tests via test db table
    // TODO: add autoupdate based on github releases
    // TODO: add cicd

    public partial class DataTableForm<Model> : Form
    {
        private readonly BindingList<Model> _viewModels;
        private Settings _settings;

        public DataTableForm(ref BindingList<Model> models, string AssemblyName)
        {
            if (models == null) throw new ArgumentNullException("Data can't be a null");
            if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");

            InitializeComponent();
            _settings = new Settings(AssemblyName);

            if (Labels.CurrentLanguage == Languages.English)
                MenuItemMenuLangEng.Checked = true;
            else
                MenuItemMenuLangRus.Checked = true;

            _viewModels = models;
            DataGridView.DataSource = _viewModels;

            MenuItemMenuLangEng.CheckedChanged += LangStripMenuItem_CheckedChanged;
            MenuItemMenuLangRus.CheckedChanged += LangStripMenuItem_CheckedChanged;

            LangSwitcher.ChangeFormLanguage(this);
            InitializeMenuViewShowColumns();
        }

        private void LangStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var langStrip = sender as ToolStripMenuItem;

            if (langStrip.Checked && langStrip.Name == MenuItemMenuLangEng.Name)
            {
                _settings.CurrentLanguage = Languages.English;
                MenuItemMenuLangRus.Checked = false;
            }

            if (langStrip.Checked && langStrip.Name == MenuItemMenuLangRus.Name)
            {
                _settings.CurrentLanguage = Languages.Russian;
                MenuItemMenuLangEng.Checked = false;
            }
            LangSwitcher.ChangeFormLanguage(this);
            InitializeMenuViewShowColumns();
            _settings.SaveSettings();
        }

        private void InitializeMenuViewShowColumns()
        {
            MenuItemViewShowColumns.DropDownItems.Clear();
            foreach (DataGridViewColumn col in DataGridView.Columns)
            {
                var t = new ToolStripMenuItem { Name = col.Name, Text = col.HeaderText, CheckOnClick = true, Checked = true};

                if (_settings.NonDisplayedColumns.Contains(t.Name))
                {
                    t.Checked = false;
                    col.Visible = false;
                }
                t.CheckedChanged += ShowColumns_CheckedChanged;
                MenuItemViewShowColumns.DropDownItems.AddRange(new ToolStripMenuItem[1] {t});
            }
        }

        private void ShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            var t = sender as ToolStripMenuItem;

            if (!t.Checked)
                _settings.NonDisplayedColumns.Add(t.Name);
            else
            {
                if (_settings.NonDisplayedColumns.Contains(t.Name))
                    _settings.NonDisplayedColumns.Remove(t.Name);
            }
            _settings.SaveSettings();

            DataGridView.Columns[t.Name].Visible = t.Checked;
        }

        public void AddButtonToLayout(ref Button btn)
        {
            btn.Size = new System.Drawing.Size(120, 35);
            ButtonsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ButtonsLayoutPanel.Controls.Add(btn);
        }

    } // public partial class DataTableForm<Model> : Form
} // Regata.UITemplates
