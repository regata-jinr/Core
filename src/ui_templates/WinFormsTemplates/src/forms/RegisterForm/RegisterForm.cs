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
using System.Windows.Forms;
using System;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class
    {

        public RegisterForm(uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f)
        {
            InitializeComponent();
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
           
            //LangItem.CheckedChanged              += LabelsLanguageItemChanged;
            //GlobalSettings.LanguageChanged       += LanguageChanged;
            //TabsPane.DataSourceChanged           += LanguageChanged; // it is possible to create form before filling the dgvs. It will lead to unlabeled dgvs columns
            //ControlAdded                         += (s, e) => LanguageChanged();
            //MenuStrip.ItemAdded                  += (s, e) => LanguageChanged();
            //StatusStrip.ItemAdded                += (s, e) => LanguageChanged();
            //FunctionalLayoutPanel.ControlAdded   += (s, e) => LanguageChanged();
            //tableLayoutPanelRegForm.ControlAdded += (s, e) => LanguageChanged();
            //LanguageChanged(); // init current language and set related checked items

            // TODO: add warning message as dialog result
            // https://github.com/regata-jinr/Core/issues/11
            //buttonRemoveSample.Click += (s, e) => {  }

        }

        //private void LabelsLanguageItemChanged()
        //{
        //    GlobalSettings.CurrentLanguage = LangItem.CheckedItem;
        //}

        //private void LanguageChanged()
        //{
        //    Labels.SetControlsLabels(Controls);
        //    LangItem.CheckedChanged -= LabelsLanguageItemChanged;
        //    LangItem.CheckItem(GlobalSettings.CurrentLanguage);
        //    LangItem.CheckedChanged += LabelsLanguageItemChanged;
        //}

    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

