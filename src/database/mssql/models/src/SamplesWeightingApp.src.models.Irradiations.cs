/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2020, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamplesWeighting
{
    [Table("reweightInfo")]
    public class reweightInfo : IEquatable<reweightInfo>
    {
        public int    loadNumber         { get; set; }
        public string Country_Code       { get; set; }
        public string Client_Id          { get; set; }
        public string Year               { get; set; }
        public string Sample_Set_Id      { get; set; }
        public string Sample_Set_Index   { get; set; }
        public string Sample_ID          { get; set; }
        public short? Container_Number   { get; set; }
        public short? Position_Number    { get; set; }
        public float? InitWght           { get; set; }
        public float? EmptyContWght      { get; set; }
        public float? ContWithSampleWght { get; set; }
        public float? ARepackWght        { get; set; }
        public float? Diff               { get; set; }

        public bool Equals(reweightInfo rw)
        {
            if (rw.Country_Code == null) return false;

            return loadNumber         == rw.loadNumber &&
                   Country_Code       == rw.Country_Code &&
                   Client_Id          == rw.Client_Id &&
                   Year               == rw.Year &&
                   Sample_Set_Id      == rw.Sample_Set_Id &&
                   Sample_Set_Index   == rw.Sample_Set_Index &&
                   Sample_ID          == rw.Sample_ID; //&&
                   //Container_Number == rw.Container_Number &&
                   //Position_Number  == rw.Position_Number;
        }
    }

    [Table("table_LLI_Irradiation_Log")]
    public class Irradiations
    {
        public DateTime Date_Start       { get; set; }
        public int      loadNumber       { get; set; }
        public string   Country_Code     { get; set; }
        public string   Client_Id        { get; set; }
        public string   Year             { get; set; }
        public string   Sample_Set_Id    { get; set; }
        public string   Sample_Set_Index { get; set; }
        public string   Sample_ID        { get; set; }
        public short?   Container_Number { get; set; }
        public short?   Position_Number  { get; set; }
    }

    public class Register
    {
        public DateTime Date_Start { get; set; }
        public int      loadNumber { get; set; }
    }

    public class reweightInfoSet
    {
        public int    loadNumber         { get; set; }
        public string Country_Code       { get; set; }
        public string Client_Id          { get; set; }
        public string Year               { get; set; }
        //public string Sample_Set_Id      { get; set; }
        public short? Container_Number   { get; set; }
        public short? Position_Number    { get; set; }
    }
}

