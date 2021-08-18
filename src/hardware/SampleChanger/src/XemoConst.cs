
using System;

namespace Regata.Core.Hardware.Xemo
{
    /// <summary>
    /// Konstanten Definitionen
    /// </summary>
    /// <remarks></remarks>
    internal static class XemoConst
    {
        public const long BAUDRATE = 19200;

        // Bits im Systemstatus 
        // ------------------------------- 
        public const int STATE_F0 = 0x1;
        public const int STATE_F1 = 0x2;
        public const int STATE_F2 = 0x4;
        public const int STATE_ERR = 0x8;
        public const int STATE_MS = 0x10;
        public const int STATE_OR0 = 0x20;
        public const int STATE_RUN = 0x40;

        public const int PSTATE_PSTATE = 0x7;
        public const int PSTATE_ERROR = 0x8;
        public const int PSTATE_ONERROR = 0x10;
        public const int PSTATE_DEBUG = 0x40;

        public const int STATE_FIFO = 0x3;
        public const int STATE_FIFO_EMPTY = 0;
        public const int STATE_FIFO_LOW = 1;
        public const int STATE_FIFO_HIGH = 2;
        public const int STATE_FIFO_FULL = 3;

        // Syscontrol Kommandos 
        public const int Reset = 1;     // System Reset 
        public const int Break = 2;     // MotionBasic Programm unterbrechen 
        public const int Halt = 3;      // MotionBasic Programm anhalten 

        public const int PrgQuit = 100;         // MotionBasic Programm benden 
        public const int DebugShell = 101;      // Zur Debug-Shell wechseln 
        public const int OnlineShell = 102;     // Zur Online-Shell wechseln 

        // SetFifo FifoStatus 
        // ------------------------------- 
        public const int FfDisable = 1;
        public const int FfEnable = 2;
        public const int FfClear = 3;

        // Tastatur-Codes 
        public const int Key_F1 = 0x13b;
        public const int Key_F2 = 0x13c;
        public const int Key_F3 = 0x13d;
        public const int Key_F4 = 0x13e;
        public const int Key_Bs = 0x8;
        public const int Key_Enter = 0xd;
        public const int Key_Up = 0x148;
        public const int Key_Down = 0x150;
        public const int Key_Right = 0x14d;
        public const int Key_Left = 0x14b;

        // KeyLed-Codes 
        // ------------------------------- 
        public const int LedOff = 0;
        public const int LedOn = 1;
        public const int LedBlink = 10;
        public const int LedFlash = 11;

        // Die Achsbezeichner 
        // ------------------------------- 
        public const int X = 0;
        public const int Y = 1;
        public const int Z = 2;
        public const int A = 3;
        public const int XALL = 255; // Alle Achsen / Achsparameter 
        public const int ALL = 254;  // Alle Achs- und Bahnparameter 

        // Seriell IO 
        public const int COM1 = 1;
        public const int BD9600 = 5;
        public const int BD19200 = 6;
        public const int BD28800 = 7;
        public const int BD38400 = 8;
        public const int BD57600 = 9;

        // USB 
        public const int USB_OFF = 0;
        public const int USB_AUTO = 1;
        public const int USB_ONLY = 2;

        // Diverse 
        public const int Off = 0;

        // Systemparameter 
        public const int BOT_SPARM_NO = 1000;

        public const int State = 1000;
        public const int Version = 1004;
        public const int Release = 1005;
        public const int ComRelease = 1006;
        public const int Extent = 1007;
        public const int Clock = 1008;
        public const int ErrNo = 1010;
        public const int ErrState = 1011;
        public const int ErrLine = 1012;
        public const int ErrAxis = 1013;
        public const int ErrParam = 1014;
        public const int SubError = 1015;

        public const int FifoSize = 1021;
        public const int FifoLevel = 1022;
        public const int FifoLeft = 1023;
        public const int FifoMarker = 1024;

        public const int Mode1 = 1030;
        public const int CanMode = 1034;
        public const int OtSelect = 1035;
        public const int Can2Mode = 1036;
        public const int Can1Device = 1038;
        public const int Can2Device = 1039;

        public const int IpGroup = 1040;

        public const int BkPwmCycle = 1050;
        public const int BkPwmDuty = 1051;

        public const int AnlIn = 1060;
        public const int AnlOut = 1061;

        public const int OvrIn = 1062;
        public const int JoyX = 1063;
        public const int JoyY = 1064;

        public const int HandWheel = 1070;
        public const int Encoder = 1071;
        public const int EncIdx = 1072;
        public const int EncIpr = 1073;

        public const int Ovride = 1075;
        public const int Omode = 1076;
        public const int OvScale = 1077;

        public const int SAnlIn0 = 1080;
        public const int SAnlIn1 = 1081;
        public const int SAnlIn2 = 1082;

        public const int VoMode = 1090;
        public const int VoScale = 1091;
        public const int VoOffs = 1092;
        public const int VoMin = 1093;
        public const int VoMax = 1094;

        public const int AnlFSel = 1100;
        public const int AFType = 1101;
        public const int AFSTime = 1102;
        public const int AFSpread = 1103;
        public const int AFHyster = 1104;
        public const int AFTrack = 1105;
        public const int AFAverage = 1106;

        // ab 1200 sind zun�chst nicht dokumetierte 
        // Systemparameter zur Ger�te�berwachung 
        public const int TOP_SPARM_NO = 1250;     // Gr�sste Systemparameter-Nummer 

