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

namespace Regata.Core.DataBase.Models
{

    public class Position
    {
        public string Name         { get; set; }
        public int    SerialNumber { get; set; }
        public string Detector     { get; set; }
        public int    X            { get; set; }
        public int    Y            { get; set; }
        public int?   C            { get; set; }

        public override string ToString() => $"X = {X}, Y = {Y}, C = {C}";

    }
}
