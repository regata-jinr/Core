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
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Gxemo;
using Regata.Core.Messages;

namespace Regata.Core.GRPC.Xemo.Services
{

    public class XemoService : GRPCXemo.GRPCXemoBase
    {
        public static Dictionary<int, int> DevCells = new Dictionary<int, int>()
        {
            {107374, 1 },
            {107375, 1 },
            {107376, 1 },
            {114005, 1 },
        };

        public static Dictionary<int, bool> DevBusy = new Dictionary<int, bool>()
        {
            {107374, false },
            {107375, false },
            {107376, false },
            {114005, false },
        };


        public static Dictionary<int, bool> SampleIsAboveDet = new Dictionary<int, bool>()
        {
            {107374, false },
            {107375, false },
            {107376, false },
            {114005, false },
        };


        public static Dictionary<int, bool> LastMeas = new Dictionary<int, bool>()
        {
            {107374, false },
            {107375, false },
            {107376, false },
            {114005, false },
        };

        public static Dictionary<int, PutSampleAboveDetReply.Types.Height> DevH = new Dictionary<int, PutSampleAboveDetReply.Types.Height>()
        {
            {107374, PutSampleAboveDetReply.Types.Height.H20 },
            {107375, PutSampleAboveDetReply.Types.Height.H20 },
            {107376, PutSampleAboveDetReply.Types.Height.H20 },
            {114005, PutSampleAboveDetReply.Types.Height.H20 },
        };
        //private readonly ILogger<XemoService> _logger;
        //public XemoService(ILogger<XemoService> logger)
        //{
        //    _logger = logger;
        //}

        public override Task<TakeSampleFromCellReply> DeviceIsReady(DeviceIsReadyRequest request, ServerCallContext context)
        {
            try
            {
                if (!request.IsReady)
                    throw new InvalidOperationException($"Xemo device '{request.DevId}' is not ready");

                Report.Notify(new Message(Codes.INFO_XM_GRPC_SERV_Dev_Start_Moving) { DetailedText = $"Start movement to cell {DevCells[request.DevId]} one from device {request.DevId}" });
                var t = new TakeSampleFromCellReply { CellNum = DevCells[request.DevId] };
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_SERV_DevIsReady_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

        public override Task<PutSampleAboveDetReply> SampleHasTaken(SampleHasTakenRequest request, ServerCallContext context)
        {
            try
            {
                if (!request.IsTaken)
                    throw new InvalidOperationException($"Xemo device '{request.DevId}' has not taken sample");
                var t = new PutSampleAboveDetReply { H = DevH[request.DevId] };
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_SERV_SmplHasTaken_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

        public override async Task<PutSampleToDiskReply> SampleAboveDetector(SampleAboveDetectorRequest request, ServerCallContext context)
        {
            try
            {
                if (!request.IsAbove)
                    throw new InvalidOperationException($"Xemo device '{request.DevId}' is not above detector");

                var t = new PutSampleToDiskReply { CellNum = DevCells[request.DevId] };
                SampleIsAboveDet[request.DevId] = request.IsAbove;

                return await Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_SERV_Smpl_Abv_Det_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

        public override Task<TakeSampleFromCellReply> SampleInCell(SampleInCellRequest request, ServerCallContext context)
        {
            try
            {
                if (!request.IsInCell)
                    throw new InvalidOperationException($"Xemo device '{request.DevId}' has not putting sample to cell");

                var t = new TakeSampleFromCellReply { CellNum = DevCells[request.DevId] };
                SampleIsAboveDet[request.DevId] = false;
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_SERV_SampleInCell_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

        public override Task<ErrorOccurredReply> DeviceError(ErrorOccurredRequest request, ServerCallContext context)
        {
            try
            {
                var t = new ErrorOccurredReply();
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_SERV_DevErr_UNREG) { DetailedText = string.Join(Environment.NewLine, ex.Message, $"Error code '{request.ErrCode}' has getting from device '{request.DevId}'") });
            }
            return null;
        }

        public override Task<IsLastMeasurementReply> IsLastMeasurement(IsLastMeasurementRequest request, ServerCallContext context)
        {
            try
            {
                Report.Notify(new Message(Codes.INFO_XM_GRPC_LAST_MEAS_HAS_DONE));

                var t = new IsLastMeasurementReply() { IsLast = LastMeas[request.DevId] } ;
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_LAST_MEAS_HAS_DONE_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

        public override Task<IsMeasurementsDoneReply> IsMeasurementsDone(IsMeasurementsDoneRequest request, ServerCallContext context)
        {
            try
            {
                Report.Notify(new Message(Codes.INFO_XM_GRPC_IS_MEAS_HAS_DONE));

                var t = new IsMeasurementsDoneReply() { IsDone = DevBusy[request.DevId] };
                return Task.FromResult(t);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_XM_GRPC_IS_MEAS_HAS_DONE_UNREG) { DetailedText = ex.Message });
            }
            return null;
        }

    } // public class XemoService : GRPCXemo.GRPCXemoBase
}     // namespace Regata.Core.GRPC.Xemo.Services
