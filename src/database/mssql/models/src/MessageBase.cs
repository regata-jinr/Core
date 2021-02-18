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

using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DB.MSSQL.Models
{ 
    [Table("MessageBases")]
    public class MessageBase
    {
        public ushort Code          { get; set; }
        public string Language      { get; set; }
        public string Head          { get; set; }
        public string Text          { get; set; }
    } //  public class MessageBase

    [Table("MessageDefaults")]
    public class MessageDefault
    {
        public string ExpandButtonText { get; set; }
        public string HideButtonText   { get; set; }
        public string FooterText       { get; set; }
        public string Language         { get; set; }
    } // public class MessageDefault

}     // namespace Regata.Core.DB.MSSQL.Models

