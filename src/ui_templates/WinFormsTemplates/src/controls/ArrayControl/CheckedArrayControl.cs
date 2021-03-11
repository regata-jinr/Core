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

using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Regata.Core.UI.WinForms.Controls
{
    public partial class CheckedArrayControl<T> : UserControl, ISingleCheckedArrayControl<T>, IMultiCheckedArrayControl<T>
    {
        private readonly CheckBox[] _checkBoxes;
        private T _seletedItem;
        private List<T> _seletedItems;
        private T[] _array;

        /// <summary>
        /// Return one and only selected element in case of MultiSelection is false and last selected element in case of MultiSelection is true.
        /// </summary>
        public T SelectedItem => _seletedItem;

        /// <summary>
        /// Returns array of selected items in case of multiselection and array with single element in case of single selection
        /// </summary>
        public T[] SelectedItems => _seletedItems.ToArray();

        public bool MultiSelection { get; set; }

        public event Action SelectionChanged;

        public FlowDirection FlowDirection
        {
            get
            {
                return flowLayoutPanel.FlowDirection;
            }
            set
            {
                flowLayoutPanel.FlowDirection = value;
            }
        }

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
            _seletedItem = default;
            _array = array;
            _seletedItems = new List<T>();
            InitializeComponent();

            MultiSelection = multiSelection;

            _checkBoxes = new CheckBox[array.Length];
            for (var i = 0; i < array.Length; ++i)
            {
                _checkBoxes[i].Name = $"rb_{typeof(T).Name}_{i}";
                _checkBoxes[i].Dock = DockStyle.Fill;
                _checkBoxes[i].Text = array[i].ToString();
                _checkBoxes[i].TabIndex = i;
                _checkBoxes[i].CheckedChanged += CheckBox_CheckedChanged;
            }

            flowLayoutPanel.Controls.AddRange(_checkBoxes);

            RBV_groupBoxTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        public void ClearSelection()
        {
            foreach (var r in _checkBoxes)
            {
                r.CheckedChanged -= CheckBox_CheckedChanged;
                r.Checked = false;
                r.CheckedChanged += CheckBox_CheckedChanged;
            }
            _seletedItems.Clear();
            _seletedItem = default;
        }

        private void ClearOtherSelection(string nameOne)
        {
            foreach (var r in _checkBoxes)
            {
                if (r.Name == nameOne) continue;
                r.CheckedChanged -= CheckBox_CheckedChanged;
                r.Checked = false;
                r.CheckedChanged += CheckBox_CheckedChanged;
            }
        }

        private void CheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            var r = sender as CheckBox;

            if (!MultiSelection)
            {
                _seletedItems.Clear();
                ClearOtherSelection(r.Name);
            }

            SelectionChanged?.Invoke();

            _seletedItem = _array[r.TabIndex];
            _seletedItems.Add(_array[r.TabIndex]);
        }

    } // public partial class RadioButtonsView<T> : UserControl, IArrayControlSingleSelection<T>
}     // namespace Regata.Core.UI.WinForms.Controls
