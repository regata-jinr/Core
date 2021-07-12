using System.Windows.Forms;
using System.Drawing;

namespace Regata.Core.UI.WinForms.Controls

{
    public partial class ButtonsArrayControl : UserControl
    {
        public Button[] _buttons;

        public ButtonsArrayControl(int buttonsCounts, bool vertical=true)
        {
            InitializeComponent();

            if (!vertical)
            {
                _tableLayoutPanel.ColumnCount = buttonsCounts;
                for (int i = 0; i < buttonsCounts; ++i)
                    _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / buttonsCounts));
                
            }
            else
            {
                _tableLayoutPanel.RowCount = buttonsCounts;
                for (int i = 0; i < buttonsCounts; ++i)
                    _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / buttonsCounts));
            }

            _buttons = new Button[buttonsCounts];
            for (var i = 0; i < buttonsCounts; ++i)
            {
                _buttons[i] = new Button();
                _buttons[i].Name = $"button_{i}";
                _buttons[i].AutoSize = true;
                _buttons[i].UseVisualStyleBackColor = true;
                _buttons[i].Dock = DockStyle.Fill;
                _buttons[i].Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
                _buttons[i].Text = $"button_{i}";
                _buttons[i].TabIndex = i;
                _tableLayoutPanel.Controls.Add(_buttons[i]);
            }

            groupBoxTitle.ResumeLayout(false);
            _tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        public override string Text
        {
            get
            {
                return groupBoxTitle.Text;
            }
            set
            {
                groupBoxTitle.Text = value;
            }
        }
    }
}
