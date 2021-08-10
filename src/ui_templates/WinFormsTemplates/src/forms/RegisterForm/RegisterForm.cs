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

using Regata.Core.DataBase.Models;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class, IId
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabsNum"></param>
        /// <param name="dgvsNum"></param>
        /// <param name="BigDgvSizeCoeff"></param>
        public RegisterForm(uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f)
        {
            InitializeComponent();
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
        }

    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

