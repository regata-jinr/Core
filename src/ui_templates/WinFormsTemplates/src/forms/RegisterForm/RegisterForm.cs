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

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class
    {
        
        public RegisterForm(uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f)
        {
            InitializeComponent();
            InitializeMainTable();
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
           
            LangItem.CheckedChanged        += LabelsLanguageItemChanged;
            GlobalSettings.LanguageChanged += LanguageChanged;
            TabsPane.DataSourceChanged     += LanguageChanged; // it is possible to create form before filling the dgvs. It will lead to unlabeled dgvs columns

            LanguageChanged(); // init current language and set related checked items
        }

        private void LabelsLanguageItemChanged()
        {
            GlobalSettings.CurrentLanguage = LangItem.CheckedItem;
        }

        private void LanguageChanged()
        {
            Labels.SetControlsLabels(Controls);
            LangItem.CheckedChanged -= LabelsLanguageItemChanged;
            LangItem.CheckItem(GlobalSettings.CurrentLanguage);
            LangItem.CheckedChanged += LabelsLanguageItemChanged;
        }

    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

