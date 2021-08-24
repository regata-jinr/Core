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

using System.Drawing;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{
    public enum IndicatorState : ushort { Unknown, Bad, Good }
    public partial class IndicatorControl : UserControl
    {

        private IndicatorState _indicator;

        public IndicatorState Indicator
        {
            get
            {
                return _indicator;
            }
            set
            {
                _indicator = value;
                switch(_indicator)
                {
                    case (IndicatorState.Bad):
                        _indPictureBox.Image = ind_red;
                        break;
                    case (IndicatorState.Good):
                        _indPictureBox.Image = ind_green;
                        break;
                    default:
                        _indPictureBox.Image = ind_gray;
                        break;
                };
            }
        }


        public IndicatorControl()
        {
            InitializeComponents();
            Indicator = IndicatorState.Unknown;
        }

        public new string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                _indLabel.Name = value;
            }
        }

        public override string Text { get => _indLabel.Text; set => _indLabel.Text = value; }

    }
}
