/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/
using Regata.Core.UI.WinForms.Controls;
using System.Drawing;
using System.Windows.Forms;


namespace Regata.Core.UI.WinForms.Forms
{
    public partial class ShareSpectra
    {

        private ListView listView1;
        private ToolStripMenuItem chooseFilesButton;
        private ToolStripMenuItem uploadAllButton;
        private ToolStripMenuItem uploadNotExistButton;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem CancelMenuButton;
        private ControlsGroupBox groupBoxMain;

        private void InitializeComponents()
        {
            listView1 = new ListView();
            chooseFilesButton = new ToolStripMenuItem();
            uploadAllButton = new ToolStripMenuItem();
            uploadNotExistButton = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            CancelMenuButton = new ToolStripMenuItem();
            groupBoxMain = new ControlsGroupBox(new Control[] { listView1 });
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            listView1.HideSelection = false;
            listView1.Location = new Point(12, 76);
            listView1.Name = "listView1";
            listView1.Size = new Size(1020, 368);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // chooseFilesButton
            // 
            chooseFilesButton.Name = "chooseFilesButton";
            chooseFilesButton.Size = new Size(194, 58);
            //chooseFilesButton.Text = "Выбрать файлы для загрузки";
            chooseFilesButton.Click += new System.EventHandler(chooseFilesButton_Click);
            // 
            // uploadAllButton
            // 
            uploadAllButton.Name = "uploadAllButton";
            uploadAllButton.Size = new Size(344, 58);
            //uploadAllButton.Text = "Загрузить и перезаписать все выбранные файлы в облачное хранилище";
            uploadAllButton.Click += new System.EventHandler(uploadAllButton_Click);
            // 
            // uploadNotExistButton
            // 
            uploadNotExistButton.Name = "uploadNotExistButton";
            uploadNotExistButton.Size = new Size(226, 58);
            //uploadNotExistButton.Text = "Загрузить только недостающие файлы";
            uploadNotExistButton.Click += new System.EventHandler(uploadNotExistButton_Click);
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Spectra files|*.cnf";
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            // 
            // CancelMenuButton
            // 
            CancelMenuButton.Name = "CancelMenuButton";
            CancelMenuButton.Size = new Size(238, 58);
            //CancelMenuButton.Text = "Отменить загрузку";
            CancelMenuButton.Click += new System.EventHandler(CancelMenuButton_Click);
            // 
            // FaceForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 450);
            MenuStrip.Items.Insert(0, CancelMenuButton);
            MenuStrip.Items.Insert(0,uploadNotExistButton);
            MenuStrip.Items.Insert(0,uploadAllButton);
            MenuStrip.Items.Insert(0,chooseFilesButton);
            Controls.Add(groupBoxMain);
            Name = "ShareSpectraForm";
            //Text = "Загрузка спектров в облачное хранилище";
            ResumeLayout(false);

        }


    }
}

