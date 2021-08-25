using System.Runtime.InteropServices;

namespace Regata.Core.Hardware.Xemo
{
    /// <summary>
    /// Definition der XemoDLL-Funktionen
    /// </summary>
    /// <remarks></remarks>
    /// 
    internal static class XemoDLL
    {
        
        // TODO: can we use it?
        // 
        //35   22 00002740 FT_GetStatus
        //37   24 00001910 FT_ListDevices
        //24   17 000090E0 FT_GetComPortNumber
        //25   18 00006C10 FT_GetDeviceInfo
        //26   19 00008940 FT_GetDeviceInfoDetail
        //27   1A 00008850 FT_GetDeviceInfoList
        //28   1B 00008A80 FT_GetDriverVersion

        public const string XEMO_DLL_VESRION = "2.40";

        // ----------------------------------------------------------------------
        //  Initialisierungs Funktionen
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_TimeOut@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_TimeOut(double ComTimeout, double FifoTimeout);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoIdle@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_FifoIdle(int FifoIdle);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_IniCom@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_IniCom(short ComNo, long Baud);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_IniUsb@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_IniUsb(short ComNo, string SerialNo);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DeIniCom@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DeIniCom();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DeIniComPort@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DeIniComPort(short ComNo);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_ComSelect@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_ComSelect(short ComNo);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_CsumMode@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_CsumMode(short mode);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetDllVersion@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_GetDllVersion(string Version, short MaxLen);

        // ----------------------------------------------------------------------
        //  Fehlerbehandlung
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_ErrorCallBack@4", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void ML_ErrorCallBack(int ErrorFunc);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_RunErrCallBack@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_RunErrCallBack(int ErrorFunc);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetErrCode@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_GetErrCode();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetErrState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_GetErrState();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_SetErrState@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_SetErrState(short State);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_LastRunErr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_LastRunErr();

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetComErrText@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_GetComErrText(short ErrCode, string ErrTxt, short MaxLen);

        // ----------------------------------------------------------------------
        //  Daten�bertragung
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoFull@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_FifoFull();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoBreak@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_FifoBreak();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DoEvents@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DoEvents();

        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutChar@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutChar(short TrmChar);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutWord@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutWord(short TrmWord);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutLong@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutLong(int TrmLong);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetRcvState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_GetRcvState();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetChar@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_GetChar();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetWord@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short ML_GetWord();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetLong@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int ML_GetLong();

        // ----------------------------------------------------------------------
        //  MotionBasic-Funktionen
        // ----------------------------------------------------------------------
        //  Systemkontrolle
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SysControl@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SysControl(short Control);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_GetState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_GetState();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SetFifo@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SetFifo(short State);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_ResErr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_ResErr();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Call@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Call(short ProgNr);

        //  Systemparameter
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Set@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Set(short Parameter, int Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_ASet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_ASet(short Axis, short Parameter, int Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_IoSet@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_IoSet(short ByteNr, short BitNr1, short BitNr2, short Parameter, short Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Get@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int MB_Get(int Parameter);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_AGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int MB_AGet(int Axis, short Parameter);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_IoGet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_IoGet(int ByteNr, short BitNr1, short BitNr2, short Parameter);

        //  Steuerung einzelner Axisn
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Jog@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Jog(short Axis, int Velocity);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Amove@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Amove(short Axis, int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Rmove@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Rmove(short Axis, int RelCoordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Home@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Home(short Axis);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Stop@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Stop(short Axis);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Busy@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_Busy(short Axis);

        //  Bahnsteuerung
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin(short C_Mask, ref int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin0@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin0(short C_Mask, ref int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin1@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin1(short C_Mask, ref int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Circle@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Circle(int Radius, int InitAngle, int FinalAngle);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arc@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arc(short C_Mask, int Radius, ref int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arcc@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arcc(short C_Mask, int Mx, int My, ref int Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arcw@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arcw(short C_Mask, int Mx, int My, ref int Coordinate);

        //  Ein- und Ausg�nge
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Out@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Out(short ByteNr, short BitNr1, short BitNr2, short Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Sout@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Sout(short ByteNr, short BitNr1, short BitNr2, short Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Rout@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_Rout(short ByteNr, short BitNr1, short BitNr2);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_In@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_In(short ByteNr, short BitNr1, short BitNr2);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Waitinp@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Waitinp(short ByteNr, short BitNr1, short BitNr2, short Value);

        [DllImport("XemoDll.dll", EntryPoint = "_MB_Inw@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_Inw(short ByteNr);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Outw@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Outw(short ByteNr, short Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Routw@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_Routw(short ByteNr);

        //  Textausgabe
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Print@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Print(string Text);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Printxy@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Printxy(short x, short y, string Text);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cpos@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cpos(short x, short y);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Ctype@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Ctype(short c_Type);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_TextAttrib@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_TextAttrib(short Attrib);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cls@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cls();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cleol@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cleol();

        //  Tastatureingabe
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_KeyState();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyPressed@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_KeyPressed();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyRead@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern short MB_KeyRead();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyClear@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_KeyClear();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyLed@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_KeyLed(short Key, short Onoff);

        //  Zeitfunktionen
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Delay@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Delay(int TimeOut);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Still@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Still(int Axis);

        // ----------------------------------------------------------------------
        //  Weitere nicht dokumetierte Funktionen
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SdoTrm@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SdoTrm(short NodeId, short Index, short SubIndex, int Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SdoRcv@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int MB_SdoRcv(short NodeId, short Index, short SubIndex);

    } // internal static class XemoDLL
}     // namespace Regata.Core.Hardware.Xemo