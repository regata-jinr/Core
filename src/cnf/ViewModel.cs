/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;

namespace GSI.Core
{
    public class ViewModel
    {
        public string   File          { get; set; }
        public string   Id            { get; set; }
        public string   Title         { get; set; }
        public string   CollectorName { get; set; }
        public string   Type          { get; set; }
        public string   AcqMod       { get; set; }
        public float    Quantity      { get; set; }
        public float    Uncertainty   { get; set; }
        public string   Units         { get; set; }
        public float    Geometry      { get; set; }
        public float    Duration      { get; set; }
        public float    DeadTime      { get; set; }
        public string   BuildUpType   { get; set; }
        public DateTime AcqStartDate  { get; set; }
        public DateTime IrrBeginDate  { get; set; }
        public DateTime IrrEndDate    { get; set; }
        public string   Description   { get; set; }
        public bool     ReadSuccess   { get; set; }
        public string   ErrorMessage  { get; set; }
    }
}
