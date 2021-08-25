/***************************************************************************
 *                                                                         *
 * Based on XemoKonst.cs by SYSTEC                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

namespace Regata.Core.Hardware.Xemo
{
    /// <summary>
    /// Xemo constants description and codes
    /// </summary>
    /// <remarks></remarks>
    internal static class XemoConst
    {
        public const long BAUDRATE = 19200;

        // Bits im Systemstatus 
        // ------------------------------- 
        public const short STATE_F0 = 0x1;
        public const short STATE_F1 = 0x2;
        public const short STATE_F2 = 0x4;
        public const short STATE_ERR = 0x8;
        public const short STATE_MS = 0x10;
        public const short STATE_OR0 = 0x20;
        public const short STATE_RUN = 0x40;

        public const short PSTATE_PSTATE = 0x7;
        public const short PSTATE_ERROR = 0x8;
        public const short PSTATE_ONERROR = 0x10;
        public const short PSTATE_DEBUG = 0x40;

        public const short STATE_FIFO = 0x3;
        public const short STATE_FIFO_EMPTY = 0;
        public const short STATE_FIFO_LOW = 1;
        public const short STATE_FIFO_HIGH = 2;
        public const short STATE_FIFO_FULL = 3;

        /// <summary>
        /// Sets the controller in the status which it had after being switched on. All axes 
        /// are stopped immediately, all outputs are reset and all parameters are returned
        /// to their initial values.
        /// </summary>
        public const short Reset = 1;
        /// <summary>
        /// A running MotionBasic is aborted. All axes are stopped and all outputs reset. 
        /// The online fifo is erased.All position counters and settings remain unchanged.
        /// </summary>
        public const short Break = 2;
        /// <summary>
        /// A running MotionBasic program is halted. The online fifo is erased. The axes 
        /// and outputs remain unchanged.
        /// </summary>
        public const short Halt = 3;
        /// <summary>
        /// Like _Reset but with a restart of the possibly loaded MotionBasic program.
        /// NOTE: When switched on and at every program start in the main routine, the controller is automatically reset. 
        /// </summary>
        public const short Restart = 4;

        public const short PrgQuit = 100;         // MotionBasic Programm benden 
        public const short DebugShell = 101;      // Zur Debug-Shell wechseln 
        public const short OnlineShell = 102;     // Zur Online-Shell wechseln 

        // SetFifo FifoStatus 
        // ------------------------------- 
        public const short FfDisable = 1;
        public const short FfEnable = 2;
        public const short FfClear = 3;

        // Tastatur-Codes 
        public const short Key_F1 = 0x13b;
        public const short Key_F2 = 0x13c;
        public const short Key_F3 = 0x13d;
        public const short Key_F4 = 0x13e;
        public const short Key_Bs = 0x8;
        public const short Key_Enter = 0xd;
        public const short Key_Up = 0x148;
        public const short Key_Down = 0x150;
        public const short Key_Right = 0x14d;
        public const short Key_Left = 0x14b;

        // KeyLed-Codes 
        // ------------------------------- 
        public const short LedOff = 0;
        public const short LedOn = 1;
        public const short LedBlink = 10;
        public const short LedFlash = 11;

        // Die Achsbezeichner 
        // ------------------------------- 
        public const short X = 0;
        public const short Y = 1;
        public const short Z = 2;
        public const short A = 3;

        /// <summary>
        /// All axes
        /// </summary>
        public const short XALL = 255;
        public const short ALL = 254;  // Alle Achs- und Bahnparameter 

        // Seriell IO 
        public const short COM1 = 1;
        public const short BD9600 = 5;
        public const short BD19200 = 6;
        public const short BD28800 = 7;
        public const short BD38400 = 8;
        public const short BD57600 = 9;

        // USB 
        public const short USB_OFF = 0;
        public const short USB_AUTO = 1;
        public const short USB_ONLY = 2;

        // Diverse 
        public const short Off = 0;
        ///<summary>
        ///System status 
        ///</summary>
        public const short BOT_SPARM_NO = 1000;

        ///<summary>
        ///System status 
        ///</summary>
        public const short State = 1000;

        ///<summary>
        ///Software - number of the operating system 
        ///</summary>
        public const short Version = 1004;

        ///<summary>
        ///Release number of the operating system 
        ///</summary>
        public const short Release = 1005;

        ///<summary>
        ///Downwards compatibility limit 
        ///</summary>
        public const short ComRelease = 1006;

        ///<summary>
        ///Expansion level of the Xemo controller 
        ///</summary>
        public const short Extent = 1007;

        ///<summary>
        ///Present status of the system timer 
        ///</summary>
        public const short Clock = 1008;

        ///<summary>
        ///Runtime error 
        ///</summary>
        public const short ErrNo = 1010;

        ///<summary>
        ///Present error status 
        ///</summary>
        public const short ErrState = 1011;

        ///<summary>
        ///Line number in the source code at which the error occurred 
        ///</summary>
        public const short ErrLine = 1012;

        ///<summary>
        ///Number of the axis affected by the error 
        ///</summary>
        public const short ErrAxis = 1013;

        ///<summary>
        ///Number of the parameter which caused the error 
        ///</summary>
        public const short ErrParam = 1014;

        ///<summary>
        ///Additional information about the error 
        ///</summary>
        public const short SubError = 1015;

        ///<summary>
        ///Size of the online fifo 
        ///</summary>
        public const short FifoSize = 1021;

        ///<summary>
        ///Fill level of the online fifo 
        ///</summary>
        public const short FifoLevel = 1022;

        ///<summary>
        ///Free memory in the fifo 
        ///</summary>
        public const short FifoLeft = 1023;

        ///<summary>
        ///Setting of the upper fifo mark 
        ///</summary>
        public const short FifoMarker = 1024;

        ///<summary>
        ///Baud rate and mode of the 1. serial shorterface 
        ///</summary>
        public const short Mode1 = 1030;

        ///<summary>
        ///Set CAN baud rate channel 1 
        ///</summary>
        public const short CanMode = 1034;

        ///<summary>
        ///Register OTxxx control panel 
        ///</summary>
        public const short OtSelect = 1035;

        ///<summary>
        ///Can2Mode Set CAN baud rate channel 2 
        ///</summary>
        public const short Can2Mode = 1036;

        ///<summary>
        ///Register first CANopen device for I/O expansion 
        ///</summary>
        public const short Can1Device = 1038;

        ///<summary>
        ///Register second CANopen device for I/O expansion 
        ///</summary>
        public const short Can2Device = 1039;

        ///<summary>
        ///Coordinate system for trajectory control 
        ///</summary>
        public const short IpGroup = 1040;

        ///<summary>
        ///Cyclic time of the PWM 
        ///</summary>
        public const short BkPwmCycle = 1050;

        ///<summary>
        ///Input/Break ratio of the PWM 
        ///</summary>
        public const short BkPwmDuty = 1051;

        ///<summary>
        ///
        ///</summary>
        public const short AnlIn = 1060;

        ///<summary>
        ///Analog value display (option) 
        ///</summary>
        public const short AnlOut = 1061;

        ///<summary>
        ///Analog input of the override-poti 
        ///</summary>
        public const short OvrIn = 1062;

        ///<summary>
        ///Analog input of the X axis joystick 
        ///</summary>
        public const short JoyX = 1063;

        ///<summary>
        ///Analog input of the Y axis joystick 
        ///</summary>
        public const short JoyY = 1064;

        ///<summary>
        ///Present position of the handwheel 
        ///</summary>
        public const short HandWheel = 1070;

        ///<summary>
        ///Present position at the incremental encoder input 
        ///</summary>
        public const short Encoder = 1071;

        ///<summary>
        ///Index position at the incremental encoder input 
        ///</summary>
        public const short EncIdx = 1072;

        ///<summary>
        ///Impulse per revolution for index monitoring 
        ///</summary>
        public const short EncIpr = 1073;

        ///<summary>
        ///Write over the present velocity 
        ///</summary>
        public const short Ovride = 1075;

        ///<summary>
        ///Override mode 
        ///</summary>
        public const short Omode = 1076;

        ///<summary>
        ///Scaling the override-poti 
        ///</summary>
        public const short OvScale = 1077;

        ///<summary>
        ///Analog input 0 (option) 
        ///</summary>
        public const short SAnlIn0 = 1080;

        ///<summary>
        ///Analog input 1 (option) 
        ///</summary>
        public const short SAnlIn1 = 1081;

        ///<summary>
        ///Analog input 2 (option) 
        ///</summary>
        public const short SAnlIn2 = 1082;

        ///<summary>
        ///Velocity-dependent analog value issue 
        ///</summary>
        public const short VoMode = 1090;

        ///<summary>
        ///Scaling of the velocity dependence 
        ///</summary>
        public const short VoScale = 1091;

        ///<summary>
        ///Offset for analog output voltage 
        ///</summary>
        public const short VoOffs = 1092;

        ///<summary>
        ///Upper limit for analog output voltage 
        ///</summary>
        public const short VoMin = 1093;

        ///<summary>
        ///Lower limit for analog output voltage 
        ///</summary>
        public const short VoMax = 1094;

        ///<summary>
        ///Selection of an analog input for filter programming 
        ///</summary>
        public const short AnlFSel = 1100;

        ///<summary>
        ///Selection of the filter type for an analog input 
        ///</summary>
        public const short AFType = 1101;

        ///<summary>
        ///Filter sampling shorterval for analog input 
        ///</summary>
        public const short AFSTime = 1102;

        ///<summary>
        ///Spread analog input 
        ///</summary>
        public const short AFSpread = 1103;

        ///<summary>
        ///Hysteresis of the hysteresis filter 
        ///</summary>
        public const short AFHyster = 1104;

        ///<summary>
        ///Step width of the tracking filter 
        ///</summary>
        public const short AFTrack = 1105;

        ///<summary>
        ///Sampling number of the mid-value filter 
        ///</summary>
        public const short AFAverage = 1106;

        ///<summary>
        ///Selection of the command shorterface 
        ///</summary>
        public const short BOT_SUPARM_NO = 1400;

        ///<summary>
        ///Configure the USB shorterface 
        ///</summary>
        public const short TOP_SUPARM_NO = 1404;

        ///<summary>
        ///Running velocity 
        ///</summary>
        public const short BOT_XPARM_NO = 2000;

        ///<summary>
        ///Running velocity 
        ///</summary>
        public const short Speed = 2000;

        ///<summary>
        ///Acceleration 
        ///</summary>
        public const short Accel = 2001;

        ///<summary>
        ///Deceleration 
        ///</summary>
        public const short Decel = 2002;

        ///<summary>
        ///Start/Stop velocity 
        ///</summary>
        public const short Vmin = 2003;

        ///<summary>
        ///Emergency stop ramp 
        ///</summary>
        public const short LDecel = 2004;

        ///<summary>
        ///Smoothing of the ramps [ms] 
        ///</summary>
        public const short Jerkms = 2007;

        ///<summary>
        ///Maximum programmable velocity 
        ///</summary>
        public const short MaxVel = 2008;

        ///<summary>
        ///Maximum programmable acceleration 
        ///</summary>
        public const short MaxAcc = 2009;

        ///<summary>
        ///Allocation to a coordinate system 
        ///</summary>
        public const short IpLink = 2010;

        ///<summary>
        ///Allocation to an shorterpolation axis in the coordinate system 
        ///</summary>
        public const short IpAxis = 2011;

        ///<summary>
        ///CAN address a CANopen power stage 
        ///</summary>
        public const short NodeId = 2012;

        ///<summary>
        ///Type of axis (connection type of axis) 
        ///</summary>
        public const short XType = 2013;

        ///<summary>
        ///Velocity 1 reference run 
        ///</summary>
        public const short H1Speed = 2020;

        ///<summary>
        ///Velocity 2 reference run 
        ///</summary>
        public const short H2Speed = 2021;

        ///<summary>
        ///Velocity 3 reference run 
        ///</summary>
        public const short H3Speed = 2022;

        ///<summary>
        ///Distance between the zero poshort of the machine and the reference poshort 
        ///</summary>
        public const short HOffset = 2023;

        ///<summary>
        ///Inspection of the reference position 
        ///</summary>
        public const short HVerify = 2024;

        ///<summary>
        ///Kind of reference run algorithm 
        ///</summary>
        public const short HMethod = 2025;

        ///<summary>
        ///Activate the regulator 
        ///</summary>
        public const short OpMode = 2030;

        ///<summary>
        ///Phase current of a stepping motor 
        ///</summary>
        public const short Current = 2031;

        ///<summary>
        ///Permissible contouring error 
        ///</summary>
        public const short FErrWin = 2032;

        ///<summary>
        ///target window 
        ///</summary>
        public const short TPosWin = 2033;

        ///<summary>
        ///Permissible positioning time 
        ///</summary>
        public const short TPosTime = 2034;

        ///<summary>
        ///Determines the output for the brake approach 
        ///</summary>
        public const short BrakeOutp = 2035;

        ///<summary>
        ///Time shorterval between setting the motor current and releasing the brake 
        ///</summary>
        public const short BrkDelay1 = 2036;

        ///<summary>
        ///Time shorterval during which the brake output is fully supplied with current 
        ///</summary>
        public const short BrkDelay2 = 2037;

        ///<summary>
        ///Scaling user units 
        ///</summary>
        public const short Uscale = 2040;

        ///<summary>
        ///Scaling increments 
        ///</summary>
        public const short Iscale = 2041;

        ///<summary>
        ///Shift of the zero poshort 
        ///</summary>
        public const short Zero = 2042;

        ///<summary>
        ///Shift of the zero poshort 
        ///</summary>
        public const short Origin = 2043;

        ///<summary>
        ///Shift of the zero poshort 
        ///</summary>
        public const short Refp = 2044;

        ///<summary>
        ///Software limit switch negative (left) 
        ///</summary>
        public const short SlLimit = 2045;

        ///<summary>
        ///Software limit switch positive (right) 
        ///</summary>
        public const short SrLimit = 2046;

        ///<summary>
        ///Position counter mode (ring mode) 
        ///</summary>
        public const short Pmode = 2047;

        ///<summary>
        ///Reversal slack compensation 
        ///</summary>
        public const short BLash = 2048;

        ///<summary>
        ///Define the gantry system 
        ///</summary>
        public const short Gantry = 2049;

        ///<summary>
        ///Microstep resolution 
        ///</summary>
        public const short Micro = 2050;

        ///<summary>
        ///
        ///</summary>
        public const short MPhi = 2051;

        ///<summary>
        ///Current reduction to the axis during standstill 
        ///</summary>
        public const short StopCurr = 2052;

        ///<summary>
        ///Settings for step monitoring 
        ///</summary>
        public const short StpEncoder = 2056;

        ///<summary>
        ///Present position of the axis 
        ///</summary>
        public const short APos = 2061;

        ///<summary>
        ///Present is-value position 
        ///</summary>
        public const short RPos = 2062;

        ///<summary>
        ///Present running velocity 
        ///</summary>
        public const short RVelo = 2063;

        ///<summary>
        ///
        ///</summary>
        public const short XEncoder = 2064;

        ///<summary>
        ///Override the present velocity of the axis 
        ///</summary>
        public const short XOvr = 2075;

        ///<summary>
        ///Override mode 
        ///</summary>
        public const short XOmode = 2076;

        ///<summary>
        ///
        ///</summary>
        public const short EgState = 2090;

        ///<summary>
        ///
        ///</summary>
        public const short EgMode = 2092;

        ///<summary>
        ///
        ///</summary>
        public const short EgSource = 2093;

        ///<summary>
        ///
        ///</summary>
        public const short EgMult = 2094;

        ///<summary>
        ///
        ///</summary>
        public const short TOP_XPARM_NO = 2094;

        ///<summary>
        ///High velocity 
        ///</summary>
        public const short BOT_IPARM_NO = 3000;

        ///<summary>
        ///High velocity 
        ///</summary>
        public const short IpSpeed = 3000;

        ///<summary>
        ///Acceleration 
        ///</summary>
        public const short IpAccel = 3001;

        ///<summary>
        ///Deceleration 
        ///</summary>
        public const short IpDecel = 3002;

        ///<summary>
        ///Start/Stop velocity 
        ///</summary>
        public const short IpVmin = 3003;

        ///<summary>
        ///Emergency stop ramp 
        ///</summary>
        public const short IpLDecel = 3004;

        ///<summary>
        ///End velocity 
        ///</summary>
        public const short IpVend = 3005;

        ///<summary>
        ///Feed motion velocity 
        ///</summary>
        public const short IpFeed = 3006;

        ///<summary>
        ///Maximum programmable velocity 
        ///</summary>
        public const short IpMaxVel = 3008;

        ///<summary>
        ///Maximum programmable acceleration 
        ///</summary>
        public const short IpMaxAcc = 3009;

        ///<summary>
        ///Number of shorterpolation axes 
        ///</summary>
        public const short IpDim = 3010;

        ///<summary>
        ///Specification for trajectory commands 
        ///</summary>
        public const short IpUnit = 3011;

        ///<summary>
        ///Look Ahead, factor for acceleration 
        ///</summary>
        public const short IpLaFact = 3013;

        ///<summary>
        ///Look Ahead, factor for circular acceleration 
        ///</summary>
        public const short IpLaCvFact = 3014;

        ///<summary>
        ///Look Ahead, running time 
        ///</summary>
        public const short IpLaTprof = 3015;

        ///<summary>
        ///Present trajectory velocity 
        ///</summary>
        public const short IpVelo = 3063;

        ///<summary>
        ///Override present trajectory velocity 
        ///</summary>
        public const short IpOvr = 3075;

        ///<summary>
        ///Override mode 
        ///</summary>
        public const short IpOmode = 3076;

        ///<summary>
        ///Override mode 
        ///</summary>
        public const short TOP_IPARM_NO = 3076;

        ///<summary>
        ///Polarity of the digital inputs 
        ///</summary>
        public const short BOT_IOPARM_NO = 4000;

        ///<summary>
        ///Polarity of the digital inputs 
        ///</summary>
        public const short InPolarity = 4000;

        ///<summary>
        ///Polarity of the digital outputs 
        ///</summary>
        public const short OutPolarity = 4001;

        ///<summary>
        ///Inquiry whether the output ports address is available 
        ///</summary>
        public const short TOP_IOPARM_NO = 4003;



        //  Xemo_DLL Koordinaten-Bezeichner
        // ---------------------------------
        public const short C_X = 0x1;
        public const short C_Y = 0x2;
        public const short C_XY = 0x3;
        public const short C_Z = 0x4;
        public const short C_XZ = 0x5;
        public const short C_YZ = 0x6;
        public const short C_XYZ = 0x7;
        public const short C_A = 0x8;
        public const short C_XA = 0x9;
        public const short C_YA = 0xA;
        public const short C_XYA = 0xB;
        public const short C_ZA = 0xC;
        public const short C_XZA = 0xD;
        public const short C_YZA = 0xE;
        /// <summary>
        /// 
        /// </summary>
        public const short C_XYZA = 0xF;

        // Xemo_DLL Error-Codes 
        // ------------------------------- 
        public const short ERR_XEMO = 1;
        public const short ERR_COM_PORT = 2;
        public const short ERR_RCV_OVERFLOW = 3;
        public const short ERR_RCV_TIMEOUT = 4;
        public const short ERR_FIFO_TIMEOUT = 5;
        public const short ERR_GETSTATE = 6;
        public const short ERR_RCV_CMD = 7;
        public const short ERR_TRM_TIMEOUT = 8;
        public const short ERR_CHECKSUM = 9;

        // Xemo_DLL Error-Status 
        // ------------------------------- 
        public const short ERR_LEFT = -1;
        public const short NO_ERR = 0;
        public const short ERR_XEMO_PENDING = 1;
        public const short ERR_COM_PENDING = 2;
        public const short ERR_RETRY = 3;
        public const short ERR_CANCEL = 4;
        public const short ERR_COM_DEINI = 5;

    } // internal static class XemoConst
}     // namespace Regata.Core.Hardware.Xemo
