/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


// Contains methods for connection, disconnection to the device. Reset connection and so on.
// Detector class divided by few files:

// ├── DetectorAcquisition.cs      --> Contains methods that allow to manage of spectra acquisition process 
// |                                    Start, stop, pause, clear acquisition process and also specify acquisition mode
// ├── DetectorCalibration.cs      --> Contains methods for loading calibration files by energy and height
// ├── DetectorConnection.cs       --> opened
// ├── DetectorFileInteractions.cs --> The code in this file determines interaction with acquiring spectra. 
// |                                    E.g. filling information about sample. Save file
// ├── DetectorInitialization.cs   --> Contains constructor of type, destructor and additional parameters. Like Status enumeration
// |                                    Events arguments and so on
// ├── DetectorParameters.cs       --> Contains methods for getting and setting any parameters by special code
// |                                    See codes here CanberraDeviceAccessLib.ParamCodes 
// |                                    Also some of important parameters wrapped into properties
// ├── DetectorProperties.cs       --> Contains description of basics properties, events, enumerations and additional classes
// ├── G2KUtilities.cs             --> Contains aliases for running utilities from GENIE2K/EXEFILES.
// └── IDetector.cs                --> Interface of the Detector type

using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;

namespace Regata.Core.Hardware
{
    public partial class Detector : IDisposable
    {
        //TODO: async code doesn't work!

        private const int _timeOut = 5;

        public static async Task<ProcessResult> RunMvcgAsync()
        {
            var result = await ExecuteShellCommand("putview.exe", @"/CXCY=-100,-100 /NO_DATASRC");
            return result;
        }

        public static async Task<ProcessResult> ShowDetectorInMvcgAsync(string det)
        {
            var result = await ExecuteShellCommand("pvopen.exe", $"DET:{det} //READ_ONLY");
            return result;
        }

        public static async Task<ProcessResult> CloseMvcgAsync()
        {
            var result = await ExecuteShellCommand("endview.exe", "");
            return result;
        }


        public static async Task<ProcessResult> ExecuteShellCommand(string command, string arguments)
        {
            var result = new ProcessResult();

            using (var process = new Process())
            {

                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"C:\GENIE2K\EXEFILES";
                // Подписка на события записи в выходные потоки процесса

                var outputBuilder = new StringBuilder();
                var outputCloseEvent = new TaskCompletionSource<bool>();

                process.OutputDataReceived += (s, e) =>
                                                {
                                                    // Поток output закрылся (процесс завершил работу)
                                                    if (string.IsNullOrEmpty(e.Data))
                                                    {
                                                        outputCloseEvent.SetResult(true);
                                                    }
                                                    else
                                                    {
                                                        outputBuilder.AppendLine(e.Data);
                                                    }
                                                };

                var errorBuilder = new StringBuilder();
                var errorCloseEvent = new TaskCompletionSource<bool>();

                process.ErrorDataReceived += (s, e) =>
                                                {
                                                    // Поток error закрылся (процесс завершил работу)
                                                    if (string.IsNullOrEmpty(e.Data))
                                                    {
                                                        errorCloseEvent.SetResult(true);
                                                    }
                                                    else
                                                    {
                                                        errorBuilder.AppendLine(e.Data);
                                                    }
                                                };

                bool isStarted;

                try
                {
                    isStarted = process.Start();
                }
                catch (Exception error)
                {
                    // Usually it occurs when an executable file is not found or is not executable

                    result.Completed = true;
                    result.ExitCode = -1;
                    result.Output = error.Message;

                    isStarted = false;
                }

                if (isStarted)
                {
                    // Reads the output stream first and then waits because deadlocks are possible
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Creates task to wait for process exit using timeout
                    var waitForExit = WaitForExitAsync(process, _timeOut);

                    // Create task to wait for process exit and closing all output streams
                    var processTask = Task.WhenAll(waitForExit, outputCloseEvent.Task, errorCloseEvent.Task);

                    // Waits process completion and then checks it was not completed by timeout
                    if (await Task.WhenAny(Task.Delay(_timeOut), processTask) == processTask && waitForExit.Result)
                    {
                        result.Completed = true;
                        result.ExitCode = process.ExitCode;

                        // Adds process output if it was completed with error
                        if (process.ExitCode != 0)
                        {
                            result.Output = $"{outputBuilder}{errorBuilder}";
                        }
                    }
                    else
                    {
                        try
                        {
                            // Kill hung process
                            process.Kill();
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return result;
        }


        private static Task<bool> WaitForExitAsync(Process process, int timeout)
        {
            return Task.Run(() => process.WaitForExit(timeout));
        }

        public struct ProcessResult
        {
            public bool Completed;
            public int? ExitCode;
            public string Output;
        }


        public static void Run(string command, string arguments)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"C:\GENIE2K\EXEFILES";
                process.Start();
            }
            //catch (Exception ex)
            //{
            //    MessageBoxTemplates.ErrorSync(ex.ToString()); 
            //}

        }

        public static void RunMvcg() => Run("putview.exe", @"/CXCY=-100,-100 /NO_DATASRC");

        public static void ShowDetectorInMvcg(string det) => Run("pvopen.exe", $"DET:{det} /READ_ONLY");

        public static void CloseDetector(string det) => Run("pvclose.exe", $"DET:{det}");

        public static void SelectDetector(string det) => Run("pvselect.exe", $"DET:{det}");

        public static void CloseMvcg() => Run("endview.exe", "");

    } // public partial class Detector : IDisposable
}     // namespace Regata.Core.Hardware
