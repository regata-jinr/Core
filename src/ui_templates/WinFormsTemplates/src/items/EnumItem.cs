/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2018-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Items
{
    /// <summary>
    /// Control provide the opportunity to wrap enumeration to MenuItem strip and Status label for a statustrip
    /// </summary>
    public class EnumItem<T>
        where T : Enum
    {
        public ToolStripMenuItem EnumMenuItem;
        public ToolStripStatusLabel EnumStatusLabel;
        public event Action<string> CheckedChanged;
        public string CheckedItemText { get; set; }
  
        private string _enumName;

        public EnumItem()
        {
            var values = Enum.GetValues(typeof(T));
            _enumName = typeof(T).Name;
            EnumMenuItem = new ToolStripMenuItem();
            EnumStatusLabel = new ToolStripStatusLabel();

            EnumMenuItem.CheckOnClick = false;

            foreach (var val in values)
            {
                var val_name = $"{_enumName}_{val}";
                EnumMenuItem.DropDownItems.Add(new ToolStripMenuItem { CheckOnClick = true, Name =  val_name});
                EnumMenuItem.DropDownItems[val_name].Click += CheckHandler;
            }

            EnumStatusLabel.Name = $"{_enumName}StatusLabel";
            EnumMenuItem.Name = $"{_enumName}MenuItem";

        }

        private void CheckHandler(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;

            foreach (ToolStripMenuItem item in EnumMenuItem.DropDownItems)
            {
                if (item == currentItem)
                {
                    item.Checked = true;
                    EnumStatusLabel.Text = $"{currentItem.Text}||";
                    CheckedItemText = $"{_enumName}: {currentItem.Text}||";
                    CheckedChanged?.Invoke(currentItem.Text);
                }
                else
                    item.Checked = false;
            }
        }
    } // public class EnumControl<T>
}     // namespace Measurements.UI.Desktop.Components
