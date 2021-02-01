/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

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
                //DurationChanged?.Invoke(this, new EventArgs());

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
            SecondsChanged?.Invoke(this, e);
            DurationChanged?.Invoke(this, e);
        }

        private void DurationControlNumericUpDownMinutes_ValueChanged(object sender, EventArgs e)
        {
            MinutesChanged?.Invoke(this, e);
            DurationChanged?.Invoke(this, e);
        }

        private void DurationControlNumericUpDownHours_ValueChanged(object sender, EventArgs e)
        {
            HoursChanged?.Invoke(this, e);
            DurationChanged?.Invoke(this, e);
        }

        private void DurationControlNumericUpDownDays_ValueChanged(object sender, EventArgs e)
        {
            DaysChanged?.Invoke(this, e);
            DurationChanged?.Invoke(this, e);
        }

    }
}
