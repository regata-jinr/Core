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

using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{ 
    [Table("UILabels")]
    public class UILabel
    {
    
        public string FormName      { get; set; }
        public string ComponentName { get; set; }
        public string RusText       { get; set; }
        public string EngText       { get; set; }
    
    } // public class UILabels
}     // namespace Regata.Core.DataBase.Models

