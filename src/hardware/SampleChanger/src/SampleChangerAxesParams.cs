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
        public const int MaxY = 37300;
        public const int MaxX = 77400;

        public  int[] MAX_VELOCITY {get;set;} =  new int[]
        {
                100,
                100,
                120,
                400,
                400
        };

        public  int[] TRAVEL_AXIS {get;set;} =  new int[]
        {
                400,
                800,
                0,
                100,
                100
        };

        public  int[] MICROSTEP_DEFINER {get;set;} =  new int[]
        {
                1,
                1,
                1,
                1,
                1
        };

        public  int[] INC_PER_REVOLUTION {get;set;} =  new int[]
        {
                10000,
                10000,
                10000,
                10000,
                10000
        };

        public  float[] MM_PER_REVOLUTION {get;set;} =  new float[]
        {
                8f,
                8f,
                14.4f,
                130f,
                130f
        };

        public  int[] INC_MONITORING_ENCODER {get;set;} =  new int[]
        {
                2000,
                -2000,
                2000,
                0,
                130
        };

        public  int[] MOTOR_CURRENT {get;set;} =  new int[]
        {
                60,
                50,
                50,
                100,
                100
        };

        public  int[] MOTOR_STOP_CURRENT {get;set;} =  new int[]
        {
                70,
                70,
                70,
                70,
                70
        };

        public  float[] ZERO_REF_OFFSET {get;set;} =  new float[]
        {
                0f,
                0f,
                0f,
                0f,
                0f
        };

        public  float[] NULL_OFFSET {get;set;} =  new float[]
        {
                -373f,
                -774f,
                0f,
                0f,
                0f
        };

        public  int[] REF_ORDER {get;set;} =  new int[]
        {
                1,
                2,
                3,
                4,
                5
        };

        public  int[] REF_PORT {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -1,
                -1
        };

        public  int[] REF_SWITCH {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -1,
                -1
        };

        public  short[] POLARITY_SWITCHES {get;set;} =  new short[]
        {
                3,
                3,
                0,
                3,
                3
        };


        public  int[] REF_VELOCITY_H1 {get;set;} =  new int[]
               {
                50,
                50,
                50,
                100,
                100
               };

        public  int[] REF_VELOCITY_H2 {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -30,
                -30
        };

        public  int[] REF_VELOCITY_H3 {get;set;} =  new int[]
        {
                100,
                100,
                100,
                200,
                200
        };

        public  float[] ACCELERATION_FACTOR {get;set;} =  new float[]
        {
                10f,
                10f,
                1f,
                10f,
                10f
        };

        public  float[] DECELERATION_FACTOR {get;set;} =  new float[]
        {
                10f,
                10f,
                1f,
                10f,
                10f
        };

        public  int[] START_STOP_FREQUENCY {get;set;} =  new int[]
        {
                0,
                0,
                0,
                2,
                2
        };

        public  float[] EMERGCY_DECEL_FACTOR {get;set;} =  new float[]
        {
                10f,
                10f,
                10f,
                10f,
                10f
        };

        public  float[] BLASH {get;set;} =  new float[]
        {
                0f,
                0f,
                0f,
                0f,
                0f
        };

        public  int[] NODE_ID {get;set;} =  new int[]
        {
                1,
                2,
                3,
                4,
                5
        };

        public  int[] POSITION_ERROR {get;set;} =  new int[]
        {
                200,
                200,
                200,
                10000,
                10000
        };

        public  int[] POSITION_WINDOW {get;set;} =  new int[]
        {
                100,
                100,
                100,
                100,
                100
        };

        public  int[] POSITION_TIME {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                0
        };

        public  int[] HOME_METHOD {get;set;} =  new int[]
        {
                19,
                19,
                19,
                19,
                19
        };

        public  int[] ENCODER_NACHBILDUNG {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -1,
                -1
        };

        public  int[] ENCODER_RESOLUTION {get;set;} =  new int[]
        {
                2000,
                2000,
                2000,
                0,
                0
        };

        public  int[] MAX_ENCODER_FEHLER {get;set;} =  new int[]
        {
                200,
                200,
                200,
                0,
                0
        };

        public  int[] BRAKE {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -1,
                -1
        };

        public  int[] INIT_PNOZ_OUT {get;set;} =  new int[]
        {
                -1,
                -1,
                -1,
                -1,
                -1
        };

        public  int[] P_FAKTOR_GESCHW {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                -1
        };

        public  int[] P_FAKTOR_POSMODUS {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                0
        };

        public  int[] KOMMUTIERUNGS_METHODE {get;set;} =  new int[]
        {
                3,
                3,
                3,
                3,
                3
        };

        public  int[] GANTRY_ACHSE {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                0
        };

        public  int[] XTYPE {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                0
        };

        public  int[] JERKMS {get;set;} =  new int[]
        {
                0,
                0,
                0,
                0,
                0
        };


    } // public partial class SampleChanger
}     // namespace Measurements.Core.Hardware
