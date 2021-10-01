/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2019-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using AdysTech.CredentialManager;
using Regata.Core.UI.WinForms.Items;
using Regata.Core.DataBase;
using RCM=Regata.Core.Messages;
using Regata.Core.Settings;
using System;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class LoginForm : Form
    {
        private static RCM.Message _msg = new RCM.Message() { Caption = "Login into regata DB" };
        private SqlConnectionStringBuilder _sqlcs;
        public LoginForm()
        {
            try
            {
                InitializeComponent();
                textBoxLoginFormUser.Focus();
#if NET5_0_OR_GREATER
                Report.NotificationEvent += (msg) => { PopUpMessage.Show(msg, 5); };
#endif
                _sqlcs = new SqlConnectionStringBuilder(CredentialManager.GetCredentials(GlobalSettings.Targets.DB).Password);

                if (System.Diagnostics.Process.GetProcesses().Count(p => p.ProcessName == System.Diagnostics.Process.GetCurrentProcess().ProcessName) >= 2)
                {
                    _msg.Status = Status.Warning;
                    _msg.Head = "Process already exists";
                    _msg.Text = "You try to open already opened application.";
                    Report.Notify(_msg);
                    throw new InvalidOperationException("Process has already opened");
                }
            }
            catch (NullReferenceException)
            {
                _msg.Status = Status.Error;
                _msg.Head = "DB target was not found";
                _msg.Text = "Before using regata application you have to add target in Windows Credential Manager";
                Report.Notify(_msg);
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error in Regata login system";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
            }
        }

        public TextBox textboxPin;
        public Button  addPinCodeButton;
        public Label pinLabel;
        private bool isPinButtonClicked = false;
        private string _user;
        private string _password;
        private string _pin;
        private bool _isPin = false;

        public event Action<SqlConnectionStringBuilder> ConnectionSuccessfull;

        private void DoesPinExist(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxLoginFormUser.Text))
                    return;
                var sm = CredentialManager.GetCredentials($"Pin_{textBoxLoginFormUser.Text}");
                if (sm == null)
                    return;
                label2.Text = "Пин код:";
                _isPin = true;
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Problem with checking of pin codes";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
            }
        }

        private async void ButtonLoginFormEnter_Click(object sender, EventArgs e)
        {
            try
            {
                var isPinCorrect = false;

                if (_isPin)
                {
                    _pin = textBoxLoginFormPassword.Text;
                    _user = textBoxLoginFormUser.Text;
                    isPinCorrect = CheckPin();

                    if (isPinCorrect)
                    {
                        var sm = CredentialManager.GetCredentials($"Password_{_user}");
                        if (sm == null)
                        {
                            _msg.Status = Status.Warning;
                            _msg.Head = "Pin cod was not found";
                            _msg.Text = "You try to use pin code, but it wasn't found. Try to add it before using.";
                            Report.Notify(_msg);
                            return;
                        }
                        _password = sm.Password;
                    }
                    else
                    {
                        _msg.Status = Status.Warning;
                        _msg.Head = "Unsuccessful login";
                        _msg.Text = "Wrong login or pin. In case of you don't remember your pin, you can save a new one.";
                        Report.Notify(_msg);
                        return;
                    }
                }
                _sqlcs.UserID = _user;
                _sqlcs.Password = _password;

                RegataContext.ConString = _sqlcs.ConnectionString;

                using (var r = new RegataContext())
                {
                    if (await r.Database.CanConnectAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token))
                    {
                        ConnectionSuccessfull?.Invoke(_sqlcs);
                        Hide();
                    }
                    else
                    {
                        _msg.Status = Status.Warning;
                        _msg.Head = "Unsuccessful login";
                        _msg.Text = "Wrong login or pin. In case of you don't remember your pin, you can save a new one.";
                        Report.Notify(_msg);
                        return;

                    }
                }
            }
            catch (TaskCanceledException)
            {
                _msg.Status = Status.Warning;
                _msg.Head = "Connection timeout";
                _msg.Text = "Too long time for connection. Seems DB is not available";
                Report.Notify(_msg);
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error in Regata login system";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
            }

        }

        private void ButtonLoginFormCreatePin_Click(object sender, EventArgs e)
        {
            try
            {
                if (isPinButtonClicked)
                {
                    this.Controls.Clear();
                    this.InitializeComponent();
                    Text = $"Regata Measurements UI - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
                    isPinButtonClicked = false;
                }
                else
                    CreatePinExtensions();
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error during pin code creation";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
                return;
            }

        }

        private void CreatePinExtensions()
        {
            try
            {
                int x = this.Size.Height - buttonLoginFormCreatePin.Location.Y - buttonLoginFormCreatePin.Size.Height; // distance between textboxes and buttons
                int y = textBoxLoginFormPassword.Location.Y - textBoxLoginFormUser.Location.Y - textBoxLoginFormUser.Size.Height; //distance between textboxes
                this.Size = new Size(this.Size.Width, this.Size.Height + y + textBoxLoginFormPassword.Size.Height);


                textboxPin = new TextBox();
                textboxPin.Location = new System.Drawing.Point(textBoxLoginFormPassword.Location.X, textBoxLoginFormPassword.Location.Y + y + textBoxLoginFormPassword.Size.Height);
                textboxPin.Size = new System.Drawing.Size(136, 22);
                this.Controls.Add(textboxPin);

                pinLabel = new Label();
                pinLabel.Location = new System.Drawing.Point(label1.Location.X, textBoxLoginFormPassword.Location.Y + y + textBoxLoginFormPassword.Size.Height);
                pinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pinLabel.Size = new System.Drawing.Size(75, 28);
                pinLabel.Text = "Пин-код:";
                label2.Text = "Пароль:";
                pinLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
                this.Controls.Add(pinLabel);

                buttonLoginFormCreatePin.Location = new System.Drawing.Point(buttonLoginFormCreatePin.Location.X - 15, buttonLoginFormCreatePin.Location.Y + x);

                buttonLoginFormEnter.Location = new System.Drawing.Point(buttonLoginFormEnter.Location.X + 20, buttonLoginFormEnter.Location.Y + x);
                isPinButtonClicked = true;
                buttonLoginFormCreatePin.Text = "Скрыть";

                addPinCodeButton = new Button();
                addPinCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                addPinCodeButton.Location = new System.Drawing.Point(-15 + (-buttonLoginFormCreatePin.Location.X - buttonLoginFormCreatePin.Size.Width + buttonLoginFormEnter.Location.X), buttonLoginFormEnter.Location.Y);
                addPinCodeButton.Size = buttonLoginFormCreatePin.Size;
                addPinCodeButton.Text = "Сохранить пин-код";
                addPinCodeButton.UseVisualStyleBackColor = true;
                addPinCodeButton.Click += new System.EventHandler(addPinCodeButton_Click);
                this.Controls.Add(addPinCodeButton);
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error during creation of 'new pin code' area";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
                return;
            }

        }

        private async void addPinCodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textboxPin == null)
                    return;
                if (string.IsNullOrEmpty(textboxPin.Text) || string.IsNullOrEmpty(textBoxLoginFormUser.Text) || string.IsNullOrEmpty(textBoxLoginFormPassword.Text))
                {
                    _msg.Status = Status.Warning;
                    _msg.Head = "You try to add pin code with empty password or login";
                    _msg.Text = "No one field should be empty";
                    Report.Notify(_msg);
                    return;
                }

                if (uint.TryParse(textboxPin.Text, out _) && textboxPin.Text.Length == 4)
                {
                    _user = textBoxLoginFormUser.Text;
                    _password = textBoxLoginFormPassword.Text;
                    _pin = textboxPin.Text;

                    _sqlcs.UserID = _user;
                    _sqlcs.Password = _password;

                    RegataContext.ConString = _sqlcs.ConnectionString;

                    using (var r = new RegataContext())
                    {
                    if (await r.Database.CanConnectAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token))
                            {
                            CredentialManager.SaveCredentials($"Password_{textBoxLoginFormUser.Text}", new NetworkCredential(textBoxLoginFormUser.Text, textBoxLoginFormPassword.Text));
                            CredentialManager.SaveCredentials($"Pin_{textBoxLoginFormUser.Text}", new NetworkCredential(textBoxLoginFormUser.Text, textboxPin.Text));
                            _msg.Status = Status.Success;
                            _msg.Head = "Pin code was saved!";
                            Report.Notify(_msg);

                            _isPin = true;
                            this.Controls.Clear();
                            this.InitializeComponent();
                            Text = $"Regata Measurements UI - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
                            isPinButtonClicked = false;
                            textBoxLoginFormUser.Text = _user;
                            textBoxLoginFormPassword.Text = _pin;
                        }
                        else
                        {
                            _msg.Status = Status.Warning;
                            _msg.Head = "Unsuccessful login";
                            _msg.Text = "Wrong login or password.";
                            Report.Notify(_msg);
                            return;

                        }
                    }
                }
                else
                {
                    _msg.Status = Status.Warning;
                    _msg.Head = "Unsuccessful saving pin code";
                    _msg.Text = "Wrong pin format. Pin code should contain only 4 digit";
                    Report.Notify(_msg);
                    return;
                }
            }
            catch (TaskCanceledException)
            {
                _msg.Status = Status.Warning;
                _msg.Head = "Connection timeout";
                _msg.Text = "Too long time for connection. Seems DB is not available";
                Report.Notify(_msg);
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error during pin code adding.";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
                return;
            }
        }


        private bool CheckPin()
        {
            try
            {
                var isCorrect = false;

                var sm = CredentialManager.GetCredentials($"Pin_{_user}");
                if (sm == null)
                    return false;

                if (_pin == sm.Password && _user == sm.UserName)
                    isCorrect = true;

                return isCorrect;
            }
            catch (Exception ex)
            {
                _msg.Status = Status.Error;
                _msg.Head = "Unregistred error during pin code checking";
                _msg.Text = ex.Message;
                _msg.DetailedText = ex.ToString();
                Report.Notify(_msg);
                return false;
            }
        }


    } //  public partial class LoginForm : Form
}     // namespace Regata.Core.UI.WinForms.Forms
