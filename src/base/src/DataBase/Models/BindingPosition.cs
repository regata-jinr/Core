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
    public class BindingPosition
    {
        public int      Id                     { get; set; }
        public int      SerialNumber           { get; set; }
        public string   Detector               { get; set; }
        public int      Axis                   { get; set; }
        public int      AboveDetector2p5       { get; set; }
        public int      AboveDetector5         { get; set; }
        public int      AboveDetector10        { get; set; }
        public int      AboveDetector20        { get; set; }
        public int      NearDiskCellExternal   { get; set; }
        public int      NearDiskCellInternal   { get; set; }
        public int      AboveDiskCellExternal  { get; set; }
        public int      AboveDiskCellInternal  { get; set; }
        public int      StepToNextCellExternal { get; set; }
        public int      StepToNextCellInternal { get; set; }

    }
}
