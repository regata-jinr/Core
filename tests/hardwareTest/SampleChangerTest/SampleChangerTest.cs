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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.Hardware;
using Regata.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Regata.Tests.Hardware.Detectors
{
    [TestClass]
    public class SampleChangerTest
    {
        [TestMethod]
        public async Task SampleChangerParallelManagmentTest()
        {
            var ct1 = new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token;
            //SampleChanger[] s = null;
            var s1 = new SampleChanger(107374);
            var s2 = new SampleChanger(107375);
            try
            {
                //s = new SampleChanger[] { new SampleChanger(107374), new SampleChanger(107375) };

                s1.ErrorOccurred += (i, j) => { Console.WriteLine($"{i}, {j}"); };
                s2.ErrorOccurred += (i, j) => { Console.WriteLine($"{i}, {j}"); };


                s1.PositionReached += async (s11) => await PositionReachedHandler(s11).ConfigureAwait(false);
                s2.PositionReached += async (s22) => await PositionReachedHandler(s22).ConfigureAwait(false);

                //var tasks = new List<Task>();
                //foreach (var x in s)
                //{
                //    SampleChanger ss = x;
                //    tasks.Add(Task.Factory.StartNew(async (xx) => await XemoCycle(xx as SampleChanger), ss));
                //}

                //await Task.WhenAll(tasks);

                await Task.WhenAll(XemoCycle(s1), XemoCycle(s2));
                //await Task.WhenAll(HomeAsync(s1), HomeAsync(s2));
                //await Task.WhenAll(MoveToPosAsync(s1), MoveToPosAsync(s2));

                Console.WriteLine("Press enter for exit...");
                Console.ReadLine();

            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("The task was cancelled");
                s1.Stop();
                s2.Stop();
            }
            finally
            {
                s1.Stop();
                s1.Disconnect();
                s2.Stop();
                s2.Disconnect();
            }
        }

        private async Task PositionReachedHandler(SampleChanger sc)
        {
            Console.WriteLine($"{sc.PairedDetector} has reached the position.Move next!");
            await MoveToPosAsync(sc);
        }

        private async Task HomeAsync(SampleChanger sc)
        {
            try
            {
                //SampleChanger.ComSelect(sc.ComPort);

                Console.WriteLine($"Xemo {sc.PairedDetector} is going to home");
                using (var ct = new CancellationTokenSource(TimeSpan.FromSeconds(45)))
                {
                    await sc?.HomeAsync(ct.Token);
                    Console.WriteLine($"{sc.PairedDetector} at home");
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Home timeout exceeds");
            }
        }

        private async Task MoveToPosAsync(SampleChanger sc, int x = 30000)
        {
            //SampleChanger.ComSelect(sc.ComPort);

            if (sc.CurrentPosition.X == 30000)
                x = 60000;
            try
            {
                using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(1)))
                {

                    Console.WriteLine($"{sc.PairedDetector} is going to position");
                    var p = new Position()
                    {
                        X = x,
                        Y = 37300
                    };
                    await sc?.MoveToPositionAsync(p, moveAlongAxisFirst: Axes.X, ct.Token);
                    Console.WriteLine($"{sc.PairedDetector} at position");

                    await Task.Delay(TimeSpan.FromSeconds(5));

                }
            }
            catch (TaskCanceledException)
            {
                // await XemoCycle(sc);
            }
        }

        private async Task XemoCycle(SampleChanger sc)
        {
            //SampleChanger.ComSelect(sc.ComPort);

            // sc.PositionReached += async (s) => await PositionReachedHandler(sc);
            await HomeAsync(sc);
            await MoveToPosAsync(sc);
        }

        private void Stop(SampleChanger sc)
        {
            sc?.Stop();
            Console.WriteLine($"{sc.PairedDetector} has stopped");
        }
    }
}
