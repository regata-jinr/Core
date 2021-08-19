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
using Regata.Core;
using Regata.Core.Hardware.Xemo;

namespace Regata.Core.Hardware
{
    /// <summary>
    /// Sample changer consist from 3 systems each can moving along one of an axis: X - hotizontal , Y - vertical and C - circle
    /// </summary>
    enum Axes : short { Y, X, C} 

    public partial class SampleChanger : IDisposable
    {
        /// <summary>
        /// Serial number of Xemo controller. Labeled on on of side panel of Xemo Controller SU 360
        /// </summary>
        private string _sn;

        /// <summary>
        /// Port number referred to Xemo device. By default is 0 that means auto matching.
        /// </summary>
        private short _comPort;

        /// <summary>
        /// BaudRate for chosen com port. Not used in case interface initialization via USB.
        /// </summary>
        private int _baudRate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sn">Serial number of Xemo controller. Labeled on on of side panel of Xemo Controller SU 360</param>
        /// <param name="comPort">Port number referred to Xemo device. By default is 0 that means auto matching.</param>
        /// <param name="baudRate">BaudRate for chosen com port. Not used in case interface initialization via USB.</param>
        /// <param name="sets">Initial settings of current settings</param><seealso cref="SampleChangerSettings"/>
        public SampleChanger(string sn, short comPort = 0, int baudRate = 19200, SampleChangerSettings sets = null)
        {
            if (string.IsNullOrEmpty(sn))
                throw new ArgumentNullException("Serial number should not be null or empty");

            if (sets == null)
                Settings = new SampleChangerSettings();
                

            _sn = sn;
            _comPort = comPort;
            _baudRate = baudRate;

            Connect();

            InitializeAxes();



        }

        private void Connect()
        {
            XemoDLL.ML_DeIniCom();
            XemoDLL.ML_IniUsb(_comPort, _sn);
            // XemoDLL.ML_ComSelect(_comPort);
            Reset();
        }

        private void InitializeAxes()
        {
            InitAxisParam(Axes.Y);
            InitAxisParam(Axes.X);
            InitAxisParam(Axes.C);
        }


        public void Disconnect()
        {
            HaltSystem();
            XemoDLL.ML_DeIniCom();
        }


        private void InitAxisParam(Axes ax)
        { 
       
            var axisNum = (short)ax;
            var XemoType = XemoDLL.MB_Get(XemoConst.Version);

            XemoDLL.MB_ASet(axisNum, XemoConst.Current, Settings.AxesParams.MOTOR_CURRENT[axisNum]);
            XemoDLL.MB_ASet(axisNum, XemoConst.StopCurr, Settings.AxesParams.MOTOR_STOP_CURRENT[axisNum]);
            XemoDLL.MB_ASet(axisNum, XemoConst.Micro, Settings.AxesParams.MICROSTEP_RESOLUTION[axisNum]);

            XemoDLL.MB_ASet(axisNum, XemoConst.Uscale, (int)Math.Round(unchecked(Settings.AxesParams.MM_PER_REVOLUTION[axisNum] * 100f)));
            XemoDLL.MB_ASet(axisNum, XemoConst.Iscale, (int)Math.Round((float)Settings.AxesParams.INC_PER_REVOLUTION[axisNum] / Settings.AxesParams.MICROSTEP_RESOLUTION[axisNum]));


            XemoDLL.MB_ASet(axisNum, XemoConst.Speed, Settings.AxesParams.MAX_VELOCITY[axisNum] * 100);
            XemoDLL.MB_ASet(axisNum, XemoConst.Accel, (int)Math.Round(unchecked(Settings.AxesParams.ACCELERATION_FACTOR[axisNum] * checked(Settings.AxesParams.MAX_VELOCITY[axisNum] * 100))));
            XemoDLL.MB_ASet(axisNum, XemoConst.Decel, (int)Math.Round(unchecked(Settings.AxesParams.DECELERATION_FACTOR[axisNum] * checked(Settings.AxesParams.MAX_VELOCITY[axisNum] * 100))));

            XemoDLL.MB_ASet(axisNum, XemoConst.Vmin, (int)Math.Round(unchecked(Settings.AxesParams.START_STOP_FREQUENCY[axisNum] * 100f) / 10.0));

            XemoDLL.MB_ASet(axisNum, XemoConst.H1Speed, Settings.AxesParams.REF_VELOCITY_H1[axisNum] * 100);
            XemoDLL.MB_ASet(axisNum, XemoConst.H2Speed, Settings.AxesParams.REF_VELOCITY_H2[axisNum] * 100);
            XemoDLL.MB_ASet(axisNum, XemoConst.H3Speed, Settings.AxesParams.REF_VELOCITY_H3[axisNum] * 100);

            XemoDLL.MB_ASet(axisNum, XemoConst.HOffset, (int)Math.Round(unchecked(Settings.AxesParams.ZERO_REF_OFFSET[axisNum] * 100)));
            XemoDLL.MB_ASet(axisNum, XemoConst.SlLimit, 0);
            XemoDLL.MB_ASet(axisNum, XemoConst.SrLimit, Settings.AxesParams.TRAVEL_AXIS[axisNum] * 100);

            XemoDLL.MB_IoSet(axisNum, 0, 3, 4000, Settings.AxesParams.POLARITY_SWITCHES[axisNum]);

            XemoDLL.MB_ASet(axisNum, XemoConst.BLash, (int)Math.Round(unchecked(Settings.AxesParams.BLASH[axisNum] * 100)));

            if (Settings.AxesParams.XTYPE[axisNum] != 0)
                XemoDLL.MB_ASet(axisNum, XemoConst.XType, Settings.AxesParams.XTYPE[axisNum]);

            if (Settings.AxesParams.GANTRY_ACHSE[axisNum] != 0)
                XemoDLL.MB_ASet(axisNum, XemoConst.Gantry, Settings.AxesParams.GANTRY_ACHSE[axisNum]);

            if (Settings.AxesParams.JERKMS[axisNum] != 0)
                XemoDLL.MB_ASet(axisNum, XemoConst.Jerkms, Settings.AxesParams.JERKMS[axisNum]);

            if (Settings.AxesParams.INC_MONITORING_ENCODER[axisNum] != 0)
            {
                XemoDLL.MB_ASet(axisNum, XemoConst.StpEncoder, Settings.AxesParams.INC_MONITORING_ENCODER[axisNum]);
                XemoDLL.MB_ASet(axisNum, XemoConst.FErrWin, Settings.AxesParams.POSITION_ERROR[axisNum]);
            }

            if (XemoType != 448)
                XemoDLL.MB_ASet(axisNum, XemoConst.LDecel, (int)Math.Round(unchecked(checked(Settings.AxesParams.MAX_VELOCITY[axisNum] * 100)) * Settings.AxesParams.EMERGCY_DECEL_FACTOR[axisNum]));

            if (Settings.AxesParams.BRAKE[axisNum] >= 0)
                XemoDLL.MB_ASet(axisNum, XemoConst.BrakeOutp, 10 + 4096 * Settings.AxesParams.BRAKE[axisNum] + 256);
        }

        private bool _isDisposed;

        private void Dispose(bool isDisposing)
        {
            //Report.Notify(new DetectorMessage(Codes.INFO_DET_CLN));

            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    NLog.LogManager.Flush();

                }
                Stop();
                Disconnect();
            }
            _isDisposed = true;
        }

        ~SampleChanger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disconnects from detector. Changes status to off. Resets ErrorMessage. Clears the detector.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } // public partial class SampleChanger  : IDisposable
}     // namespace Measurements.Core.Hardware
