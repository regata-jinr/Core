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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabsNum"></param>
        /// <param name="dgvsNum"></param>
        /// <param name="BigDgvSizeCoeff"></param>
        public RegisterForm(Language lang, uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f)
        {
            InitializeComponent();
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
            //LangItem.CheckedChanged += ChangeLanguage;
            //LangItem.CheckItem(lang);
            //LanguageChanged += LanguageChanged;
            //TabsPane.DataSourceChanged += ChangeLanguage; // it is possible to create form before filling the dgvs. It will lead to unlabeled dgvs columns
            //ControlAdded += (s, e) => ChangeLanguage();
            //MenuStrip.ItemAdded += (s, e) => ChangeLanguage();
            //StatusStrip.ItemAdded += (s, e) => ChangeLanguage();
            //FunctionalLayoutPanel.ControlAdded += (s, e) => ChangeLanguage();
            //tableLayoutPanelRegForm.ControlAdded += (s, e) => ChangeLanguage();
            //ChangeLanguage(); // init current language and set related checked items

            // TODO: add warning message as dialog result
            // https://github.com/regata-jinr/Core/issues/11
            //buttonRemoveSample.Click += (s, e) => {  }

        }


        //public void ChangeLanguage()
        //{
        //    Labels.SetControlsLabels(Controls);
        //    //LangItem.CheckedChanged -= ChangeLanguage;
        //    //LangItem.CheckItem(GlobalSettings.CurrentLanguage);
        //    //LangItem.CheckedChanged += ChangeLanguage;
        //}

    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

