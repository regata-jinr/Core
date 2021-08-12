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

using Microsoft.EntityFrameworkCore;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regata.Core
{
    public static partial class Data
    {
        public static async IAsyncEnumerable<Measurement> GetMeasurementsWithDiskPositionAsync(int loadNumber, string detName, IEnumerable<short?> conts)
        {
            using (var rc = new RegataContext())
            {
                int i = 0;
                foreach (var ir in await rc.Irradiations.AsNoTracking().Where(i => i.LoadNumber == loadNumber && conts.Contains(i.Container)).OrderBy(i => i.Container).ThenBy(i => i.Position).ToArrayAsync())
                {
                    yield return new Measurement(ir) { Detector = detName, DiskPosition = i + 1  };
                }
            }
        }

    }  //  public static class Data
}      // namespace Regata.Core
