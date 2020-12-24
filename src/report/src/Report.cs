/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using AdysTech.CredentialManager;

namespace Regata.Core
{
    /// <summary>
    /// Implementation of the Report service. The service allows to user setting up reporting system:
    /// - logs
    /// - different levels messaging:
    ///     - info  
    ///     - warn  
    ///     - error
    ///     - success
    /// - email notification
    /// - different gui notification
    /// For each class we have to use instance of logger with predefined settings.
    /// Initialization of the system based on secrets that keeps into Windows Credential Manager (WCM). You can load them via specifying Target as arguments:
    /// </summary>
    public static class Report
    {
        public static Status LogVerbosity = Status.Info;
        public static Status ShowVerbosity = Status.Warning;
        public static Status MailVerbosity = Status.Error;

        public static string MailHostTarget;
        public static string User;
        public static string PathToMessages;
        private static string _logDir;

        public static string LogDir
        {
            get { return _logDir; }
            set
            {
                if (string.IsNullOrEmpty(LogConnectionStringTarget))
                    return;

                if (_nLogger == null)
                {
                    NLog.GlobalDiagnosticsContext.Set("LogConnectionString", CredentialManager.GetCredentials(LogConnectionStringTarget).Password);
                    _nLogger = NLog.LogManager.GetCurrentClassLogger();
                }

                _logDir = value;
                NLog.GlobalDiagnosticsContext.Set("LogDir", _logDir);

            }
        }

        private static string _logConnectionStringTarget;

        public static string LogConnectionStringTarget
        {
            get { return _logConnectionStringTarget; }
            set
            {
                _logConnectionStringTarget = value;
                if (string.IsNullOrEmpty(_logConnectionStringTarget))
                {
                    _nLogger = null;
                    return;
                }
                NLog.GlobalDiagnosticsContext.Set("LogConnectionString", CredentialManager.GetCredentials(LogConnectionStringTarget).Password);
                _nLogger = NLog.LogManager.GetCurrentClassLogger();
            }
        }
        private  static NLog.Logger _nLogger;


        private static readonly Dictionary<Status, NLog.LogLevel> ExceptionLevel_LogLevel = new Dictionary<Status, NLog.LogLevel> { { Status.Error, NLog.LogLevel.Error }, { Status.Warning, NLog.LogLevel.Warn }, { Status.Info, NLog.LogLevel.Info } };

        public static MailAddressCollection Emails = new MailAddressCollection();

        // FIXME: in case of one of the available detector has already opened in not read-only mode (e.g. by hand) application will not run. The problem related with this  event!
        /// <summary>
        /// This event can be use as entry point for GUI messaging
        /// </summary>
        public  static event Action<Message> NotificationEvent;

        public static void Notify(ushort code, bool callEvent=false, bool WriteToLog=false, bool NotifyByEmail = false) 
        {
            try
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                var method = sf.GetMethod();
                Console.WriteLine($"{method.DeclaringType}.{method.Name}");
                Status status = code == 0 ? Status.Error : (Status)(code / 1000);
                _nLogger.SetProperty("Sender", method.DeclaringType.Name);
                _nLogger.SetProperty("Assistant", User);

                var msg = new Message()
                {
                    User     = User,
                    Code     = code,
                    Level    = status,
                    Sender   = method.DeclaringType.Name,
                    Place    = method.Name,
                    Title    = "",  // FIXME: form the list of linked messages(title, basebody, techbody) with                 code.
                                    // it should depends from PathToMessages that lead to file with contents
                                    // so as follow up I can change language
                    BaseBody = "",
                    TechBody = ""
                };

                if ((int)LogVerbosity <= (int)status || WriteToLog)
                {
                    _nLogger?.Log(ExceptionLevel_LogLevel[status], msg.TechBody);
                }
                 
                if ((int)MailVerbosity <= (int)status || NotifyByEmail)
                     SendMessageByEmail(msg);
                
                if ((int)ShowVerbosity <= (int)status || callEvent)
                    NotificationEvent?.Invoke(msg);

            }
            catch (Exception e)
            {
                if (NotificationEvent == null) return;
                e.Data.Add("Assembly", "Regata.Core.Inform.WriteMessage");
                var tstr = e.ToString();
                if (tstr.Length <= 300)
                    _nLogger.Log(NLog.LogLevel.Error, e.ToString());
                else
                    _nLogger.Log(NLog.LogLevel.Error, e.ToString().Substring(0, 300));
            }
        }


        // FIXME in case of network error will this freeze the app despite of SendAsync?
        private static void SendMessageByEmail(Message msg)
        {
            if (Emails == null || !Emails.Any()) return;
            var mail = CredentialManager.GetCredentials(MailHostTarget).UserName;
            var mailPass = CredentialManager.GetCredentials(MailHostTarget).Password;
            var fromMail = new MailAddress(mail, "REGATA Report Service");

            using (var smtp = new SmtpClient
            {
                Host = "smtp.jinr.ru",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, mailPass)
            })
            {
                foreach (var toAddress in Emails)
                {
                    using (var message = new MailMessage(fromMail, toAddress)
                    {
                        Subject = msg.ToString(),
                        Body = string.Join(
                          msg.BaseBody,
                          Environment.NewLine,
                          "===*******TECH INFO*******==",
                          Environment.NewLine,
                          msg.TechBody
                          )
                    })
                    {
                        //var ct = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                        smtp.Send(message);
                    }
                }
            }
        }
    }  // public  class Notify

    public enum Status { Info, Success, Warning, Error }


    public class Message
    {
        public string BaseBody;
        public string TechBody;
        public string Place;
        public string User;
        public string Sender;
        public ushort Code;
        public Status Level;
        public string Title;
        public override string ToString() => $"[{Level}] {Title}";
    }

} // namespace Regata.Measurements.Managers
