
using System;
using System.Runtime.InteropServices;

namespace Regata.Core.Hardware.Xemo
{
    /// <summary>
    /// Definition der XemoDLL-Funktionen
    /// </summary>
    /// <remarks></remarks>
    internal static class XemoDLL
    {
        public const string XEMO_DLL_VESRION = "2.40";

        // ----------------------------------------------------------------------
        //  Initialisierungs Funktionen
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_TimeOut@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_TimeOut(double ComTimeout, double FifoTimeout);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoIdle@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_FifoIdle(Int32 FifoIdle);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_IniCom@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_IniCom(Int16 ComNo, long Baud);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_IniUsb@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_IniUsb(Int16 ComNo, string SerialNo);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DeIniCom@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DeIniCom();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DeIniComPort@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DeIniComPort(Int16 ComNo);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_ComSelect@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_ComSelect(Int16 ComNo);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_CsumMode@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_CsumMode(Int16 mode);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetDllVersion@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_GetDllVersion(string Version, Int16 MaxLen);

        // ----------------------------------------------------------------------
        //  Fehlerbehandlung
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_ErrorCallBack@4", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void ML_ErrorCallBack(Int32 ErrorFunc);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_RunErrCallBack@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_RunErrCallBack(Int32 ErrorFunc);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetErrCode@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_GetErrCode();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetErrState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_GetErrState();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_SetErrState@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_SetErrState(Int16 State);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_LastRunErr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_LastRunErr();

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetComErrText@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_GetComErrText(Int16 ErrCode, string ErrTxt, Int16 MaxLen);

        // ----------------------------------------------------------------------
        //  Daten�bertragung
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoFull@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_FifoFull();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_FifoBreak@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_FifoBreak();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_DoEvents@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_DoEvents();

        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutChar@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutChar(Int16 TrmChar);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutWord@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutWord(Int16 TrmWord);
        [DllImport("XemoDll.dll", EntryPoint = "_ML_PutLong@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void ML_PutLong(Int32 TrmLong);

        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetRcvState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_GetRcvState();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetChar@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_GetChar();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetWord@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 ML_GetWord();
        [DllImport("XemoDll.dll", EntryPoint = "_ML_GetLong@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 ML_GetLong();

        // ----------------------------------------------------------------------
        //  MotionBasic-Funktionen
        // ----------------------------------------------------------------------
        //  Systemkontrolle
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SysControl@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SysControl(Int16 Control);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_GetState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_GetState();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SetFifo@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SetFifo(Int16 State);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_ResErr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_ResErr();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Call@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Call(Int16 ProgNr);

        //  Systemparameter
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Set@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Set(Int16 Parameter, Int32 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_ASet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_ASet(Int16 Achse, Int16 Parameter, Int32 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_IoSet@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_IoSet(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2, Int16 Parameter, Int16 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Get@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 MB_Get(int Parameter);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_AGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 MB_AGet(int Achse, Int16 Parameter);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_IoGet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_IoGet(int ByteNr, Int16 BitNr1, Int16 BitNr2, Int16 Parameter);

        //  Steuerung einzelner Achsen
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Jog@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Jog(Int16 Achse, Int32 Geschwindigkeit);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Amove@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Amove(Int16 Achse, Int32 Zielposition);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Rmove@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Rmove(Int16 Achse, Int32 Verfahrweg);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Home@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Home(Int16 Achse);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Stop@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Stop(Int16 Achse);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Busy@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_Busy(Int16 Achse);

        //  Bahnsteuerung
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin(Int16 C_Mask, ref Int32 Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin0@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin0(Int16 C_Mask, ref Int32 Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Lin1@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Lin1(Int16 C_Mask, ref Int32 Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Circle@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Circle(Int32 Radius, Int32 Anfangswinkel, Int32 Endwinkel);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arc@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arc(Int16 C_Mask, Int32 Radius, ref Int32 Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arcc@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arcc(Int16 C_Mask, Int32 Mx, Int32 My, ref Int32 Coordinate);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Arcw@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Arcw(Int16 C_Mask, Int32 Mx, Int32 My, ref Int32 Coordinate);

        //  Ein- und Ausg�nge
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Out@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Out(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2, Int16 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Sout@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Sout(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2, Int16 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Rout@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_Rout(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_In@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_In(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Waitinp@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Waitinp(Int16 ByteNr, Int16 BitNr1, Int16 BitNr2, Int16 Wert);

        [DllImport("XemoDll.dll", EntryPoint = "_MB_Inw@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_Inw(Int16 ByteNr);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Outw@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Outw(Int16 ByteNr, Int16 Wert);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Routw@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_Routw(Int16 ByteNr);

        //  Textausgabe
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Print@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Print(string Text);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Printxy@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Printxy(Int16 x, Int16 y, string Text);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cpos@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cpos(Int16 x, Int16 y);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Ctype@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Ctype(Int16 c_Type);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_TextAttrib@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_TextAttrib(Int16 Attrib);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cls@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cls();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Cleol@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Cleol();

        //  Tastatureingabe
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyState@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_KeyState();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyPressed@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_KeyPressed();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyRead@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int16 MB_KeyRead();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyClear@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_KeyClear();
        [DllImport("XemoDll.dll", EntryPoint = "_MB_KeyLed@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_KeyLed(Int16 Key, Int16 Onoff);

        //  Zeitfunktionen
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Delay@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Delay(Int32 Verweilzeit);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_Still@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_Still(Int32 Achse);

        // ----------------------------------------------------------------------
        //  Weitere nicht dokumetierte Funktionen
        // ----------------------------------------------------------------------
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SdoTrm@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void MB_SdoTrm(Int16 NodeId, Int16 Index, Int16 SubIndex, Int32 Value);
        [DllImport("XemoDll.dll", EntryPoint = "_MB_SdoRcv@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 MB_SdoRcv(Int16 NodeId, Int16 Index, Int16 SubIndex);
    }
}