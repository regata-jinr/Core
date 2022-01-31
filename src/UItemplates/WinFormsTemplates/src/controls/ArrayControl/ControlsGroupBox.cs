using System.Windows.Forms;
using System.Drawing;

namespace Regata.Core.UI.WinForms.Controls

{
    public partial class ControlsGroupBox : UserControl
    {
        public Control[] _controls;

        public ControlsGroupBox(Control[] controls, bool vertical=true)
        {
            InitializeComponent();

            _controls = controls;

            if (!vertical)
            {
                _tableLayoutPanel.ColumnCount = _controls.Length;
                for (int i = 0; i < _controls.Length; ++i)
                    _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _controls.Length));
                
            }
            else
            {
                _tableLayoutPanel.RowCount = _controls.Length;
                for (int i = 0; i < _controls.Length; ++i)
                    _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _controls.Length));
            }

            for (var i = 0; i < _controls.Length; ++i)
                _tableLayoutPanel.Controls.Add(_controls[i]);

            groupBoxTitle.ResumeLayout(false);
            _tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        private Font _font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Point);

        public override Font Font
        {
            get => _font;
            set
            {
                _font = value;
                foreach (var c in _controls)
                {
                    c.Font = _font;
                }
            }
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
