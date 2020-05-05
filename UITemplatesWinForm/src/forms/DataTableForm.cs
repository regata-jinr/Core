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

namespace Regata.UITemplates
{
    // TODO:  add sort by any column
    // FIXME: there is not possible to pass side labels
    // TODO:  add tests via test db table
    // TODO:  add autoupdate based on github releases
    // TODO:  add cicd

    public abstract partial class DataTableForm<Model> : Form
    {
        public readonly BindingList<Model> Data;
        private Settings _settings;

        public DataTableForm(string AssemblyName)
        {
            if (string.IsNullOrEmpty(AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");

            InitializeComponent();
            Settings.AssemblyName = AssemblyName;
            _settings = new Settings();

            _settings.LanguageChanged += () => ChangeLanguageOfControlsTexts(Controls);

            if (_settings.CurrentLanguage == Languages.English)
                MenuItemMenuLangEng.Checked = true;
            else
                MenuItemMenuLangRus.Checked = true;

            Data = new BindingList<Model>();
            DataGridView.DataSource = Data;

            MenuItemMenuLangEng.CheckedChanged += LangStripMenuItem_CheckedChanged;
            MenuItemMenuLangRus.CheckedChanged += LangStripMenuItem_CheckedChanged;
            ChangeLanguageOfControlsTexts(Controls);
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

            InitializeMenuViewShowColumns();
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

            DataGridView.Columns[t.Name].Visible = t.Checked;
        }

        public void AddButtonToLayout(Button btn)
        {
            btn.Size = new System.Drawing.Size(120, 35);
            ButtonsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ButtonsLayoutPanel.Controls.Add(btn);
        }

        private void ChangeLanguageOfControlsTexts(Control.ControlCollection controls)
        {
            foreach (var cont in controls)
                ChangeLanguageOfObjectText(cont);
        }

        private string GetValueOfSetting(string name)
        {
            return typeof(Labels).GetProperty(name)?.GetValue(null).ToString();
        }

        private void ChangeLanguageOfObjectText(object cont)
        {
            switch (cont)
            {
                case GroupBox grpb:
                    grpb.Text = GetValueOfSetting(grpb.Name);
                    ChangeLanguageOfControlsTexts(grpb.Controls);
                    break;

                case TabControl tbcont:
                    foreach (TabPage page in tbcont.TabPages)
                    {
                        page.Text = GetValueOfSetting(page.Name);
                        ChangeLanguageOfControlsTexts(page.Controls);
                    }
                    break;

                case DataGridView dgv:
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        var headerTmp = GetValueOfSetting(col.Name);
                        if (!string.IsNullOrEmpty(headerTmp))
                            col.HeaderText = GetValueOfSetting(col.Name);
                    }
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                        ChangeLanguageOfObjectText(item);
                    break;

                case ToolStripMenuItem tsi:
                    tsi.Text = GetValueOfSetting(tsi.Name);
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        ChangeLanguageOfObjectText(innerTsi);
                    break;

                default:
                    var getNameMethod = cont.GetType().GetProperty("Name").GetGetMethod();
                    var setTextMethod = cont.GetType().GetProperty("Text").GetSetMethod();

                    var propertyName = getNameMethod.Invoke(cont, null).ToString();
                    var NameFromLabels = GetValueOfSetting(propertyName);

                    if (!string.IsNullOrEmpty(NameFromLabels))
                        setTextMethod.Invoke(cont, new object[] { NameFromLabels });
                    else
                        setTextMethod.Invoke(cont, new object[] { propertyName });
                    break;

                case null:
                    throw new ArgumentNullException("Have trying to set language for null control");
            }
        }

    } // public partial class DataTableForm<Model> : Form
} // Regata.UITemplates
