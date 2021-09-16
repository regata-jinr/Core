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

using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{
    public partial class CheckedArrayControl<T> : UserControl
    {
        /// <summary>
        /// Return one and only selected element in case of MultiSelection is false and last selected element in case of MultiSelection is true.
        /// </summary>
        public T SelectedItem => (T)checkedListBox.SelectedItem;


        public void SelectItem(T item)
        {
            checkedListBox.SelectedItem = item;
        }

        /// <summary>
        /// Returns array of selected items in case of multiselection and array with single element in case of single selection
        /// </summary>
        public T[] SelectedItems => checkedListBox.CheckedItems.Cast<T>().ToArray();

        public readonly bool MultiSelection;

        public event ItemCheckEventHandler SelectionChanged;


        public override string Text
        {
            get
            {
                return RBV_groupBoxTitle.Text;
            }
            set
            {
                RBV_groupBoxTitle.Text = value;
            }
        }

        public CheckedArrayControl(T[] array, bool multiSelection = false)
        {
            InitializeComponent();

            MultiSelection = multiSelection;

            for (var i = 0; i < array.Length; ++i)
            {
                checkedListBox.Items.Add(array[i]);
            }

            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            Dock = DockStyle.Fill;

            checkedListBox.ResumeLayout(false);
            RBV_groupBoxTitle.ResumeLayout(false);
            ResumeLayout(false);

        }

        private int lastCheckedIndex = -1;

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBox.ItemCheck -= CheckedListBox_ItemCheck;

            if (!MultiSelection)
            {
                if (e.Index != lastCheckedIndex)
                {
                    if (lastCheckedIndex != -1)
                        checkedListBox.SetItemCheckState(lastCheckedIndex, CheckState.Unchecked);
                    lastCheckedIndex = e.Index;
                }
            }
            SelectionChanged?.Invoke(sender, e);
            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

        }

        public void Add(T elem)
        {
            checkedListBox.Items.Add(elem);
        }

        public void Remove(T elem)
        {
            checkedListBox.Items.Remove(elem);
        }   


    } // public partial class RadioButtonsView<T> : UserControl, IArrayControlSingleSelection<T>
}     // namespace Regata.Core.UI.WinForms.Controls
