/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


/// <summary>
/// This file contains definition of basic codes. The idea of such usage is support general code for any application and also allow user to extend code by his own codes. One of the disadvanteges is hot to guarantee unique code identifier in case of user will extend it? Perhaps attributes?
/// </summary>
namespace Regata.Core
{
    /// <summary>
    /// Codes class contains unique identificator of message. 
    /// - [3000-4000) - Error codes
    ///    -[3000-3099) - DataBase           error codes
    ///    -[3100-3199) - Cloud              error codes
    ///    -[3200-3299) - Detector           error codes
    ///    -[3300-3399) - Logger             error codes
    ///    -[3400-3499) - Settings           error codes
    ///    -[3500-3599) - Export:Excel       error codes
    ///    -[3600-3630) - Data               error codes
    ///    -[3630-3799) - XEMO SampleChanger error codes
    ///    -[3800-3899) - UI:WinForms        error codes
    ///    -[3900-3999) - Scales             error codes
    /// </summary>
    public partial struct Codes
    {

        #region DataBase 
        // 3000 - 3099

        public const int ERR_DB_CON = 3000;
        public const int ERR_DB_INS = 3001;
        public const int ERR_DB_UPD = 3002;
        public const int ERR_DB_EXEC = 3003;
        public const int ERR_DB_REMOVE = 3004;
        public const int ERR_DB_PRIV = 3005;
        public const int ERR_DB_EMPTY_CS = 3006;
        public const int ERR_DB_UPD_RNG = 3007;
        public const int ERR_DB_SAVE = 3008;
        public const int ERR_DB_CLEAR = 3009;
        public const int ERR_DB_REMOVE_RNG = 3010;
        public const int ERR_DB_INS_RNG = 3011;


        #endregion

        #region Cloud Errors
        // 3100 - 3199

        public const int ERR_CLD_CON = 3100;
        public const int ERR_CLD_UPLD = 3101;
        public const int ERR_CLD_TKN = 3102;
        public const int ERR_CLD_FND = 3103;
        public const int ERR_CLD_DWLD = 3104;
        public const int ERR_CLD_TRGT_NFND = 3105;
        public const int ERR_CLD_HC_NULL = 3106;
        public const int ERR_CLD_UPL_FILE_NFND = 3107;
        public const int ERR_CLD_FL_SHRNG_FNFND = 3108;
        public const int ERR_CLD_UPL_UNREG = 3109;
        public const int ERR_CLD_GEN_TKN = 3110;
        public const int ERR_CLD_UPL_FILE = 3111;
        public const int ERR_CLD_CON_UNREG = 3112;
        public const int ERR_CLD_RMV_FILE_UNREG = 3113;
        public const int ERR_CLD_UPL_FILE_UNREG = 3114;
        public const int ERR_CLD_FL_SHRNG_UNREG = 3115;
        public const int ERR_CLD_IS_EXST_UNREG = 3116;
        public const int ERR_CLD_CRT_DIR_UNREG = 3117;
        public const int ERR_CLD_BAD_RSPN_UNREG = 3118;
        public const int ERR_CLD_SEND_REQ_UNREG = 3119;
        public const int ERR_CLD_DWNLD_FILE_UNREG = 3120;
        public const int ERR_CLD_DWLD_UNREG = 3121;
        public const int ERR_CLD_DWLD_FNFND = 3122;
        public const int ERR_CLD_DEL_FIL_DIR_UNREG = 3123;

        #endregion

        #region Detector Errors
        // 3200 - 3299

