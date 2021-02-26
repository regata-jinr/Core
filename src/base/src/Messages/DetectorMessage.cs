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

namespace Regata.Core.Messages
{
   public class DetectorMessage : Message
    {
        public string Name { get; set; }

        public DetectorMessage(ushort code) : base(code)
        {
            base.Head += Name;
        }
    }
}
