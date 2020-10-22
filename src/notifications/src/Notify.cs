/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

// TODO: I think that have to provide two ways for notification:
//          1. By hand in case of success or warning messages
//          2. Automatically based on exception
// TODO: How to convert exception to notificationeventsargs
// TODO: To process notification (show it via message box, or via email, user should subscribe event)

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Collections.ObjectModel;

namespace Regata.Core.Notifications
{

    public static class Notify
    {
        static Notify()
        {
            _mailSubs = new ObservableCollection<string>();
            //TODO: fill from settings
        }

        // TODO: move to settings class
        #region Move to settings class

        private static ObservableCollection<string> _mailSubs;

        public static void AddEmail(string email)
        {
            _mailSubs.Add(email);
        }

        public static void RemoveMail(string email)
        {
            _mailSubs.Remove(email);
        }

        #endregion

        private static Dictionary<NotificationLevel, NLog.LogLevel> ExceptionLevel_LogLevel = new Dictionary<NotificationLevel, NLog.LogLevel> { { NotificationLevel.Error, NLog.LogLevel.Error }, { NotificationLevel.Warning, NLog.LogLevel.Warn }, { NotificationLevel.Info, NLog.LogLevel.Info } };

        // FIXME: in case of one of the available detector has already opened (e.g. by hand not in read only mode)
        //       application will not run. The problem related with this static event!

        public static event Action<Notification> NotificationEvent;

        public static event Action<Notification> NotifyErrorEvent;
        public static event Action<Notification> NotifySuccessEvent;
        public static event Action<Notification> NotifyWarningEvent;
        public static event Action<Notification> NotifyInfoEvent;


        private static NLog.Logger _nLogger = AppManager.logger;

        public static void NotifyError(RException rex)
        {
            //TODO: add user from settings and place from stack trace

            var notif = new Notification
            {
                Level = NotificationLevel.Error,
                TechBody = ex.ToString(),
                BaseBody = ex.Message,
                Title = $"[{ex.RErrorCode}]: {ex.Sender}"
            };

            //TODO: exception message length as parameter to settings
            if (notif.TechBody.Length > 200)
                notif.TechBody = notif.TechBody.Substring(0, 200);
            return notif;

            if (CriticalErrors.Contains(rex.RErrorCode))
            {
                //TODO: send by email
            }


        }

        public static void NotifySuccess(string message, string title)
        {
            NotifyCase(NotificationLevel.Success, message, title);

        }

        public static void NotifyWarning(string message, string title)
        {
            NotifyCase(NotificationLevel.Warning, message, title);
        }

        public static void NotifyInfo(string message, string title)
        {
            NotifyCase(NotificationLevel.Info, message, title);
        }


        private static void NotifyCase(Notification notf)
        {
            switch (notf.Level)
            {
                case NotificationLevel.Info:
                    break;
                case NotificationLevel.Success:
                    break;
                case NotificationLevel.Warning:
                    break;
                case NotificationLevel.Error:

                    

                    break;
            }


            NotificationEvent?.Invoke(notf);
        }

        private static Notification ConvertExceptionToNotification(RException ex)
        {
            
        }

        public static void Not(NotificationLevel lvl, string sender, bool NotifyByEmail = false)
        {
            try
            {
                _nLogger.WithProperty("Sender", sender);

                var notif = ConvertExceptionToNotification(ex, lvl, sender);

                _nLogger.Log(ExceptionLevel_LogLevel[lvl], notif.TechBody);

                Notify(notif, NotifyByEmail || lvl == NotificationLevel.Error);
            }
            catch (Exception e)
            {
                if (NotificationEvent == null) return;
                e.Data.Add("Assembly", "Measurements.Core");
                var tstr = e.ToString();
                if (tstr.Length <= 1998)
                    _nLogger.Log(NLog.LogLevel.Error, e.ToString());
                else
                    _nLogger.Log(NLog.LogLevel.Error, e.ToString().Substring(0, 1998));
            }
        }


        public static void Notify(Notification nea, bool NotifyByEmail = false)
        {
            NotificationEvent?.Invoke(nea);

            if (NotifyByEmail)
                SendNotificationByEmail(nea);
        }

        // FIXME in case of network error will this suspend the app despite of SendAsync?
        private static void SendNotificationByEmail(Notification nea)
        {
            var mail = SecretsManager.GetCredential(AppManager.MailServiceTarget).Name;
            var mailPass = SecretsManager.GetCredential(AppManager.MailServiceTarget).Secret;
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
                foreach (var toAddress in _mailSubs)
                {
                    using (var message = new MailMessage(mail, toAddress)
                    {
                        Subject = nea.ToString(),
                        Body = string.Join(
                          nea.BaseBody,
                          Environment.NewLine,
                          "===*******TECH INFO*******==",
                          Environment.NewLine,
                          nea.TechBody
                          )
                    })
                    {
                        smtp.SendAsync(message, null);
                    }
                }
            }
        }
    }  // public static class Notify

    public enum NotificationLevel { Error, Info, Warning, Success }


    public class Notification : EventArgs
    {
        public NotificationLevel Level;
        public string BaseBody;
        public string TechBody;
        public string Place;
        public string User;
        public RErrorCodes ErrorCode;
        public string Title;
        public override string ToString() => $"[{Level}] {Title}";
    }

} // namespace Regata.Measurements.Managers
