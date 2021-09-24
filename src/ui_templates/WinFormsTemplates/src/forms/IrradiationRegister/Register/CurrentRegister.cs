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
using Regata.Core;
using RCM = Regata.Core.Messages;
using System;
using System.Linq;

namespace Regata.Core.UI.WinForms.Forms.Irradiations
{
    public partial class IrradiationRegister
    {

        private void InitCurrentRegister()
        {
            try
            {
                switch (_irrType)
                {
                    case DataBase.Models.IrradiationType.lli:
                mainForm.MainRDGV.CurrentDbSet.Where(ir => ir.DateTimeStart.Value.Date == _irrDateTime.Date && ir.LoadNumber == _loadNumber).OrderBy(ir => ir.Container).ThenBy(ir => ir.Position).Load();
                        break;
                    case DataBase.Models.IrradiationType.sli:
                        mainForm.MainRDGV.CurrentDbSet.Where(ir => ir.DateTimeStart.Value.Date == _irrDateTime.Date && ir.LoadNumber == _loadNumber).OrderBy(ir => ir.DateTimeStart).Load();
                        break;

                };
                mainForm.MainRDGV.DataSource = mainForm.MainRDGV.CurrentDbSet.Local.ToBindingList();

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INIT_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

     

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
