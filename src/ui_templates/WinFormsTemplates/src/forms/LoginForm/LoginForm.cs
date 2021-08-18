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
using Regata.Core.DataBase;
using RCM=Regata.Core.Messages;
using Regata.Core.Settings;
using System;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Forms
{
    public partial class LoginForm : Form
    {
        private SqlConnectionStringBuilder _sqlcs;
        public LoginForm()
        {
            try
            {
                InitializeComponent();
                textBoxLoginFormUser.Focus();

                _sqlcs = new SqlConnectionStringBuilder(CredentialManager.GetCredentials(GlobalSettings.Targets.DB).Password);

                if (System.Diagnostics.Process.GetProcesses().Count(p => p.ProcessName == System.Diagnostics.Process.GetCurrentProcess().ProcessName) >= 2)
                {
                    Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_APP_ALREADY_OPENED));
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_UNREG) { DetailedText = ex.ToString()});
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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CHCK_PIN_UNREG) { DetailedText = ex.ToString()});
            }
        }

        private void ButtonLoginFormEnter_Click(object sender, EventArgs e)
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
                            Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_ENTER_PIN_NOT_FOUND));
                            //MessageBoxTemplates.ErrorSync("Пароль связанный с пин-кодом не найден. Попробуйте создать пин-код заново");
                            return;
                        }
                        _password = sm.Password;
                    }
                    else
                    {
                        Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_ENTER_WRONG_PIN_OR_USER));
                        //MessageBoxTemplates.ErrorSync("Неправильный логин или пин-код");
                        return;
                    }
                }
                _sqlcs.UserID = _user;
                _sqlcs.Password = _password;

                using (var r = new RegataContext(_sqlcs.ConnectionString))
                {
                    if (r.Database.CanConnect())
                    {
                        ConnectionSuccessfull?.Invoke(_sqlcs);
                        Hide();
                    }
                    else
                    {
                        Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_ENTER_WRONG_LOGIN_OR_PASS));
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_LOGIN_ENTER_UNREG) { DetailedText = ex.ToString()});

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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CRT_PIN_UNREG) { DetailedText = ex.ToString()});
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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CRT_PIN_FIELD_UNREG) { DetailedText = ex.ToString()});
            }

        }

        private void addPinCodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textboxPin == null)
                    return;
                if (string.IsNullOrEmpty(textboxPin.Text) || string.IsNullOrEmpty(textBoxLoginFormUser.Text) || string.IsNullOrEmpty(textBoxLoginFormPassword.Text))
                {
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_EMPTY_FIELD));
                    //MessageBoxTemplates.ErrorSync("При создании пин-кода все поля должны быть заполнены");
                    return;
                }

                if (uint.TryParse(textboxPin.Text, out _) && textboxPin.Text.Length == 4)
                {
                    _user = textBoxLoginFormUser.Text;
                    _password = textBoxLoginFormPassword.Text;
                    _pin = textboxPin.Text;

                    _sqlcs.UserID = _user;
                    _sqlcs.Password = _password;

                    using (var r = new RegataContext(_sqlcs.ConnectionString))
                    {
                        if (r.Database.CanConnect())
                        {
                            CredentialManager.SaveCredentials($"Password_{textBoxLoginFormUser.Text}", new NetworkCredential(textBoxLoginFormUser.Text, textBoxLoginFormPassword.Text));
                            CredentialManager.SaveCredentials($"Pin_{textBoxLoginFormUser.Text}", new NetworkCredential(textBoxLoginFormUser.Text, textboxPin.Text));
                            Report.Notify(new RCM.Message(Codes.SUCC_UI_WF_PIN_SAVED));
                            //MessageBoxTemplates.InfoAsync("Пин-код успешно сохранен");

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
                            Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CRT_PIN_WRONG_PASS_OR_USER));
                            return;
                        }
                    }
                }
                else
                {
                    Report.Notify(new RCM.Message(Codes.WARN_UI_WF_WRONG_PIN_FORMAT));
                    //MessageBoxTemplates.ErrorSync("Пин-код должен быть целым четырехзначным числом");
                    return;
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ADD_PIN_UNREG) { DetailedText = ex.ToString()});
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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CHCK_PIN_UNREG) { DetailedText = ex.ToString()});
                return false;
            }
        }


    } //  public partial class LoginForm : Form
}     // namespace Regata.Desktop.WinForms.Measurements
