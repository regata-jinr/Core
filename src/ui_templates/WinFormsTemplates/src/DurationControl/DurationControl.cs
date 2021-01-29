using System;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms
{
    public partial class DurationControl : UserControl
    {
        public TimeSpan Duration
        {
            get
            {
                return new TimeSpan((int) DurationControlNumericUpDownDays.Value, 
                                    (int) DurationControlNumericUpDownHours.Value,
                                    (int) DurationControlNumericUpDownMinutes.Value,
                                    (int) DurationControlNumericUpDownSeconds.Value
                                    );
            }

            set
            {
                DurationControlNumericUpDownDays.Value    = value.Days;
                DurationControlNumericUpDownHours.Value   = value.Hours;
                DurationControlNumericUpDownMinutes.Value = value.Minutes;
                DurationControlNumericUpDownSeconds.Value = value.Seconds;
            }
        }

        public event EventHandler DurationChanged;
        public event EventHandler DaysChanged;
        public event EventHandler HoursChanged;
        public event EventHandler MinutesChanged;
        public event EventHandler SecondsChanged;

        public DurationControl(uint days, uint hours, uint minutes, uint seconds) : this()
        {
            DurationControlNumericUpDownDays.Value = days;
            DurationControlNumericUpDownHours.Value = hours;
            DurationControlNumericUpDownMinutes.Value = minutes;
            DurationControlNumericUpDownSeconds.Value = seconds;
        }

        public DurationControl(TimeSpan _duration) : this()
        {
            DurationControlNumericUpDownDays.Value    = _duration.Days;
            DurationControlNumericUpDownHours.Value   = _duration.Hours;
            DurationControlNumericUpDownMinutes.Value = _duration.Minutes;
            DurationControlNumericUpDownSeconds.Value = _duration.Seconds;
        }

        public DurationControl()
        {
            InitializeComponent();

            DurationControlNumericUpDownDays.ValueChanged += DurationControlNumericUpDownDays_ValueChanged;
            DurationControlNumericUpDownHours.ValueChanged += DurationControlNumericUpDownHours_ValueChanged;
            DurationControlNumericUpDownMinutes.ValueChanged += DurationControlNumericUpDownMinutes_ValueChanged;
            DurationControlNumericUpDownSeconds.ValueChanged += DurationControlNumericUpDownSeconds_ValueChanged;

        }

        private void DurationControlNumericUpDownSeconds_ValueChanged(object sender, EventArgs e)
        {
            SecondsChanged?.Invoke(sender, e);
            DurationChanged?.Invoke(sender, e);
        }

        private void DurationControlNumericUpDownMinutes_ValueChanged(object sender, EventArgs e)
        {
            MinutesChanged?.Invoke(sender, e);
            DurationChanged?.Invoke(sender, e);
        }

        private void DurationControlNumericUpDownHours_ValueChanged(object sender, EventArgs e)
        {
            HoursChanged?.Invoke(sender, e);
            DurationChanged?.Invoke(sender, e);
        }

        private void DurationControlNumericUpDownDays_ValueChanged(object sender, EventArgs e)
        {
            DaysChanged?.Invoke(sender, e);
            DurationChanged?.Invoke(sender, e);
        }

    }
}
