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
        private Axes _activeAxis;

        /// <summary>
        /// The flag shows that sample changer has sample on caret.
        /// </summary>
        public bool IsSampleCaptured { get; set; }

        public string Text;
        public bool IsError => XemoDLL.ML_GetErrState() == 0;
        public int Code => IsError ? XemoDLL.ML_GetErrCode() : XemoDLL.MB_GetState();
        
        public bool NegativeSwitcherX => IsNegativeSwitcher(Axes.X);
        public bool NegativeSwitcherY => IsNegativeSwitcher(Axes.Y);

        public bool PositiveSwitcherX => IsPositiveSwitcher(Axes.X);
        public bool PositiveSwitcherY => IsPositiveSwitcher(Axes.Y);

        public bool ReferenceSwitcherX => IsReferenceSwitcher(Axes.X);
        public bool ReferenceSwitcherY => IsReferenceSwitcher(Axes.Y);

        public bool ReferenceSwitcher => NegativeSwitcherX  || NegativeSwitcherY ;
        public bool PositiveSwitcher  => PositiveSwitcherX  || PositiveSwitcherY ;
        public bool NegativeSwitcher  => ReferenceSwitcherX || ReferenceSwitcherY;


        public bool IsReferenceSwitcher(Axes ax)
        {
            return XemoDLL.MB_IoGet((short)ax, 0, 2, XemoConst.InPolarity) != 0;
        }

        public bool IsPositiveSwitcher(Axes ax)
        {
            return XemoDLL.MB_IoGet((short)ax, 0, 1, XemoConst.InPolarity) != 0;
        }

        public bool IsNegativeSwitcher(Axes ax)
        {
            return XemoDLL.MB_IoGet((short)ax, 0, 0, XemoConst.InPolarity) != 0;
        }

        public const int MaxX = 77400;
        public const int MaxY = 37300;
        public const int MaxC = 0;

        public readonly Position HomePosition = new Position(MaxX, MaxY, MaxC);

        public SampleChangerSettings Settings { get; set; }

        /// <summary>
        /// Before an axis in a machine can be used, the following preparatory steps must 
        /// be taken.On the one hand, this applies to the electric connections, on the
        /// other it is the initialization of all parameters relevant for the axes. Not until this
        /// has all been correctly prepared can an axis fulfil its actual assignment. 
        /// 
        /// The corresponding references for the electric connections and the procedure
        /// are described in the technical documentation of the respective devices (device
        /// manual) and in the installation manual and can be referred to there.In this
        /// chapter, the complete programming of all axis parameters and the interrelattions between these are dealt with. 
        /// 
        /// The important parameters comprise: 
        /// • Current setting 
        /// • Step resolution 
        /// • Scaling of the axes 
        /// • Running velocity 
        /// • Acceleration ramps 
        /// • Running velocity for reference runs 
        /// • Machine zero point
        /// • Polarity of the end / reference switches
        /// • Polarity of the digital inputs and outputs
        /// These parameters are prescribed through the prevailing kinematics and their
        /// technical characteristics.Accordingly, the programmer must know and consider all of this data.
        /// </summary>
        public class AxesParams
        {
            /// <summary>
            /// The stepping-motor power stages of the Xemo Compact Controller 
            /// function as current regulators.With MotionBasic 5, the desired motor
            /// current can be programmed between 0 % and 100% of the maximum
            /// output current of the power stage.Initially, the current is set at 0% when
            /// the controller is powered up. 
            ///  
            /// **As a rule, the power current tolerance is inscribed on the motors as “rated current”.**
            /// 
            /// This indicates the rated current per coil.If the coils of a stepping motor with eight conductors
            /// are wired in series, the motor current should be set at this rated current. 
            /// However, if the coils are wired in parallel, the rated current can be
            /// multiplied with a factor of 1.414 (√2) to determine the motor current
            /// setting.
            /// NOTE: If the motor current is set too high, it will result in the destruction of the
            /// motor by overheating.
            /// </summary>
            /// <seealso>625-11-09_Xemo_Equipment_Manual_Web page 30</seealso>
            public int[] MOTOR_CURRENT { get; set; } = new int[]
           {
                50,
                50,
                50,
                100,
                100
           };

            /// <summary>
            /// By setting the microstep resolution via the parameter _Micro, it is possible to vary the motor resolution. 
            /// NOTE: Any kind of division ratio, including non-periodic, is possible.
            /// NOTE: The microstep resolution may not be changed during the running time!
            /// For stepping motors: 
            /// With the current technology of the Xemo controllers, a full step can be divided into up to 
            /// 50 microsteps.
            /// With _Micro for _Iscale, the scaling of the motor steps is as follows: 
            /// _Iscale=steps/rev. = number of full steps/rev. * (50 microsteps / _Micro) 
            /// The following table shows some of the possible resolutions for a 50-pin motor with 200 full
            /// steps: 
            /// _Micro _Iscale 
            ///    1     10000  steps/revolution
            ///    2     5000   steps/revolution
            ///    5     2000   steps/revolution
            ///    10    1000   steps/revolution
            ///    50    200    steps/revolution
            /// _Iscale = steps / rev. = 200 / rev. * (50 microsteps / _Micro) 
            /// For servomotors: 
            /// _Iscale=(increments/rev.) / _Micro
            /// The following table shows some of the possible resolutions for a servomotor with 
            /// 400 inc/rev.: 
            /// _Micro _Iscale 
            ///    1     40000  inc/revolution
            ///    2     20000  inc/revolutio
            /// </summary>
            public int[] MICROSTEP_RESOLUTION { get; set; } = new int[]
           {
                1,
                1,
                1,
                1,
                1
           };

            /// <summary>
            /// Only relevant for stepping motors. This value states the reduction of the motor's current during standstill.
            /// Unit: % of the preset motor current
            /// NOTE: Fifty ms after the relevant axis has reached standstill, the current will automatically be reduced.
            /// Prior to the movement of the axis or the motor, it will be raised again.If the axis or
            /// motor stood still for longer than 50 ms, i.e.was the current reduced, the axis will remain
            /// standing for 10 ms to make certain that the renewed raise of current in the end stages is 
            /// carried out. Not until after that can the axis move again.If the current was not reduced
            /// (0% reduction), no delay will occur.
            /// The latter does not apply if the axis has already reached standstill when the current reduction is being deactivated. 
            /// Note: During µ-step operation, the current should only be reduced insofar as the holding torque
            /// can resist extrinsic forces.
            /// </summary>
            /// <seealso>625-11-09_Xemo_Equipment_Manual_Web page 31</seealso>
            public int[] MOTOR_STOP_CURRENT { get; set; } = new int[]
           {
                50,
                50,
                50,
                50,
                50
           };

            public int[] MAX_VELOCITY { get; set; } = new int[]
            {
                1000,
                1000,
                1000,
                400,
                400
            };

            public int[] RIGHT_SOFTWARE_LIMIT { get; set; } = new int[]
            {
                40000,
                80000,
                0,
                100,
                100
            };



            public int[] INC_PER_REVOLUTION { get; set; } = new int[]
            {
                10000,
                10000,
                10000,
                10000,
                10000
            };

            public float[] MM_PER_REVOLUTION { get; set; } = new float[]
            {
                8f,
                8f,
                14.4f,
                130f,
                130f
            };

            public int[] INC_MONITORING_ENCODER { get; set; } = new int[]
            {
                2000,
                -2000,
                2000,
                0,
                130
            };

            public float[] ZERO_REF_OFFSET { get; set; } = new float[]
            {
                0f,
                0f,
                0f,
                0f,
                0f
            };

            /// <summary>
            /// The parameter is bit-oriented. A "1" inverts the logical level of the corresponding input. 
            /// Example:
            /// 
            /// _InPolarity(10.0) = 1 
            /// "Polarity" of the dig.entry 10.0 will be changed to opener
            /// 
            /// _InPolarity(0.0..2) = 7
            /// "Polarity" the limit switch (inputs 0 and 1) and of the reference switch (input 2) 
            /// of motor X(port 0) will be changed to opener.The binary statement "7" 
            /// means that the last 3 bits(bits 0 to 2) are set(111).
            /// </summary>
            public short[] POLARITY_SWITCHES { get; set; } = new short[]
            {
                3,
                3,
                0,
                3,
                3
            };


            public int[] REF_VELOCITY_H1 { get; set; } = new int[]
                   {
                50,
                50,
                50,
                100,
                100
                   };

            public int[] REF_VELOCITY_H2 { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -30,
                -30
            };

            public int[] REF_VELOCITY_H3 { get; set; } = new int[]
            {
                100,
                100,
                100,
                200,
                200
            };

            public float[] ACCELERATION_FACTOR { get; set; } = new float[]
            {
                10f,
                10f,
                1f,
                10f,
                10f
            };

            public float[] DECELERATION_FACTOR { get; set; } = new float[]
            {
                10f,
                10f,
                1f,
                10f,
                10f
            };

            /// <summary>
            /// This value determines the start velocity to which a motor can be accelerated starting from 
            /// its standstill or the stop velocity from which the motor is to be braked down to its standstill.
            /// </summary>
            public int[] START_STOP_FREQUENCY { get; set; } = new int[]
            {
                0,
                0,
                0,
                2,
                2
            };

            public float[] EMERGCY_DECEL_FACTOR { get; set; } = new float[]
            {
                10f,
                10f,
                10f,
                10f,
                10f
            };

            public float[] BLASH { get; set; } = new float[]
            {
                0f,
                0f,
                0f,
                0f,
                0f
            };

            public int[] POSITION_ERROR { get; set; } = new int[]
            {
                200,
                200,
                200,
                10000,
                10000
            };

            public int[] BRAKE { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

            public int[] GANTRY_AXIS { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };

            public int[] XTYPE { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };

            public int[] JERKMS { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };
        }

    } // public partial class SampleChanger
}     // namespace Regata.Core.Hardware