        public const int ERR_DET_NAME_EXSTS = 3200; //$"Detector with name '{name}' wasn't find in the MID wizard 
        public const int ERR_DET_NAME_EXSTS_UNREG = 3201;
        public const int ERR_DET_CTOR_UNREG = 3202;
        public const int ERR_DET_RST_UNREG = 3203;
        public const int ERR_DET_AVAIL_UNREG = 3204;
        public const int ERR_DET_INTR_CONN_UNREG = 3205;
        public const int ERR_DET_CONN_UNREG = 3206;
        public const int ERR_DET_DCON_UNREG = 3207;
        public const int ERR_DET_ACQ_START_UNREG = 3208;
        public const int ERR_DET_ACQ_STOP_UNREG = 3209;
        public const int ERR_DET_MEAS_ZERO_DUR = 3210;
        public const int ERR_DET_ACQ_PAUSE_UNREG = 3211;
        public const int ERR_DET_ACQ_CLR_UNREG = 3212;
        public const int ERR_DET_ACQ_HRDW = 3213;
        public const int ERR_DET_MSG_UNREG = 3214;
        public const int ERR_DET_EFF_H_FILE_NF = 3215;
        public const int ERR_DET_EFF_H_FILE_UNREG = 3216;
        public const int ERR_DET_EFF_ENG_FILE_NF = 3217;
        public const int ERR_DET_EFF_ENG_FILE_UNREG = 3218;
        public const int ERR_DET_CHCK_MEAS_UNREG = 3219;
        public const int ERR_DET_FSAVE_DCON = 3220;
        public const int ERR_DET_MEAS_WRONG_DET = 3221;
        public const int ERR_DET_MEAS_EMPTY_FLDS = 3222;
        public const int ERR_DET_MEAS_EMPTY = 3223;
        public const int ERR_DET_CHCK_IRR_UNREG = 3224;
        public const int ERR_DET_IRR_EMPTY_FLDS = 3225;
        public const int ERR_DET_IRR_EMPTY = 3226;
        public const int ERR_DET_FILE_SAVE_UNREG = 3227;
        public const int ERR_DET_FILE_NOT_SAVED = 3228;
        public const int ERR_DET_GET_DEADT_UNREG = 3230;
        public const int ERR_DET_LOAD_SMPL_INFO_UNREG = 3231;
        public const int ERR_DET_NAME_N_EXST = 3232;
        public const int ERR_DET_NOT_READY = 3235;
        public const int ERR_DET_GET_PARAM_UNREG = 3236;
        public const int ERR_DET_SET_NULL_PARAM = 3237;
        public const int ERR_DET_SET_PARAM_UNREG = 3238;
        public const int ERR_DET_SMPL_CNVTR = 3240;
        public const int ERR_DET_EFF_DIR_EMPTY = 3241;
        public const int ERR_DET_CLR_SMPL_INFO_UNREG = 3242;
        public const int ERR_DET_FSAVE_NOT_EFF_LOAD = 3243;
        

        #endregion

        #region Logger Errors
        // 3300 - 3399
        public const int ERR_REP_SEND_MAIL_UNREG = 3300;


        #endregion

        #region Settings Errors
        // 3400 - 3499
        public const int ERR_SET_MSG_ARR_NULL            = 3400;
        public const int ERR_SET_LBL_ARR_NULL            = 3401;
        public const int ERR_SET_LBL_FILE_NOT_EXST       = 3402;
        public const int ERR_SET_CODE_FILE_NOT_EXST      = 3403;
        public const int ERR_SET_GET_MSG_UNREG           = 3404;
        public const int ERR_SET_GET_LBL_UNREG           = 3405;
        public const int ERR_SET_CODE_FILE_UNREG         = 3406;
        public const int ERR_SET_LBL_FILE_UNREG          = 3407;
        public const int ERR_SET_SAVE_UNREG              = 3408;
        public const int ERR_SET_SAVE_EMPT_ASMBL         = 3409;
        public const int ERR_SET_RST_UNREG               = 3410;
        public const int ERR_SET_RST_EMPT_ASMBL          = 3411;
        public const int ERR_SET_LOAD_UNREG              = 3412;
        public const int ERR_SET_LOAD_EMPT_ASMBL         = 3413;
        public const int ERR_SET_GET_FILE_SET_EMPT_ASMBL = 3414;
        public const int ERR_SET_SET_ASMBL_NAME_EMPTY    = 3415;
        public const int ERR_LBL_UNREG                   = 3416;
        public const int ERR_LBL_FNF                     = 3417;

        #endregion



        #region Export Errors
        // 3500 - 3599

        #region Excel Errors

        #endregion

        #region Google sheets Errors

        #endregion

        #region CSV Errors

        #endregion

        #endregion

        #region Data Errors
        // 3600 - 3630

        public const int ERR_DATA_GET_SSID   = 3600;
        public const int ERR_DATA_GET_SRMSID = 3601;

        #endregion

        #region XEMO SampleChanger Errors
        // 3630 - 3799

        public const int ERR_XM_COM_UNREG                        = 3630;
        public const int ERR_XM_INI_UNREG                        = 3631;
        public const int ERR_XM_CON_UNREG                        = 3632;
        public const int ERR_XM_INI_AX_UNREG                     = 3633;
        public const int ERR_XM_WRONG_POS                        = 3754;
        public const int ERR_XM_TRCK_POS_UNREG                   = 3755;
        public const int ERR_XM_TRCK_POS                         = 3756;
        public const int ERR_XM_HOME_UNREG                       = 3757;
        public const int ERR_XM_PUT_ABV_DET_ASYNC_UNREG          = 3758;
        public const int ERR_XM_PUT_ABV_DET_UNREG                = 3759;
        public const int ERR_XM_GET_PIN_POS_UNREG                = 3760;
        public const int ERR_XM_MOVE_UNREG                       = 3761;
        public const int ERR_XM_INI_DEV_NF                       = 3762;
        public const int ERR_XM_ERR_HNDL_UNREG                   = 3763;
        public const int ERR_XM_GRPC_CLNT_UNREG                  = 3764;
        public const int ERR_XM_GRPC_SERV_DevIsReady_UNREG       = 3765;
        public const int ERR_XM_GRPC_SERV_SmplHasTaken_UNREG     = 3766;
        public const int ERR_XM_GRPC_SERV_Smpl_Abv_Det_UNREG     = 3767;
        public const int ERR_XM_GRPC_SERV_SampleInCell_UNREG     = 3768;
        public const int ERR_XM_GRPC_SERV_DevErr_UNREG           = 3769;
        public const int ERR_XM_GRPC_IS_MEAS_HAS_DONE_UNREG      = 3770;
        public const int ERR_XM_GRPC_LAST_MEAS_HAS_DONE_UNREG    = 3771;
        public const int ERR_XM_GRPC_CLNT_DEV_NOT_ANS            = 3772;
        



