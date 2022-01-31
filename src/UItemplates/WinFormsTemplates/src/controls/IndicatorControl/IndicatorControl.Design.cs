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
    public partial class IndicatorControl : UserControl
    {
        private TableLayoutPanel _indTable;
        private PictureBox _indPictureBox;
        private Label _indLabel;

        public void InitializeComponents()
        {
            _indTable = new TableLayoutPanel();
            _indPictureBox = new PictureBox();
            _indLabel = new Label();

            _indLabel.SuspendLayout();
            _indPictureBox.SuspendLayout();
            _indTable.SuspendLayout();
            SuspendLayout();


            // _indLabel

            _indLabel.AutoSize = true;
            _indLabel.TextAlign = ContentAlignment.TopLeft;


            // _indPictureBox

            //_indPictureBox.Dock = DockStyle.Fill;
            _indPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _indPictureBox.Size = new Size(10, 10);

            // _indTable

            _indTable.Dock = DockStyle.Fill;

            _indTable.ColumnCount = 2;

            _indTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            _indTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            _indLabel.ImageAlign = ContentAlignment.MiddleCenter;
            _indLabel.TextAlign = ContentAlignment.TopLeft;

            _indTable.Controls.Add(_indLabel, 0, 0);
            _indTable.Controls.Add(_indPictureBox, 1, 0);

            // this

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            MinimumSize = new Size(150, 20);
            MaximumSize = new Size(200, 20);
            AutoSize = true;
            //Dock = DockStyle.Fill;

            Controls.Add(_indTable);
            _indLabel.ResumeLayout(false);
            _indPictureBox.ResumeLayout(false);
            _indTable.ResumeLayout(false);
            ResumeLayout(false);

        }

    } // public partial class IndicatorControl : UserControl
}     // namespace Regata.Core.UI.WinForms.Controls
