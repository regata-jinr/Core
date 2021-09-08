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

using Regata.Core.Hardware.Xemo;

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {

        public void ResetAllSoftwareLimits()
        {
            ResetOnlySoftwareLimits();
            ResetLDecels();
        }

        public void ResetOnlySoftwareLimits()
        {
            Settings.SoftwareXLeftLimit = 0;
            Settings.SoftwareXRightLimit = 0;

            Settings.SoftwareYDownLimit = 0;
            Settings.SoftwareYUpLimit = 0;

            Settings.SoftwareCLeftLimit = 0;
            Settings.SoftwareCRightLimit = 0;
        }

        public void ResetLDecels()
        {
            Settings.LXDecel = 0;

            Settings.LYDecel = 0;

            Settings.LCDecel = 0;
        }

        /// <summary>
        /// When an error occurs, the related error number is stored in an error buffer. 
        /// Further, the bit ERR is set in the system status _state.The numbers of sequential errors
        /// are stored in the error buffer as well.With ResErr, the oldest of the
        /// errors is always erased from the error buffer first, then the next error, and so
        /// on.A critical error erases all other errors in the fifo and puts itself in first place. 
        /// If another critical error occurs after that, it will overwrite the already present
        /// critical error. No commands can be executed. Not until after all errors have
        /// been erased with the instruction ResErr is it possible to execute commands again. 
        /// </summary>
        public void ResetErrors()
        {
            XemoDLL.MB_ResErr();
        }

        /// <summary>
        /// Sets the controller in the status which it had after being switched on. All axes 
        /// are stopped immediately, all outputs are reset and all parameters are returned
        /// to their initial values.
        /// </summary>
        public void ResetSystem()
        {
            //XemoDLL.MB_SetFifo(XemoConst.FfClear);
            XemoDLL.MB_SysControl(XemoConst.Reset);
        }

        /// <summary>
        /// A running MotionBasic program is halted. The online fifo is erased. The axes 
        /// and outputs remain unchanged.
        /// </summary>
        public void HaltSystem()
        {
            XemoDLL.MB_SysControl(XemoConst.Halt);
        }

        /// <summary>
        /// A running MotionBasic is aborted. All axes are stopped and all outputs reset. 
        /// The online fifo is erased.All position counters and settings remain unchanged.
        /// </summary>
        public void BreakSystemProgram()
        {
            IsStopped = true;
            XemoDLL.MB_SysControl(XemoConst.Break);
        }

        /// <summary>
        /// Like _Reset but with a restart of the possibly loaded MotionBasic program.
        /// NOTE: When switched on and at every program start in the main routine, the controller is automatically reset. 
        /// </summary>
        public void RestartSystem()
        {
            XemoDLL.MB_SysControl(XemoConst.Restart);
        }

        public void Reset()
        {
            ResetSystem();
            ResetErrors();
        }


    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware

