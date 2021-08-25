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

namespace Regata.Core.Hardware.Xemo
{
    /// <summary>
    /// Xemo errors description and codes
    /// </summary>
    /// <remarks></remarks>
    internal static class XemoErrors
    {
        ///<summary>
        /// Unknown command or P-code
        /// NOTE: Critical error
        ///</summary>
        public const int UnknwCom = 1;
        ///<summary>
        /// Exceeds data range
        /// NOTE: Critical error
        ///</summary>
        public const int ExcDataRng = 2;
        ///<summary>
        /// Stack overflow
        /// NOTE: Critical error
        ///</summary>
        public const int StckOvrfl = 3;
        ///<summary>
        /// Unknown library function
        /// NOTE: Critical error
        ///</summary>
        public const int UnknwLibFunc = 4;
        ///<summary>
        /// Unknown operator
        /// NOTE: Critical error
        ///</summary>
        public const int _UnknwOper = 5;
        ///<summary>
        /// Overflow during type conversion 
        ///</summary>
        public const int TypeConvOverflw = 6;
        ///<summary>
        /// P-Code not implemented
        /// NOTE: Critical error
        ///</summary>
        public const int PCodeNotImpl = 7;
        ///<summary>
        /// Array dimension conflict 
        ///</summary>
        public const int ArrDimWrong = 8;
        ///<summary>
        /// Exceeds array range 
        ///</summary>
        public const int OutOfRange = 9;
        ///<summary>
        /// Library function not implemented
        /// NOTE: Critical error
        ///</summary>
        public const int LibFuncNotImpl = 10;
        ///<summary>
        /// Exceeds maximum string length 
        ///</summary>
        public const int OutOfStrRange = 11;
        ///<summary>
        /// Not enough memory for data range
        /// NOTE: Critical error
        ///</summary>
        public const int NotEnoughMmrDataRange = 12;
        ///<summary>
        /// Not enough memory for stack area
        /// NOTE: Critical error
        ///</summary>
        public const int NotEnoughMmrStack = 13;
        ///<summary>
        /// Not enough memory for P-code
        /// NOTE: Critical error
        ///</summary>
        public const int NotEnoughMmrPCode = 14;
        ///<summary>
        /// online fifo overflow 
        ///</summary>
        public const int FifoOvrfl = 15;
        ///<summary>
        /// Timeout while burning flash
        /// NOTE: Critical error
        ///</summary>
        public const int TimeOutBurnFlash = 16;
        ///<summary>
        /// Erase error for flash sector
        /// NOTE: Critical error
        ///</summary>
        public const int EraseErrorFlashSec = 17;
        ///<summary>
        /// Read-only for flash active
        /// NOTE: Critical error
        ///</summary>
        public const int ReadOnlyFlashActive = 18;
        ///<summary>
        /// Check-sum error in P-code
        /// NOTE: Critical error
        ///</summary>
        public const int _19 = 19;
        ///<summary>
        /// Invalid signature in P-code
        /// NOTE: Critical error
        ///</summary>
        public const int _20 = 20;
        ///<summary>
        /// Not enough memory for EEprom area
        /// NOTE: Critical error
        ///</summary>
        public const int _21 = 21;
        ///<summary>
        /// Read-only for EEprom active 
        ///</summary>
        public const int _22 = 22;
        ///<summary>
        /// Timeout while burning EEprom 
        ///</summary>
        public const int _23 = 23;
        ///<summary>
        /// Invalid axis number 
        ///</summary>
        public const int InvAxisNum = 24;
        ///<summary>
        /// Invalid parameter number 
        ///</summary>
        public const int InvParamNum = 25;
        ///<summary>
        /// Invalid Setfifo command 
        ///</summary>
        public const int _26 = 26;
        ///<summary>
        /// Invalid SysControl command 
        ///</summary>
        public const int _27 = 27;
        ///<summary>
        /// Invalid I/O address 
        ///</summary>
        public const int _28 = 28;
        ///<summary>
        /// Assignment to a constant not possible 
        ///</summary>
        public const int _29 = 29;
        ///<summary>
        /// Task already active 
        ///</summary>
        public const int _30 = 30;
        ///<summary>
        /// Invalid signature in EEprom
        /// NOTE: Critical error
        ///</summary>
        public const int _31 = 31;
        ///<summary>
        /// Defect memory allocation in EEprom
        /// NOTE: Critical error
        ///</summary>
        public const int _32 = 32;
        ///<summary>
        /// Check-sum error in EEprom
        /// NOTE: Critical error
        ///</summary>
        public const int _33 = 33;
        ///<summary>
        /// Incompatible P-code
        /// NOTE: Critical error
        ///</summary>
        public const int _35 = 35;
        ///<summary>
        /// Assignment to identical string not permitted 
        ///</summary>
        public const int _36 = 36;
        ///<summary>
        /// Limit switch reached 
        ///</summary>
        public const int LimSwtchRchd = 37;
        ///<summary>
        /// Not enough memory for download 
        ///</summary>
        public const int _38 = 38;
        ///<summary>
        /// Invalid parameter value 
        ///</summary>
        public const int InvParamVal = 39;
        ///<summary>
        /// Function not configured 
        ///</summary>
        public const int _40 = 40;
        ///<summary>
        /// Command only permitted during axis standstill 
        ///</summary>
        public const int _41 = 41;
        ///<summary>
        /// Circle commands require at least 2D 
        ///</summary>
        public const int _42 = 42;
        ///<summary>
        /// No program loaded 
        ///</summary>
        public const int _43 = 43;
        ///<summary>
        /// Unknown subprocedure number 
        ///</summary>
        public const int _45 = 45;
        ///<summary>
        /// target position outside the software limit 
        ///</summary>
        public const int TrgtPosExcSL = 47;
        ///<summary>
        /// Parameter value too large 
        ///</summary>
        public const int ParValTooLrg = 48;
        ///<summary>
        /// Not enabled 
        ///</summary>
        public const int _49 = 49;
        ///<summary>
        /// Software limit reached 
        ///</summary>
        public const int LimSoftRchd = 50;
        ///<summary>
        /// Parameter can only be read 
        ///</summary>
        public const int _51 = 51;
        ///<summary>
        /// Contouring error in the electronic transmission 
        ///</summary>
        public const int _52 = 52;
        ///<summary>
        /// Ventilator overload (overcurrent) 
        ///</summary>
        public const int _53 = 53;
        ///<summary>
        /// Excess temperature in the device 
        ///</summary>
        public const int ExcDevTemp = 54;
        ///<summary>
        /// Error in the index monitoring at the encoder input 
        ///</summary>
        public const int _55 = 55;
        ///<summary>
        /// Electric error in the encoder signal 
        ///</summary>
        public const int _56 = 56;
        ///<summary>
        /// Electronic transmission: synchronic position missed 
        ///</summary>
        public const int _57 = 57;
        ///<summary>
        /// Function not available in this stage of expansion 
        ///</summary>
        public const int _58 = 58;
        ///<summary>
        /// Function (Gantry operation, step monitoring) not available with this hardware 
        ///</summary>
        public const int _59 = 59;
        ///<summary>
        /// Velocity setting too large 
        ///</summary>
        public const int _60 = 60;
        ///<summary>
        /// Acceleration setting too large 
        ///</summary>
        public const int _61 = 61;
        ///<summary>
        /// Circle radius too large 
        ///</summary>
        public const int _62 = 62;
        ///<summary>
        /// Negative parameter value not permitted 
        ///</summary>
        public const int _63 = 63;
        ///<summary>
        /// Error during reference run 
        ///</summary>
        public const int _64 = 64;
        ///<summary>
        /// Error on a power card 
        ///</summary>
        public const int _65 = 65;
        ///<summary>
        /// Encoder input overflow 
        ///</summary>
        public const int _66 = 66;
        ///<summary>
        /// Not permitted with activated electronic transmission 
        ///</summary>
        public const int _67 = 67;
        ///<summary>
        /// Parameter value must be unequal to zero 
        ///</summary>
        public const int _68 = 68;
        ///<summary>
        /// CAN communication error 
        ///</summary>
        public const int _69 = 69;
        ///<summary>
        /// Check-sum error in online command 
        ///</summary>
        public const int _70 = 70;
        ///<summary>
        /// Over- or undervoltage in the 12 volt vehicle voltage 
        ///</summary>
        public const int _71 = 71;
        ///<summary>
        /// Over- or undervoltage in the 24 volt feed in 
        ///</summary>
        public const int _72 = 72;
        ///<summary>
        /// Over- or undervoltage in the motor's intermediate circuit voltage 
        ///</summary>
        public const int _73 = 73;
        ///<summary>
        /// Short circuit in a digital output 
        ///</summary>
        public const int _74 = 74;
        ///<summary>
        /// Serial interface: format error 
        ///</summary>
        public const int _75 = 75;
        ///<summary>
        /// Serial interface: overflow 
        ///</summary>
        public const int _76 = 76;
        ///<summary>
        /// Setup parameter is write-protected 
        ///</summary>
        public const int _77 = 77;
        ///<summary>
        /// Error during writing of the setup parameter 
        ///</summary>
        public const int _78 = 78;
        ///<summary>
        /// Setup parameter check-sum error 
        ///</summary>
        public const int _79 = 79;
        ///<summary>
        /// Communications error in the second CAN channel 
        ///</summary>
        public const int _80 = 80;
        ///<summary>
        /// axis is not available 
        ///</summary>
        public const int _81 = 81;
        ///<summary>
        /// Too many I/O ports 
        ///</summary>
        public const int _82 = 82;
        ///<summary>
        /// CANopen guarding error 
        ///</summary>
        public const int _83 = 83;
        ///<summary>
        /// axis regulator cannot be switched on 
        ///</summary>
        public const int _84 = 84;
        ///<summary>
        /// axis regulator has turned itself off 
        ///</summary>
        public const int _85 = 85;
        ///<summary>
        /// No axis is registered 
        ///</summary>
        public const int _86 = 86;
        ///<summary>
        /// Reference run method not implemented 
        ///</summary>
        public const int _87 = 87;
        ///<summary>
        /// H-portal transformation: axes not within a coordinate system 
        ///</summary>
        public const int _88 = 88;
        ///<summary>
        /// Not permitted with a switched-on gantry axis 
        ///</summary>
        public const int _89 = 89;
        ///<summary>
        /// Error in axis regulator; cannot be read out 
        ///</summary>
        public const int _99 = 99;
        ///<summary>
        /// Unknown error in the axis regulator 
        ///</summary>
        public const int _100 = 100;
        ///<summary>
        /// Software reset in the axis regulator 
        ///</summary>
        public const int _101 = 101;
        ///<summary>
        /// Loss of synchronization in the axis regulator 
        ///</summary>
        public const int _102 = 102;
        ///<summary>
        /// Motor-encoder antivalence error 
        ///</summary>
        public const int _103 = 103;
        ///<summary>
        /// Motor-encoder counter error 
        ///</summary>
        public const int _104 = 104;
        ///<summary>
        /// Master-encoder counter error 
        ///</summary>
        public const int _105 = 105;
        ///<summary>
        /// Excessive temperature in the axis regulator 
        ///</summary>
        public const int _106 = 106;
        ///<summary>
        /// Undervoltage in the logic-unit supply in the axis regulator 
        ///</summary>
        public const int _107 = 107;
        ///<summary>
        /// Intermediate circuit overvoltage in the axis regulator 
        ///</summary>
        public const int _108 = 108;
        ///<summary>
        /// Intermediate circuit undervoltage in the axis regulator 
        ///</summary>
        public const int _109 = 109;
        ///<summary>
        /// Short circuit phase A in the axis regulator 
        ///</summary>
        public const int _110 = 110;
        ///<summary>
        /// Short circuit phase B in the axis regulator 
        ///</summary>
        public const int _111 = 111;
        ///<summary>
        /// Short circuit digital output in the axis regulator 
        ///</summary>
        public const int _112 = 112;
        ///<summary>
        /// Axis regulator not enabled 
        ///</summary>
        public const int _113 = 113;
        ///<summary>
        /// Contouring error too high 
        ///</summary>
        public const int _114 = 114;
        ///<summary>
        /// Velocity too high 
        ///</summary>
        public const int _115 = 115;
        ///<summary>
        /// Communication not found 
        ///</summary>
        public const int _116 = 116;
        ///<summary>
        /// CAN communication interrupted 
        ///</summary>
        public const int _117 = 117;
        ///<summary>
        /// i^2*t - monitoring activated 
        ///</summary>
        public const int _118 = 118;
        ///<summary>
        /// Negative hardware end position activated 
        ///</summary>
        public const int _119 = 119;
        ///<summary>
        /// Positive hardware end position activated 
        ///</summary>
        public const int _120 = 120;


    } // internal static class XemoErrors
}     // namespace Regata.Core.Hardware.Xemo
