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
using System;

namespace Regata.Core.Hardware
{
    public class SampleChangerSettings
    {
        public SampleChanger.AxesParams AxesParams { get; set; } = new SampleChanger.AxesParams();

        #region Velocity
        public int XVelocity
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.X, XemoConst.Speed);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.Speed, Math.Abs(value));
            }

        }

        public int YVelocity
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.Y, XemoConst.Speed);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.Speed, Math.Abs(value));
            }

        }
        public int CVelocity
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.C, XemoConst.Speed);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.Speed, Math.Abs(value));
            }

        }
        #endregion 

        #region X software limits


        public int SoftwareXRightLimit 
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.X, XemoConst.SrLimit);
            }
            set 
            {
                XemoDLL.MB_ASet((short) Axes.X, XemoConst.SrLimit, value);
            } 
        }

        
        public int SoftwareXLeftLimit
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.X, XemoConst.SlLimit);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.SlLimit, value);
            }
        }

        public int LXDecel             
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.X, XemoConst.LDecel);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.X, XemoConst.LDecel, value);
            }
        }

        #endregion

        #region Y software limits

        public int SoftwareYUpLimit
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.Y, XemoConst.SrLimit);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.SrLimit, value);
            }
        }

        public int SoftwareYDownLimit
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.Y, XemoConst.SlLimit);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.SlLimit, value);
            }
        }


        public int LYDecel
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.Y, XemoConst.LDecel);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.Y, XemoConst.LDecel, value);
            }
        }

        #endregion


        #region C software limits


        public int SoftwareCRightLimit
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.C, XemoConst.SrLimit);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.SrLimit, value);
            }
        }


        public int SoftwareCLeftLimit
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.C, XemoConst.SlLimit);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.SlLimit, value);
            }
        }

        public int LCDecel
        {
            get
            {
                return XemoDLL.MB_AGet((short)Axes.C, XemoConst.LDecel);
            }
            set
            {
                XemoDLL.MB_ASet((short)Axes.C, XemoConst.LDecel, value);
            }
        }

        #endregion

    } // public class SampleChangerSettings
}     // namespace Regata.Core.Hardware
