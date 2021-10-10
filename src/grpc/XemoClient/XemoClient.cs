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

using Grpc.Net.Client;
using Gxemo;
using Regata.Core.Hardware;
using Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Regata.Core.GRPC.Xemo
{

    public class Client
    {
        public static IReadOnlyDictionary<PutSampleAboveDetReply.Types.Height, Heights> HH = new Dictionary<PutSampleAboveDetReply.Types.Height, Heights>()
        {
            { PutSampleAboveDetReply.Types.Height.H2P5, Heights.h2p5 },
            { PutSampleAboveDetReply.Types.Height.H5,   Heights.h5   },
            { PutSampleAboveDetReply.Types.Height.H10,  Heights.h10  },
            { PutSampleAboveDetReply.Types.Height.H20,  Heights.h20  }
        };
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GRPCXemo.GRPCXemoClient(channel);
            int devId = 0;

            try
            {
                Report.Notify(new Message(Codes.INFO_XM_GRPC_CLNT_INIT));
                // The port number(5001) must match the port of the gRPC server.
                devId = int.Parse(args[0]);

                if (devId == 0) throw new ArgumentException("Wrong device serial number!");

                using (var sc = new SampleChanger(devId))
                {
                    var ctinit = new CancellationTokenSource(TimeSpan.FromSeconds(90));

                    await sc.HomeAsync(ctinit.Token);

                    var ct = new CancellationTokenSource();
                    Report.Notify(new Message(Codes.INFO_XM_GRPC_CLNT_IN_HOME));

                    var stopMeas = false;

                    while (!stopMeas)
                    {
                        Report.Notify(new Message(Codes.INFO_XM_GRPC_CLNT_IN_RUN_CYCL));

                        var ts = await client.DeviceIsReadyAsync(new DeviceIsReadyRequest { DevId = devId, IsReady = true });

                        await client.SampleInCellAsync(new SampleInCellRequest { DevId = devId, IsInCell = false });

                        await sc.TakeSampleFromTheCellAsync((short)ts.CellNum, ct.Token);

                        var gotodeth = await client.SampleHasTakenAsync(new SampleHasTakenRequest { DevId = devId, IsTaken = true });

                        await sc.PutSampleAboveDetectorWithHeightAsync(HH[gotodeth.H], ct.Token);

                        var takeSample1 = await client.SampleAboveDetectorAsync(new SampleAboveDetectorRequest { DevId = devId, IsAbove = true });

                        var isMeasDone = false;

                        Report.Notify(new Message(Codes.INFO_XM_GRPC_CLNT_WAIT_MEAS));
                        do
                        {
                            var measStatus = await client.IsMeasurementsDoneAsync(new IsMeasurementsDoneRequest { DevId = devId });
                            isMeasDone = measStatus.IsDone;
                            await Task.Delay(TimeSpan.FromSeconds(2));
                        }
                        while (!isMeasDone);

                        Report.Notify(new Message(Codes.INFO_XM_GRPC_CLNT_MEAS_DONE));

                        var takeSample2 = await client.SampleAboveDetectorAsync(new SampleAboveDetectorRequest { DevId = devId, IsAbove = false });

                        await sc.PutSampleToTheDiskAsync((short)ts.CellNum, ct.Token);

                        var tsNext = await client.SampleInCellAsync(new SampleInCellRequest { DevId = devId, IsInCell = true });

                        var isLastMeasReply = await client.IsLastMeasurementAsync(new IsLastMeasurementRequest { DevId = devId });

                        stopMeas = isLastMeasReply.IsLast;
                    }

                    await sc.HomeAsync(ct.Token);

                    // NOTE: In order to not close main thread after await!
                    Console.WriteLine("Push any key to exit...");
                    Console.ReadKey();

                } // using (var sc = new SampleChanger(devId))
            }
            catch (TaskCanceledException)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_CLNT_DEV_NOT_ANS) { DetailedText = $"Device '{devId}' do not answer. XemoClient will be rerun." });
                Shell.StartProcess("XemoClient.exe", devId.ToString());
            }
            catch (Exception ex)
            {
                await client.DeviceErrorAsync(new ErrorOccurredRequest { DevId = devId });
                Report.Notify(new Message(Codes.ERR_XM_GRPC_CLNT_UNREG) { DetailedText = ex.Message });
            }
        }

    } // public class Client
}     // namespace Regata.Core.GRPC.Xemo