        // Setup-Parameter 
        // Die Setup-Parameter sind besonders schreibgesch�tzt 
        // und k�nnen nur beschrieben werden, wenn der Hexschalter auf 0x0e steht 
        public const int BOT_SUPARM_NO = 1400;
        public const int ComPort = 1400;      // RS232 Programmier-Schnittstelle ausw�hlen 
        public const int Bdrate = 1401;       // Baudrate der RS232 Schnittstelle festlegen 
        public const int DsplContr = 1402;    // Xemo Display Kontrast 
        public const int PonComDelay = 1403;  // Power On ComPort Delay 
        public const int USBConfig = 1404;    // USB Schnittstelle konfigurieren 
        public const int TOP_SUPARM_NO = 1404;

        // Achsparameter 
        public const int BOT_XPARM_NO = 2000;

        public const int Speed = 2000;
        public const int Accel = 2001;
        public const int Decel = 2002;
        public const int Vmin = 2003;
        public const int LDecel = 2004;
        
        public const int Jerkms = 2007;
        
        public const int MaxVel = 2008;
        public const int MaxAcc = 2009;
        public const int IpLink = 2010;
        public const int IpAxis = 2011;

        public const int NodeId = 2012;
        
        public const int XType = 2013;

        public const int H1Speed = 2020;
        public const int H2Speed = 2021;
        public const int H3Speed = 2022;
        public const int HOffset = 2023;
        public const int HVerify = 2024;
        public const int HMethod = 2025;

        public const int OpMode = 2030;
        public const int Current = 2031;
        public const int FErrWin = 2032;
        public const int TPosWin = 2033;
        public const int TPosTime = 2034;
        public const int BrakeOutp = 2035;
        public const int BrkDelay1 = 2036;
        public const int BrkDelay2 = 2037;

        public const int Uscale = 2040;
        public const int Iscale = 2041;

        public const int Zero = 2042;
        public const int Origin = 2043;
        public const int Refp = 2044;
        public const int SlLimit = 2045;
        public const int SrLimit = 2046;
        public const int Pmode = 2047;
        public const int BLash = 2048;
        
        public const int Gantry = 2049;

        public const int Micro = 2050;
        public const int MPhi = 2051;
        public const int StopCurr = 2052;
        
        public const int StpEncoder = 2056;

        public const int APos = 2061;
        public const int RPos = 2062;
        public const int RVelo = 2063;
        public const int XEncoder = 2064;

        public const int XOvr = 2075;
        public const int XOmode = 2076;

        public const int EgState = 2090;
        public const int EgMode = 2092;
        public const int EgSource = 2093;
        public const int EgMult = 2094;

        public const int TOP_XPARM_NO = 2094;

        // Bahnparameter 
        public const int BOT_IPARM_NO = 3000;

        public const int IpSpeed = 3000;
        public const int IpAccel = 3001;
        public const int IpDecel = 3002;
        public const int IpVmin = 3003;
        public const int IpLDecel = 3004;
        public const int IpVend = 3005;
        public const int IpFeed = 3006;
        public const int IpMaxVel = 3008;
        public const int IpMaxAcc = 3009;
        public const int IpDim = 3010;
        public const int IpUnit = 3011;

        public const int IpLaFact = 3013;
        public const int IpLaCvFact = 3014;
        public const int IpLaTprof = 3015;

        public const int IpVelo = 3063;

        public const int IpOvr = 3075;
        public const int IpOmode = 3076;

        public const int TOP_IPARM_NO = 3076;

        // EA-Parameter 
        public const int BOT_IOPARM_NO = 4000;
        public const int InPolarity = 4000;
        public const int OutPolarity = 4001;
        public const int TOP_IOPARM_NO = 4003;

        //  Xemo_DLL Koordinaten-Bezeichner
        // ---------------------------------
        public const int C_X = 0x1;
        public const int C_Y = 0x2;
        public const int C_XY = 0x3;
        public const int C_Z = 0x4;
        public const int C_XZ = 0x5;
        public const int C_YZ = 0x6;
        public const int C_XYZ = 0x7;
        public const int C_A = 0x8;
        public const int C_XA = 0x9;
        public const int C_YA = 0xA;
        public const int C_XYA = 0xB;
        public const int C_ZA = 0xC;
        public const int C_XZA = 0xD;
        public const int C_YZA = 0xE;
        public const int C_XYZA = 0xF;

        // Xemo_DLL Error-Codes 
        // ------------------------------- 
        public const int ERR_XEMO = 1;
        public const int ERR_COM_PORT = 2;
        public const int ERR_RCV_OVERFLOW = 3;
        public const int ERR_RCV_TIMEOUT = 4;
        public const int ERR_FIFO_TIMEOUT = 5;
        public const int ERR_GETSTATE = 6;
        public const int ERR_RCV_CMD = 7;
        public const int ERR_TRM_TIMEOUT = 8;
        public const int ERR_CHECKSUM = 9;

        // Xemo_DLL Error-Status 
        // ------------------------------- 
        public const int ERR_LEFT = -1;
        public const int NO_ERR = 0;
        public const int ERR_XEMO_PENDING = 1;
        public const int ERR_COM_PENDING = 2;
        public const int ERR_RETRY = 3;
        public const int ERR_CANCEL = 4;
        public const int ERR_COM_DEINI = 5;

    }
}