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

using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class
    {
        
        public RegisterForm(uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f, Settings.Language lang = Settings.Language.English)
        {
            InitializeComponent();
            InitializeMainTable();
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
           
            LangItem.CheckedChanged += Labels_LanguageItemChanged;
            Labels.LanguageChanged += Labels_LanguageChanged;

            Labels.CurrentLanguage = lang;

        }

        private void Labels_LanguageItemChanged()
        {
            Labels.CurrentLanguage = LangItem.CheckedItem;
        }

        private void Labels_LanguageChanged()
        {
            Labels.SetControlsLabels(this.Controls);
            LangItem.CheckedChanged -= Labels_LanguageItemChanged;
            LangItem.CheckItem(Labels.CurrentLanguage);
            LangItem.CheckedChanged += Labels_LanguageItemChanged;
        }

    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

