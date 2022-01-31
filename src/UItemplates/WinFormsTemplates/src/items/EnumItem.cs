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
using System.Linq;
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
        public event Action CheckedChanged;

        public void CheckItem(T itm)
        {
            foreach (ToolStripMenuItem item in EnumMenuItem.DropDownItems)
            {
                item.Text = item.Name;

                if (item.Name == itm.ToString())
                {
                    item.Checked = true;
                    EnumStatusLabel.Name = item.Name;
                    EnumStatusLabel.Text = item.Text;
                    //EnumStatusLabel.Text = $"{itm}||";
                }
                else
                    item.Checked = false;
            }
        }

        public T CheckedItem => (T)Enum.Parse(typeof(T), ToString());

        public override string ToString() => EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Where(i => i.Checked).First().Name;

        private string _enumName;

        public EnumItem()
        {
            var values = Enum.GetValues(typeof(T));
            _enumName = typeof(T).Name;
            EnumMenuItem = new ToolStripMenuItem();
            EnumStatusLabel = new ToolStripStatusLabel();
            EnumStatusLabel.Alignment = ToolStripItemAlignment.Left;

            EnumMenuItem.CheckOnClick = false;

            foreach (var val in values)
            {
                var val_name = val.ToString();
                EnumMenuItem.DropDownItems.Add(new ToolStripMenuItem { CheckOnClick = true, Name = val_name, Checked = false, AutoSize = true });
                EnumMenuItem.DropDownItems[val_name].Click += CheckHandler;

                //if (val_name == currentItem.ToString())
                //EnumMenuItem.DropDownItems[val_name].PerformClick();
            }

            //EnumStatusLabel.Name = $"{_enumName}StatusLabel";
            EnumMenuItem.Name = $"{_enumName}MenuItem";
            EnumMenuItem.Text = $"{_enumName}MenuItem";

        }

        public EnumItem(T currentItem) : this()
        {
            CheckItem(currentItem);
        }

        private void CheckHandler(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            CheckItem((T)Enum.Parse(typeof(T), currentItem.Name));
            CheckedChanged?.Invoke();
        }
    } // public class EnumControl<T>
}     // namespace Measurements.UI.Desktop.Components
