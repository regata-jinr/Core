using System.Windows.Forms;
using System;
using System.Drawing;

public class Prompt : IDisposable
{
    private Form prompt { get; set; }
    public string Result { get; }

    public Prompt(string text, string caption)
    {
        Result = ShowDialog(text, caption);
    }

    private string ShowDialog(string text, string caption)
    {
        prompt = new Form()
        {
            Width = 500,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = caption,
            StartPosition = FormStartPosition.CenterScreen,
            TopMost = true,
            MaximizeBox = false,
            MinimizeBox = false


        };
        Label textLabel = new Label() { Left = 50, Top = 20, AutoSize = true, Text = text, TextAlign = ContentAlignment.MiddleCenter };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
        Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
    }

    public void Dispose()
    {
        //See Marcus comment
        if (prompt != null)
        {
            prompt.Dispose();
        }
    }
}