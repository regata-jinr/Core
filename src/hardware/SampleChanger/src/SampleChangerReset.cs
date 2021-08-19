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

using System;
using Regata.Core.Hardware.Xemo;

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {

        public void ResetAllSoftwareLimits()
        {
            Settings.SoftwareXLeftLimit = 0;
            Settings.SoftwareXRightLimit = 0;
            Settings.LXDecel = 0;

            Settings.SoftwareYLeftLimit = 0;
            Settings.SoftwareYRightLimit = 0;
            Settings.LYDecel = 0;
            
            Settings.SoftwareCLeftLimit = 0;
            Settings.SoftwareCRightLimit = 0;
            Settings.LCDecel = 0;
        }

        public void ResetOnlySoftwareLimits()
        {
            Settings.SoftwareXLeftLimit = 0;
            Settings.SoftwareXRightLimit = 0;

            Settings.SoftwareYLeftLimit = 0;
            Settings.SoftwareYRightLimit = 0;

            Settings.SoftwareCLeftLimit = 0;
            Settings.SoftwareCRightLimit = 0;
        }

        public void ResetLDecels()
        {
            Settings.LXDecel = 0;

            Settings.LYDecel = 0;

            Settings.LCDecel = 0;
        }

        public void ResetErrors()
        {
            XemoDLL.MB_ResErr();
        }

        public void ResetSystem()
        {
            XemoDLL.MB_SysControl(XemoConst.Reset);
        }

        public void HaltSystem()
        {
            XemoDLL.MB_SysControl(XemoConst.Halt);
        }

        public void BreakSystemProgram()
        {
            XemoDLL.MB_SysControl(XemoConst.Break);
        }

        public void Reset()
        {
            ResetSystem();
            ResetErrors();
        }


    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

