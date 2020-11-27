/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/


using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using AdysTech.CredentialManager;

namespace Regata.Core.Report
{
    public static class Report
    {
        /// <summary>
        /// Initialization of Report service. The service allows to user setting up reporting system:
        /// - logs
        /// - different levels messaging:
        ///     - info  
        ///     - warn  
        ///     - error
        ///     - success
        /// - email notification
        /// - different gui notification
        /// Initialization of the system based on secrets that keeps into Windows Credential Manager (WCM). You can load them via specifying Target as arguments:
        /// </summary>
        /// <param name="LogConnectionStringTarget">WCM target for connection string for NLog DataBase provider</param>
        /// <param name="MailHostTarget">WCM target for Mail Host adress and password</param>
        public static void Init(string LogConnectionStringTarget, string  MailHostTarget)
        {
            NLog.GlobalDiagnosticsContext.Set("LogConnectionString", CredentialManager.GetCredentials(LogConnectionStringTarget).Password);
            _nLogger = NLog.LogManager.GetCurrentClassLogger();
            _hostTarget = MailHostTarget;
        }

        private static string _hostTarget;

        private static readonly Dictionary<InformLevel, NLog.LogLevel> ExceptionLevel_LogLevel = new Dictionary<InformLevel, NLog.LogLevel> { { InformLevel.Error, NLog.LogLevel.Error }, { InformLevel.Warning, NLog.LogLevel.Warn }, { InformLevel.Info, NLog.LogLevel.Info } };

        public static MailAddressCollection Emails = new MailAddressCollection();

        // FIXME: in case of one of the available detector has already opened in not read-only mode (e.g. by hand) application will not run. The problem related with this static event!
        /// <summary>
        /// This event can be use as entry point for GUI messaging
        /// </summary>
        public static event Action<Message> NotificationEvent;

        private static NLog.Logger _nLogger;

        private static Message ExceptionToMessage(Exception ex)
        {
            var notif = new Message
            {
                Level = InformLevel.Error,
                TechBody = ex.ToString(),
                BaseBody = ex.Message,
                Title = $"[{InformLevel.Error}]: Unregistered error"
            };

            //TODO: exception message length as parameter to settings
            if (notif.TechBody.Length > 200)
                notif.TechBody = notif.TechBody.Substring(0, 200);
            return notif;

        }

        public static async Task Success(Message msg, bool callEvent = true, bool write2logs= false, bool NotifyByEmail = false)
        {
            msg.Level = InformLevel.Success;
            await WriteMessage(msg, callEvent, write2logs, NotifyByEmail);

        }

        public static async Task Info(Message msg, bool callEvent = false, bool write2logs = false)
        {
            msg.Level = InformLevel.Info;
            await WriteMessage(msg, callEvent, write2logs, NotifyByEmail:false);

        }

        public static async Task Warning(Message msg, bool callEvent = true, bool write2logs = true, bool NotifyByEmail = false)
        {
            msg.Level = InformLevel.Warning;
            await WriteMessage(msg, callEvent, write2logs, NotifyByEmail);

        }

        public static async Task Error(Message msg, bool NotifyByEmail = true)
        {
            msg.Level = InformLevel.Error;
            await WriteMessage(msg, callEvent: true, WriteToLog: true, NotifyByEmail);
        }

        public static async Task Error(Exception excp, bool NotifyByEmail = true)
        {
            Error(ExceptionToMessage(excp), NotifyByEmail: NotifyByEmail);
        }

        private static async Task WriteMessage(Message msg, bool callEvent, bool WriteToLog, bool NotifyByEmail = false)
        {
            try
            {
                if (WriteToLog)
                {
                    //_nLogger.WithProperty("assistant", msg.User);
                    _nLogger.SetProperty("Assistant", msg.User);
                    _nLogger.SetProperty("Sender", msg.Sender);
                    _nLogger.Log(ExceptionLevel_LogLevel[msg.Level], msg.TechBody);
                }

                if (NotifyByEmail)
                    await SendMessageByEmail(msg);
                
                if (callEvent)
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


        // FIXME in case of network error will this suspend the app despite of SendAsync?
        private static async Task SendMessageByEmail(Message msg)
        {
            if (Emails == null || !Emails.Any()) return;
            var mail = CredentialManager.GetCredentials(_hostTarget).UserName;
            var mailPass = CredentialManager.GetCredentials(_hostTarget).Password;
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
                        await smtp.SendMailAsync(message);
                    }
                }
            }
        }
    }  // public static class Notify

    public enum InformLevel { Error, Info, Warning, Success }


    public class Message : EventArgs
    {
        public string BaseBody;
        public string TechBody;
        public string Place;
        public string User;
        public string Sender;
        public uint   Code;
        public InformLevel  Level;
        public string Title;
        public override string ToString() => $"[{Level}] {Title}";
        
    }

} // namespace Regata.Measurements.Managers
