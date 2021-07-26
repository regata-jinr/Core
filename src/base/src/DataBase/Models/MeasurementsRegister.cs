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

using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    [Table("MeasurementsRegister")]
    public class MeasurementsRegister : INotifyPropertyChanged
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int       Id               { get; set; }
        public DateTime? IrradiationDate  { get; set; }
        public string    Name             { get; set; }
        public int?      LoadNumber       { get; set; }

        private int _type;
        public int       Type             
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? DateTimeStart    { get; set; }
        public DateTime? DateTimeFinish   { get; set; }
        public int       SamplesCnt       { get; set; }
        public string    Detectors        { get; set; }
        public int?      Assistant        { get; set; }
        public string    Note             { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