        #region MotionBasic runtime errors 
        ///<summary>
        /// Unknown command or P-code Critical error
        ///</summary>
        public const int ERR_XM_1 = 3634;
        ///<summary>
        /// Exceeds data range Critical error
        ///</summary>
        public const int ERR_XM_2 = 3635;
        ///<summary>
        /// Stack overflow Critical error
        ///</summary>
        public const int ERR_XM_3 = 3636;
        ///<summary>
        /// Unknown library function Critical error
        ///</summary>
        public const int ERR_XM_4 = 3637;
        ///<summary>
        /// Unknown operator Critical error
        ///</summary>
        public const int ERR_XM_5 = 3638;
        ///<summary>
        /// Overflow during type conversion 
        ///</summary>
        public const int ERR_XM_6 = 3639;
        ///<summary>
        /// P-Code not implemented Critical error
        ///</summary>
        public const int ERR_XM_7 = 3640;
        ///<summary>
        /// Array dimension conflict 
        ///</summary>
        public const int ERR_XM_8 = 3641;
        ///<summary>
        /// Exceeds array range 
        ///</summary>
        public const int ERR_XM_9 = 3642;
        ///<summary>
        /// Library function not implemented Critical error
        ///</summary>
        public const int ERR_XM_10 = 3643;
        ///<summary>
        /// Exceeds maximum string length 
        ///</summary>
        public const int ERR_XM_11 = 3644;
        ///<summary>
        /// Not enough memory for data range Critical error
        ///</summary>
        public const int ERR_XM_12 = 3645;
        ///<summary>
        /// Not enough memory for stack area Critical error
        ///</summary>
        public const int ERR_XM_13 = 3646;
        ///<summary>
        /// Not enough memory for P-code Critical error
        ///</summary>
        public const int ERR_XM_14 = 3647;
        ///<summary>
        /// online fifo overflow 
        ///</summary>
        public const int ERR_XM_15 = 3648;
        ///<summary>
        /// Timeout while burning flash Critical error
        ///</summary>
        public const int ERR_XM_16 = 3649;
        ///<summary>
        /// Erase error for flash sector Critical error
        ///</summary>
        public const int ERR_XM_17 = 3650;
        ///<summary>
        /// Read-only for flash active Critical error
        ///</summary>
        public const int ERR_XM_18 = 3651;
        ///<summary>
        /// Check-sum error in P-code Critical error
        ///</summary>
        public const int ERR_XM_19 = 3652;
        ///<summary>
        /// Invalid signature in P-code Critical error
        ///</summary>
        public const int ERR_XM_20 = 3653;
        ///<summary>
        /// Not enough memory for EEprom area Critical error
        ///</summary>
        public const int ERR_XM_21 = 3654;
        ///<summary>
        /// Read-only for EEprom active 
        ///</summary>
        public const int ERR_XM_22 = 3655;
        ///<summary>
        /// Timeout while burning EEprom 
        ///</summary>
        public const int ERR_XM_23 = 3656;
        ///<summary>
        /// Invalid axis number 
        ///</summary>
        public const int ERR_XM_24 = 3657;
        ///<summary>
        /// Invalid parameter number 
        ///</summary>
        public const int ERR_XM_25 = 3658;
        ///<summary>
        /// Invalid Setfifo command 
        ///</summary>
        public const int ERR_XM_26 = 3659;
        ///<summary>
        /// Invalid SysControl command 
        ///</summary>
        public const int ERR_XM_27 = 3660;
        ///<summary>
        /// Invalid I/O address 
        ///</summary>
        public const int ERR_XM_28 = 3661;
        ///<summary>
        /// Assignment to a constant not possible 
        ///</summary>
        public const int ERR_XM_29 = 3662;
        ///<summary>
        /// Task already active 
        ///</summary>
        public const int ERR_XM_30 = 3663;
        ///<summary>
        /// Invalid signature in EEprom Critical error
        ///</summary>
        public const int ERR_XM_31 = 3664;
        ///<summary>
        /// Defect memory allocation in EEprom Critical error
        ///</summary>
        public const int ERR_XM_32 = 3665;
        ///<summary>
        /// Check-sum error in EEprom Critical error
        ///</summary>
        public const int ERR_XM_33 = 3666;
        ///<summary>
        /// Incompatible P-code Critical error
        ///</summary>
        public const int ERR_XM_35 = 3668;
        ///<summary>
        /// Assignment to identical string not permitted 
        ///</summary>
        public const int ERR_XM_36 = 3669;
        ///<summary>
        /// Limit switch reached 
        ///</summary>
        public const int ERR_XM_37 = 3670;
        ///<summary>
        /// Not enough memory for download 
        ///</summary>
        public const int ERR_XM_38 = 3671;
        ///<summary>
        /// Invalid parameter value 
        ///</summary>
        public const int ERR_XM_39 = 3672;
        ///<summary>
        /// Function not configured 
        ///</summary>
        public const int ERR_XM_40 = 3673;
        ///<summary>
        /// Command only permitted during axis standstill 
        ///</summary>
        public const int ERR_XM_41 = 3674;
        ///<summary>
        /// Circle commands require at least 2D 
        ///</summary>
        public const int ERR_XM_42 = 3675;
        ///<summary>
        /// No program loaded 
        ///</summary>
        public const int ERR_XM_43 = 3676;
        ///<summary>
        /// Unknown subprocedure number 
        ///</summary>
        public const int ERR_XM_45 = 3678;
        ///<summary>
        /// target position outside the software limit 
        ///</summary>
        public const int ERR_XM_47 = 3680;
        ///<summary>
        /// Parameter value too large 
        ///</summary>
        public const int ERR_XM_48 = 3681;
        ///<summary>
        /// Not enabled 
        ///</summary>
        public const int ERR_XM_49 = 3682;
        ///<summary>
        /// Software limit reached 
        ///</summary>
        public const int ERR_XM_50 = 3683;
        ///<summary>
        /// Parameter can only be read 
        ///</summary>
        public const int ERR_XM_51 = 3684;
        ///<summary>
        /// Contouring error in the electronic transmission 
        ///</summary>
        public const int ERR_XM_52 = 3685;
        ///<summary>
        /// Ventilator overload (overcurrent) 
        ///</summary>
        public const int ERR_XM_53 = 3686;
        ///<summary>
        /// Excess temperature in the device 
        ///</summary>
        public const int ERR_XM_54 = 3687;
        ///<summary>
        /// Error in the index monitoring at the encoder input 
        ///</summary>
        public const int ERR_XM_55 = 3688;
        ///<summary>
        /// Electric error in the encoder signal 
        ///</summary>
        public const int ERR_XM_56 = 3689;
        ///<summary>
        /// Electronic transmission: synchronic position missed 
        ///</summary>
        public const int ERR_XM_57 = 3690;
        ///<summary>
        /// Function not available in this stage of expansion 
        ///</summary>
        public const int ERR_XM_58 = 3691;
        ///<summary>
        /// Function (Gantry operation, step monitoring) not available with this hardware 
        ///</summary>
        public const int ERR_XM_59 = 3692;
        ///<summary>
        /// Velocity setting too large 
        ///</summary>
        public const int ERR_XM_60 = 3693;
        ///<summary>
        /// Acceleration setting too large 
        ///</summary>
        public const int ERR_XM_61 = 3694;
        ///<summary>
        /// Circle radius too large 
        ///</summary>
        public const int ERR_XM_62 = 3695;
        ///<summary>
        /// Negative parameter value not permitted 
        ///</summary>
        public const int ERR_XM_63 = 3696;
        ///<summary>
        /// Error during reference run 
        ///</summary>
        public const int ERR_XM_64 = 3697;
        ///<summary>
        /// Error on a power card 
        ///</summary>
        public const int ERR_XM_65 = 3698;
        ///<summary>
        /// Encoder input overflow 
        ///</summary>
        public const int ERR_XM_66 = 3699;
        ///<summary>
        /// Not permitted with activated electronic transmission 
        ///</summary>
        public const int ERR_XM_67 = 3700;
        ///<summary>
        /// Parameter value must be unequal to zero 
        ///</summary>
        public const int ERR_XM_68 = 3701;
        ///<summary>
        /// CAN communication error 
        ///</summary>
        public const int ERR_XM_69 = 3702;
        ///<summary>
        /// Check-sum error in online command 
        ///</summary>
        public const int ERR_XM_70 = 3703;
        ///<summary>
        /// Over- or undervoltage in the 12 volt vehicle voltage 
        ///</summary>
        public const int ERR_XM_71 = 3704;
        ///<summary>
        /// Over- or undervoltage in the 24 volt feed in 
        ///</summary>
        public const int ERR_XM_72 = 3705;
        ///<summary>
        /// Over- or undervoltage in the motor's intermediate circuit voltage 
        ///</summary>
        public const int ERR_XM_73 = 3706;
        ///<summary>
        /// Short circuit in a digital output 
        ///</summary>
        public const int ERR_XM_74 = 3707;
        ///<summary>
        /// Serial interface: format error 
        ///</summary>
        public const int ERR_XM_75 = 3708;
        ///<summary>
        /// Serial interface: overflow 
        ///</summary>
        public const int ERR_XM_76 = 3709;
        ///<summary>
        /// Setup parameter is write-protected 
        ///</summary>
        public const int ERR_XM_77 = 3710;
        ///<summary>
        /// Error during writing of the setup parameter 
        ///</summary>
        public const int ERR_XM_78 = 3711;
        ///<summary>
        /// Setup parameter check-sum error 
        ///</summary>
        public const int ERR_XM_79 = 3712;
        ///<summary>
        /// Communications error in the second CAN channel 
        ///</summary>
        public const int ERR_XM_80 = 3713;
        ///<summary>
        /// axis is not available 
        ///</summary>
        public const int ERR_XM_81 = 3714;
        ///<summary>
        /// Too many I/O ports 
        ///</summary>
        public const int ERR_XM_82 = 3715;
        ///<summary>
        /// CANopen guarding error 
        ///</summary>
        public const int ERR_XM_83 = 3716;
        ///<summary>
        /// axis regulator cannot be switched on 
        ///</summary>
        public const int ERR_XM_84 = 3717;
        ///<summary>
        /// axis regulator has turned itself off 
        ///</summary>
        public const int ERR_XM_85 = 3718;
        ///<summary>
        /// No axis is registered 
        ///</summary>
        public const int ERR_XM_86 = 3719;
        ///<summary>
        /// Reference run method not implemented 
        ///</summary>
        public const int ERR_XM_87 = 3720;
        ///<summary>
        /// H-portal transformation: axes not within a coordinate system 
        ///</summary>
        public const int ERR_XM_88 = 3721;
        ///<summary>
        /// Not permitted with a switched-on gantry axis 
        ///</summary>
        public const int ERR_XM_89 = 3722;
        ///<summary>
        /// Error in axis regulator; cannot be read out 
        ///</summary>
        public const int ERR_XM_99 = 3732;
        ///<summary>
        /// Unknown error in the axis regulator 
        ///</summary>
        public const int ERR_XM_100 = 3733;
        ///<summary>
        /// Software reset in the axis regulator 
        ///</summary>
        public const int ERR_XM_101 = 3734;
        ///<summary>
        /// Loss of synchronization in the axis regulator 
        ///</summary>
        public const int ERR_XM_102 = 3735;
        ///<summary>
        /// Motor-encoder antivalence error 
        ///</summary>
        public const int ERR_XM_103 = 3736;
        ///<summary>
        /// Motor-encoder counter error 
        ///</summary>
        public const int ERR_XM_104 = 3737;
        ///<summary>
        /// Master-encoder counter error 
        ///</summary>
        public const int ERR_XM_105 = 3738;
        ///<summary>
        /// Excessive temperature in the axis regulator 
        ///</summary>
        public const int ERR_XM_106 = 3739;
        ///<summary>
        /// Undervoltage in the logic-unit supply in the axis regulator 
        ///</summary>
        public const int ERR_XM_107 = 3740;
        ///<summary>
        /// Intermediate circuit overvoltage in the axis regulator 
        ///</summary>
        public const int ERR_XM_108 = 3741;
        ///<summary>
        /// Intermediate circuit undervoltage in the axis regulator 
        ///</summary>
        public const int ERR_XM_109 = 3742;
        ///<summary>
        /// Short circuit phase A in the axis regulator 
        ///</summary>
        public const int ERR_XM_110 = 3743;
        ///<summary>
        /// Short circuit phase B in the axis regulator 
        ///</summary>
        public const int ERR_XM_111 = 3744;
        ///<summary>
        /// Short circuit digital output in the axis regulator 
        ///</summary>
        public const int ERR_XM_112 = 3745;
        ///<summary>
        /// Axis regulator not enabled 
        ///</summary>
        public const int ERR_XM_113 = 3746;
        ///<summary>
        /// Contouring error too high 
        ///</summary>
        public const int ERR_XM_114 = 3747;
        ///<summary>
        /// Velocity too high 
        ///</summary>
        public const int ERR_XM_115 = 3748;
        ///<summary>
        /// Communication not found 
        ///</summary>
        public const int ERR_XM_116 = 3749;
        ///<summary>
        /// CAN communication interrupted 
        ///</summary>
        public const int ERR_XM_117 = 3750;
        ///<summary>
        /// i??*t ??� monitoring activated 
        ///</summary>
        public const int ERR_XM_118 = 3751;
        ///<summary>
        /// Negative hardware end position activated 
        ///</summary>
        public const int ERR_XM_119 = 3752;
        ///<summary>
        /// Positive hardware end position activated 
        ///</summary>
        public const int ERR_XM_120 = 3753;

