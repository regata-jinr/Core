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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Regata.UITemplates
{
    // TODO: add sort by any column
    // TODO: for global settings settings.json must be separated on the forms for views
    /* 
     * settings.json:
     *  "language": "english",
     *  {
     *  "form1": {"hidedColumns": [...]},
     *  "form2": {"hidedColumns": [...]},
     *  }
    */
    // FIXME: dict solution support only hided columns in case of extensions I have to reimplements this part. Use global settings class and local settings class for each form.
    // DONE:  the same functional like for translation but for user access - this is the bad idea, because user can do it easily
    //        in the form constructor.
    // DONE:   better is provide list of roles for user
    // TODO:  add tests via test db table
    // TODO:  add autoupdate based on github releases
    // TODO:  add cicd

    public abstract partial class DataTableForm<Model> : Form
    {
        public readonly BindingList<Model> Data;
        public readonly string SettingsPath;
        private readonly string _derivedFormName;
        protected readonly Labels _labels;

        public DataTableForm(string DerivedFormName)
        {
            if (string.IsNullOrEmpty(Settings.AssemblyName)) throw new ArgumentNullException("You must specify name of calling assembly. Just use 'System.Reflection.Assembly.GetExecutingAssembly().GetName().Name' as argument.");
            if (string.IsNullOrEmpty(DerivedFormName)) throw new ArgumentNullException("You must specify name of derived form for correct working of settings.");
            if (string.IsNullOrEmpty(Settings.ConnectionString)) throw new ArgumentNullException("You must assign Settings.ConnectionString for correct working of settings.");

            _derivedFormName = DerivedFormName;
            _labels = new Labels(DerivedFormName);

            UserRoles = GetUserRoles();


            InitializeComponent();

            Settings.LanguageChanged += () => ChangeLanguageOfControlsTexts(Controls);
            SettingsPath = Settings.FilePath;

            if (Settings.CurrentLanguage == Languages.English)
                MenuItemMenuLangEng.Checked = true;
            else
                MenuItemMenuLangRus.Checked = true;

            Data = new BindingList<Model>();
            DataGridView.DataSource = Data;

            MenuItemMenuLangEng.CheckedChanged += LangStripMenuItem_CheckedChanged;
            MenuItemMenuLangRus.CheckedChanged += LangStripMenuItem_CheckedChanged;

            // is this redundant? User adds control the most frequantly during form init
            ButtonsLayoutPanel.ControlAdded += DataTableForm_ControlAdded;
            ControlAdded += DataTableForm_ControlAdded;
            //ChangeLanguageOfControlsTexts(Controls);
            InitializeMenuViewShowColumns();
        }

        private void DataTableForm_ControlAdded(object sender, ControlEventArgs e)
        {
            ChangeLanguageOfControlsTexts(Controls);
        }

        private void LangStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            // circle switching

            var langStrip = sender as ToolStripMenuItem;

            if (langStrip.Checked && langStrip.Name == MenuItemMenuLangEng.Name)
            {
                Settings.CurrentLanguage = Languages.English;
                MenuItemMenuLangRus.Checked = false;
            }

            if (langStrip.Checked && langStrip.Name == MenuItemMenuLangRus.Name)
            {
                Settings.CurrentLanguage = Languages.Russian;
                MenuItemMenuLangEng.Checked = false;
            }

            if (!(MenuItemMenuLangEng.Checked || MenuItemMenuLangRus.Checked))
            {
                if (langStrip.Name == MenuItemMenuLangEng.Name)
                {
                    Settings.CurrentLanguage = Languages.Russian;
                    MenuItemMenuLangRus.Checked = true;
                }
                if (langStrip.Name == MenuItemMenuLangRus.Name)
                {
                    Settings.CurrentLanguage = Languages.English;
                    MenuItemMenuLangEng.Checked = true;
                }
            }

            InitializeMenuViewShowColumns();
        }

        private void InitializeMenuViewShowColumns()
        {
            MenuItemViewShowColumns.DropDownItems.Clear();
            foreach (DataGridViewColumn col in DataGridView.Columns)
            {
                if (_hidedColumnsIndexes.Contains(col.Index)) continue;

                var t = new ToolStripMenuItem { Name = col.Name, Text = col.HeaderText, CheckOnClick = true, Checked = true};

                if (Settings.FormNonDisplayedColumns.ContainsKey(_derivedFormName) && Settings.FormNonDisplayedColumns[_derivedFormName].Contains(t.Name))
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
                Settings.HideColumn(_derivedFormName, t.Name);
            else
                Settings.ShowColumn(_derivedFormName, t.Name);

            DataGridView.Columns[t.Name].Visible = t.Checked;
        }

        public void AddButtonToLayout(Button btn)
        {
            btn.Size = new System.Drawing.Size(120, 50);
            ButtonsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ButtonsLayoutPanel.Controls.Add(btn);
        }

        protected readonly string[] UserRoles;

        private string[] GetUserRoles()
        {
            var ListOfRoles = new List<string>();
            using (SqlConnection conn = new SqlConnection(Settings.ConnectionString))
            {
                var cs = new SqlConnectionStringBuilder(Settings.ConnectionString);
                using (var cmd = new SqlCommand($"declare @results varchar(500) select @results = coalesce(@results + ',', '') +  a.Role from ( select distinct Role from UserRoles where UserName = '{cs.UserID}' and Role is not NULL) as a; select @results as roles;", conn))
                {
                    conn.Open();
                    object o = cmd.ExecuteScalar();
                    if (o == null || DBNull.Value == o) return ListOfRoles.ToArray();

                    ListOfRoles.AddRange(o.ToString().Split(','));
                }
            }
            return ListOfRoles.ToArray();
        }
     
        public void ChangeLanguageOfControlsTexts(Control.ControlCollection controls)
        {
            foreach (var cont in controls)
                ApplyActionToComponent(cont, SetTextLabel);
        }

        private void ApplyActionToComponent(object component, Action<object> act)
        {
            switch (component)
            {
                case DataGridView dgv:
                    act(dgv);
                    foreach (DataGridViewColumn col in dgv.Columns)
                        act(col);
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                    {
                        item.Text = _labels.GetLabel(item.Name);
                        ApplyActionToComponent(item, act);
                      
                    }
                    break;

                case ToolStripMenuItem tsi:
                    tsi.Text = _labels.GetLabel(tsi.Name);
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        ApplyActionToComponent(innerTsi,act);
                    break;

                case Control nestedControl:
                    if (nestedControl.Controls.Count > 0)
                    {
                        foreach (Control nc in nestedControl.Controls)
                            ApplyActionToComponent(nc,act);
                    }
                    else
                        act(nestedControl);
                    break;
                
                case null:
                    throw new ArgumentNullException("Have trying to set language for null control");

                default:
                    act(component);
                    break;
            }
        }

        private void SetTextLabel(object obj)
        {
            switch (obj)
            {
                case DataGridViewColumn dgvc:
                    var headerTmp = _labels.GetLabel(dgvc.Name);
                    if (!string.IsNullOrEmpty(headerTmp))
                        dgvc.HeaderText = _labels.GetLabel(dgvc.Name);
                    break;
                default:

                    var getNameMethod = obj.GetType().GetProperty("Name").GetGetMethod();
                    var setTextMethod = obj.GetType().GetProperty("Text").GetSetMethod();

                    if (getNameMethod == null || setTextMethod == null) return;

                    var propertyName = getNameMethod.Invoke(obj, null).ToString();
                    var NameFromLabels = _labels.GetLabel(propertyName);

                    if (!string.IsNullOrEmpty(NameFromLabels))
                        setTextMethod.Invoke(obj, new object[] { NameFromLabels });
                    else
                        setTextMethod.Invoke(obj, new object[] { propertyName });

                    Text = _labels.GetLabel("FormText");
                    FooterStatusLabel.Text = "";
                    break;
            }
        }

        private int[] _hidedColumnsIndexes = { -1 };
        public void HideColumnsWithIndexes(params int[] indexes)
        {
            _hidedColumnsIndexes = indexes;
            foreach (var i in indexes)
            {
                DataGridView.Columns[i].Visible = false;
                MenuItemViewShowColumns.DropDownItems[i].Visible = false;
            }
        }

    } // public abstract partial class DataTableForm<Model> : Form
} // Regata.UITemplates
