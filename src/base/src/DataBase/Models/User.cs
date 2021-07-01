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


using System;
using System.ComponentModel.DataAnnotations;

namespace Regata.Core.DataBase.Models
{
    public class User
    {
        public int      Id         { get; set; }
        public string   LastName   { get; set; }
        public string   FirstName  { get; set; }
        public string   MiddleName { get; set; }
        public DateTime BirthDate  { get; set; }
        public string   Position   { get; set; }
        public string   WPhone     { get; set; }
        public string   PPhone     { get; set; }
        public string   Email      { get; set; }
        public string   Role       { get; set; }
        public bool     Former     { get; set; }
        public string   OldId      { get; set; }
        public string   Login      { get; set; }
    }
}
