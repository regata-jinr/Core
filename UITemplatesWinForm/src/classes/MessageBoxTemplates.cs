using System;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Regata.UITemplates
{
    public static class MessageBoxTemplates
    {
        //FIXME: now such notification (via message box) paused measurements process. In case of errors
        //       this is correct behaviour, user should decide what he wants to do retry or cancel and change something
        //       but for warnings and successes it should has timeout

        private static Task ErrorTask(string message)
        {
            return Task.Run(() => MessageBox.Show(message, $"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private static Task WarningTask(string message)
        {
            return Task.Run(() => MessageBox.Show(message, $"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning));
        }
        private static Task InfoTask(string message)
        {
            return Task.Run(() => MessageBox.Show(message, $"Info!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk));
        }

        public static async void InfoAsync(string message)
        {
            await InfoTask(message);
        }

        public static void InfoSync(string message)
        {
            InfoTask(message).Wait();
        }

        public static async void WarningAsync(string message)
        {
            await WarningTask(message);
        }

        public static async void ErrorAsync(string message)
        {
            await ErrorTask(message);
        }

        //TODO: add retry abort mechanic
        public static void ErrorSync(string message)
        {
            ErrorTask(message).Wait();
        }


        private static string MessageTemplate(ref ExceptionEventsArgs exceptionEventsArgs)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var ex = exceptionEventsArgs.exception;
                return ex.Message;

                if (ex.Data != null && ex.Data["Assembly"] != null)
                    stringBuilder.Append($"Assembly name: {ex.Data["Assembly"].ToString()}{Environment.NewLine}");
                if (ex.TargetSite != null)
                {
                    stringBuilder.Append($"Instanse name: {ex.TargetSite.DeclaringType}{Environment.NewLine}");
                    //stringBuilder.Append($"Member type:   {ex.TargetSite.MemberType}{Environment.NewLine}");
                    stringBuilder.Append($"Member name:   {ex.TargetSite.Name}{Environment.NewLine}");
                }
                stringBuilder.Append($"Message:       {ex.Message}{Environment.NewLine}");
                stringBuilder.Append($"Stack trace:   {ex.StackTrace}");

                return stringBuilder.ToString();
            }
            catch (Exception e)
            {
                return $"Something wrong with exception handlers!{Environment.NewLine}{e.ToString()}";
            }
        }

        public static async void WrapExceptionToMessageBoxAsync(ExceptionEventsArgs eventsArgs)
        {
            if (eventsArgs.Level == ExceptionLevel.Error)
                await ErrorTask(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Warn)
                await WarningTask(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Info)
                await InfoTask(MessageTemplate(ref eventsArgs));
        }

        public static async void WrapExceptionToMessageBox(ExceptionEventsArgs eventsArgs)
        {
            if (eventsArgs.Level == ExceptionLevel.Error)
                ErrorSync(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Warn)
                await WarningTask(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Info)
                await InfoTask(MessageTemplate(ref eventsArgs));
        }
    }

    public enum ExceptionLevel { Info, Warn, Error };

    public class ExceptionEventsArgs : EventArgs
    {
        public ExceptionLevel Level;
        public Exception exception;
    }
}
