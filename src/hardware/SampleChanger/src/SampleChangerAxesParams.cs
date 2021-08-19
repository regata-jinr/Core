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

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {
        public const int MaxX = 77400;
        public const int MaxY = 37300;
        public const int MaxC = 0;

        public readonly Position HomePosition = new Position(MaxX, MaxY, MaxC);

        public SampleChangerSettings Settings { get; set; }



        public class AxesParams
        { 
            /// <summary>
            /// The stepping-motor power stages of the Xemo Compact Controller 
            /// function as current regulators.With MotionBasic 5, the desired motor
            /// current can be programmed between 0 % and 100% of the maximum
            /// output current of the power stage.Initially, the current is set at 0% when
            /// the controller is powered up. As a rule, the power current tolerance is 
            /// inscribed on the motors as “rated current”. This indicates the rated
            /// current per coil.If the coils of a stepping motor with eight conductors
            /// are wired in series, the motor current should be set at this rated current. 
            /// However, if the coils are wired in parallel, the rated current can be
            /// multiplied with a factor of 1.414 (√2) to determine the motor current
            /// setting.
            /// NOTE: If the motor current is set too high, it will result in the destruction of the
            /// motor by overheating.
            /// </summary>
            public int[] MOTOR_CURRENT { get; set; } = new int[]
           {
                60,
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

            public int[] MAX_VELOCITY { get; set; } = new int[]
            {
                100,
                100,
                120,
                400,
                400
            };

            public int[] TRAVEL_AXIS { get; set; } = new int[]
            {
                400,
                800,
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



            public int[] MOTOR_STOP_CURRENT { get; set; } = new int[]
            {
                70,
                70,
                70,
                70,
                70
            };

            public float[] ZERO_REF_OFFSET { get; set; } = new float[]
            {
                0f,
                0f,
                0f,
                0f,
                0f
            };

            public float[] NULL_OFFSET { get; set; } = new float[]
            {
                -373f,
                -774f,
                0f,
                0f,
                0f
            };

            public int[] REF_ORDER { get; set; } = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };

            public int[] REF_PORT { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

            public int[] REF_SWITCH { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

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

            public int[] NODE_ID { get; set; } = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };

            public int[] POSITION_ERROR { get; set; } = new int[]
            {
                200,
                200,
                200,
                10000,
                10000
            };

            public int[] POSITION_WINDOW { get; set; } = new int[]
            {
                100,
                100,
                100,
                100,
                100
            };

            public int[] POSITION_TIME { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };

            public int[] HOME_METHOD { get; set; } = new int[]
            {
                19,
                19,
                19,
                19,
                19
            };

            public int[] ENCODER_NACHBILDUNG { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

            public int[] ENCODER_RESOLUTION { get; set; } = new int[]
            {
                2000,
                2000,
                2000,
                0,
                0
            };

            public int[] MAX_ENCODER_FEHLER { get; set; } = new int[]
            {
                200,
                200,
                200,
                0,
                0
            };

            public int[] BRAKE { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

            public int[] INIT_PNOZ_OUT { get; set; } = new int[]
            {
                -1,
                -1,
                -1,
                -1,
                -1
            };

            public int[] P_FAKTOR_GESCHW { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                -1
            };

            public int[] P_FAKTOR_POSMODUS { get; set; } = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };

            public int[] KOMMUTIERUNGS_METHODE { get; set; } = new int[]
            {
                3,
                3,
                3,
                3,
                3
            };

            public int[] GANTRY_ACHSE { get; set; } = new int[]
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
}     // namespace Measurements.Core.Hardware
