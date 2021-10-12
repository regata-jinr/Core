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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Regata.Core.DataBase.Models
{
    [Table("Staff")]
    public class User
    {
        [Column("personalId")]
        public int      Id         { get; set; }
        public string   LastName   { get; set; }
        public string   FirstName  { get; set; }
        public string   MiddleName { get; set; }
        public DateTime BirthDate  { get; set; }
        [Column("Post")]
        public string   Position   { get; set; }
        public string   WPhone     { get; set; }
        public string   PPhone     { get; set; }
        public string   Email      { get; set; }
        public string   Role       { get; set; }
        public bool     Former     { get; set; }
        public string   OldId      { get; set; }
        public string   Login      { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(MiddleName))
                return $"{LastName} {FirstName[0]}.";
            return $"{LastName} {FirstName[0]}.{MiddleName[0]}.";
        }

        public static User GetUserByLogin(string log)
        {
            using (var rc = new RegataContext())
            {
                return rc.Users.Where(u => u.Login == log).FirstOrDefault();

            }
        }
    }
}