        #endregion

        #endregion

        #region UI:Winforms Errors

        public const int ERR_UI_WF_RDGV_Null_Data                  = 3800;
        public const int ERR_UI_WF_LOGIN_UNREG                     = 3801;
        public const int ERR_UI_WF_LOGIN_ENTER_WRONG_PIN_OR_USER   = 3802;
        public const int ERR_UI_WF_LOGIN_ENTER_WRONG_LOGIN_OR_PASS = 3803;
        public const int ERR_UI_WF_LOGIN_ENTER_UNREG               = 3804;
        public const int ERR_UI_WF_LOGIN_ENTER_PIN_NOT_FOUND       = 3805;
        public const int ERR_UI_WF_LOGIN_APP_ALREADY_OPENED        = 3806;
        public const int ERR_UI_WF_CRT_PIN_UNREG                   = 3807;
        public const int ERR_UI_WF_CRT_PIN_FIELD_UNREG             = 3808;
        public const int ERR_UI_WF_CHCK_PSWD_UNREG                 = 3809;
        public const int ERR_UI_WF_CHCK_PIN_UNREG                  = 3810;
        public const int ERR_UI_WF_ADD_PIN_UNREG                   = 3811;
        public const int ERR_UI_WF_CRT_PIN_WRONG_PASS_OR_USER      = 3812;
        public const int ERR_UI_WF_TIME_CHNG_UNREG                 = 3813;
        public const int ERR_UI_WF_DET_SAVE_UNREG                  = 3814;
        public const int ERR_UI_WF_DET_STOP_UNREG                  = 3815;
        public const int ERR_UI_WF_DET_HGHT_CHNG_UNREG             = 3816;
        public const int ERR_UI_WF_DET_STAT_CHNG_UNREG             = 3817;
        public const int ERR_UI_WF_DCP_PAUSE_UNREG                 = 3818;
        public const int ERR_UI_WF_DCP_CLR_UNREG                   = 3819;
        public const int ERR_UI_WF_DCP_REFR_TIME_UNREG             = 3820;
        public const int ERR_UI_WF_DCP_INIT_UNREG                  = 3821;
        public const int ERR_UI_WF_ACQ_DONE_UNREG                  = 3822;
        public const int ERR_UI_WF_ACQ_DONE_DB                     = 3823;
        public const int ERR_UI_WF_ACQ_START                       = 3824;
        public const int ERR_UI_WF_ACQ_STOP                        = 3825;
        public const int ERR_UI_WF_ACQ_CLEAR                       = 3826;
        public const int ERR_UI_WF_ACQ_PAUSE                       = 3827;
        public const int ERR_UI_WF_INI_DET                         = 3828;
        public const int ERR_UI_WF_INI_MEAS_CONTR                  = 3829;
        public const int ERR_UI_WF_INI_MEAS_REGS                   = 3830;
        public const int ERR_UI_WF_FILL_MEAS_REGS                  = 3831;
        public const int ERR_UI_WF_FILL_SEL_MEAS                   = 3832;
        public const int ERR_UI_WF_HIDE_MEAS_COLS                  = 3833;
        public const int ERR_UI_WF_INI_IRR_CONTR                   = 3834;
        public const int ERR_UI_WF_INI_IRR_REGS                    = 3835;
        public const int ERR_UI_WF_FILL_IRR_REGS                   = 3836;
        public const int ERR_UI_WF_FILL_IRR_MEAS                   = 3837;
        public const int ERR_UI_WF_HIDE_IRR_COLS                   = 3838;
        public const int ERR_UI_WF_FILL_SEL_IRR                    = 3839;
        public const int ERR_UI_WF_GEN_QUE_FORM                    = 3840;
        public const int ERR_UI_WF_INI_MEAS_PARAMS                 = 3841;
        public const int ERR_UI_WF_FILL_DUR                        = 3842;
        public const int ERR_UI_WF_UPD_CUR_MEAS_REG                = 3843;
        public const int ERR_UI_WF_CRT_MEAS_REG                    = 3844;
        public const int ERR_UI_WF_HIDE_MRDGV_COLS                 = 3845;
        public const int ERR_UI_WF_MAIN_FORM_DISP                  = 3846;
        public const int ERR_UI_WF_INIT_CUR_REG                    = 3847;
        public const int ERR_UI_WF_CLR_CUR_REG                     = 3848;
        public const int ERR_UI_WF_ADD_REC                         = 3849;
        public const int ERR_UI_WF_INI_MENU                        = 3850;
        public const int ERR_UI_WF_INI_STAT                        = 3851;
        public const int ERR_UI_WF_FILL_DB_VAL                     = 3852;
        public const int ERR_UI_WF_DCP_CTOR_UNREG                  = 3853;
        public const int ERR_UI_WF_REM_REC                         = 3854;
        public const int ERR_UI_WF_ACQ_START_IRR_NF                = 3855;
        public const int ERR_UI_WF_SAVE_CHNG                       = 3856;
        public const int ERR_UI_WF_ADD_MEAS_REC                    = 3857;
        public const int ERR_UI_WF_TAB_HIDE_COLS_UNREG             = 3858;
        public const int ERR_UI_WF_FILL_SEL_TBLS_UNREG             = 3859;
        public const int ERR_UI_WF_FILL_TBL1_UNREG                 = 3860;
        public const int ERR_UI_WF_INI_TAB_TABLS_UNREG             = 3861;
        public const int ERR_UI_WF_FILL_SMPL_REGS                  = 3862;
        public const int ERR_UI_WF_INI_SMPL_REGS                   = 3863;
        public const int ERR_UI_WF_FILL_SEL_MON_UNREG              = 3864;
        public const int ERR_UI_WF_FILL_MON_REGS_UNREG             = 3865;
        public const int ERR_UI_WF_INI_MON_REGS_UNREG              = 3866;
        public const int ERR_UI_WF_FILL_SEL_STD_UNREG              = 3867;
        public const int ERR_UI_WF_FILL_STD_REGS_UNREG             = 3868;
        public const int ERR_UI_WF_INI_STD_REGS_UNREG              = 3869;
        public const int ERR_UI_WF_FILL_SEL_SMP_UNREG              = 3870;
        public const int ERR_UI_WF_FILL_SMP_REGS_UNREG             = 3871;
        public const int ERR_UI_WF_INI_SMP_REGS_UNREG              = 3872;
        public const int ERR_UI_WF_INI_TAB_1_REGS_UNREG            = 3873;
        public const int ERR_UI_WF_FILL_1TABL_TAB_UNREG            = 3874;
        public const int ERR_UI_WF_FILL_2TABL_TAB_UNREG            = 3875;




        #endregion

        #region Scales Errors

        public const int ERR_SCL_EMPT_COM = 3900;
        public const int ERR_SCL_GET_WGHT = 3901;
        public const int ERR_SCL_UNREG    = 3902;

        #endregion


    } // public partial struct Codes
}     // namespace Regata.Core
