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

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Regata.Core
{
    public static class Shell
    {
        public const int TimeOutSec = 15;

        public static async Task<ProcessResult> ExecuteCommandAsync(string command, string arguments = "", string workDir = "")
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
                if (Directory.Exists(workDir))
                    process.StartInfo.WorkingDirectory = workDir;
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
                    var waitForExit = WaitForExitAsync(process, TimeOutSec);

                    // Create task to wait for process exit and closing all output streams
                    var processTask = Task.WhenAll(waitForExit, outputCloseEvent.Task, errorCloseEvent.Task);

                    // Waits process completion and then checks it was not completed by timeout
                    if (await Task.WhenAny(Task.Delay(TimeSpan.FromSeconds(TimeOutSec)), processTask) == processTask && waitForExit.Result)
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
            return Task.Run(() => process.WaitForExit(timeout * 1000));
        }

        public static void ShowXemoDevicesCams()
        {
            Process.Start(new ProcessStartInfo("http://159.93.105.78/") { UseShellExecute = true });
            Process.Start(new ProcessStartInfo("http://159.93.105.75/") { UseShellExecute = true });
            Process.Start(new ProcessStartInfo("http://159.93.105.79/") { UseShellExecute = true });
        }

    } // public static class Shell

    public struct ProcessResult
    {
        public bool Completed;
        public int? ExitCode;
        public string Output;
    }

}     // namespace Regata.Core