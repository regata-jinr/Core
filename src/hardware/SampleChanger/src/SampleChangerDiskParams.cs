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

namespace Regata.Core.Hardware
{
    public struct DiskParams
    {
        public int    CellNum;
        public int    Gap;
        public string DiskName;
        public int    InitCellNum;
        public string PairedDetector;

        public DiskParams(string det, int cellNum)
        {

            PairedDetector = det;

            DiskName = cellNum switch
            {
                > 30 => "Internal",
                <= 30 => "External"
            };

            this.CellNum = cellNum switch
            {
                > 45 => 1,
                <= 0 => 1,
                _ => cellNum
            };

            Gap = cellNum switch
            {
                > 30 => 2400,
                <= 30 => 1200
            };

            if (PairedDetector == "D4")
            {
                Gap = cellNum switch
                {
                    > 30 => 3600,
                    <= 30 => 1800
                };
            }

            InitCellNum = cellNum switch
            {
                > 30 => 31,
                <= 30 => 1
            };
        }

    } // public struct DiskParams
}     // namespace Regata.Core.Hardware

