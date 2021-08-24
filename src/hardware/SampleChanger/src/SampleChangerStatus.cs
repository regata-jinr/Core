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

namespace Regata.Core.Hardware
{
    public partial class SampleChanger
    {
        public event Action<string, int> ErrorOccurred;
        public event Action<string, int> NegativeSwitcherFired;
        public event Action<string, int> PositiveSwitcherFired;
        public event Action<string, int> ReferenceSwitcherFired;

        public void GetStatus()
        {
            if (IsError)
            {
                ErrorOccurred?.Invoke(SerialNumber, Code);
            }

            if (NegativeSwitcher)
            {
                NegativeSwitcherFired?.Invoke(SerialNumber, Code);
            }

            if (PositiveSwitcher)
            {
                PositiveSwitcherFired?.Invoke(SerialNumber, Code);
            }

            if (ReferenceSwitcher)
            {
                ReferenceSwitcherFired?.Invoke(SerialNumber, Code);
            }
        }

        //public async Task GetStatusAsync()
        //{

        //}

    } // public partial class SampleChanger
}     // namespace Measurements.Core
