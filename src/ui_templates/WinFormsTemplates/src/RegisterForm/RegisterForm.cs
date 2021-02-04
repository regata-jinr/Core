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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using NewForms.Models;

namespace Regata.Core.UI.WinForms
{
    public partial class RegisterForm<MainTableModel> : Form
        where MainTableModel : class
    {
        
        public RegisterForm(string connectionString, uint tabsNum=2, uint dgvsNum=2, float BigDgvSizeCoeff = 0.66f)
        {
            InitializeComponent();
            InitializeMainTable(connectionString);
            InitializeTabControl(tabsNum, dgvsNum, BigDgvSizeCoeff);
        }


      



    } //public partial class RegisterForm : Form
}     // namespace Regata.Core.UI.WinForms

