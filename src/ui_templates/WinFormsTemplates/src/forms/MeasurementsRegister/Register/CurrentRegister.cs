/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.EntityFrameworkCore;
using RCM = Regata.Core.Messages;
using Regata.Core.DataBase.Models;
using System;
using System.Linq;

namespace Regata.Core.UI.WinForms.Forms.Measurements
{
    public partial class MeasurementsRegisterForm
    {
        private void InitCurrentRegister(int id)
        {
            try
            {
                mainForm.MainRDGV.CurrentDbSet.Where(m => m.RegId == id).Load();
                mainForm.MainRDGV.DataSource = mainForm.MainRDGV.CurrentDbSet.Local.ToBindingList();

                //mainForm.MainRDGV.HideColumns();

                //mainForm.MainRDGV.SetUpWritableColumns();

                Labels.SetControlsLabels(mainForm);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INIT_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MeasurementsRegisterForm
}     // namespace Regata.Core.UI.WinForms.Forms.Measurements
