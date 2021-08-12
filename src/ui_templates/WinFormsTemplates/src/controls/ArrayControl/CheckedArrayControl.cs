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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Core.UI.WinForms.Controls
{
    public partial class CheckedArrayControl<T> : UserControl, ISingleCheckedArrayControl<T>, IMultiCheckedArrayControl<T>
    {
        private readonly List<CheckBox> _checkBoxes;
        private T _selectedItem;
        private List<T> _selectedItems;
        private List<T> _array;

        /// <summary>
        /// Return one and only selected element in case of MultiSelection is false and last selected element in case of MultiSelection is true.
        /// </summary>
        public T SelectedItem => _selectedItem;

        /// <summary>
        /// Returns array of selected items in case of multiselection and array with single element in case of single selection
        /// </summary>
        public T[] SelectedItems => _selectedItems.ToArray();

        public bool IsSelected(T elem)
        {
            return _selectedItems.Contains(elem);
        }

        public bool MultiSelection { get; set; }

        public event Action<CheckedArrayControl<T>> SelectionChanged;

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
            _selectedItem = default;
            _array = new List<T>(array.Length);
            _array.AddRange(array);
            _selectedItems = new List<T>();
            InitializeComponent();

            MultiSelection = multiSelection;

            _checkBoxes = new List<CheckBox>(array.Length);
            for (var i = 0; i < array.Length; ++i)
            {
                _checkBoxes.Add(CreateCheckBox(array[i]?.ToString()));
            }

            Dock = DockStyle.Fill;

            RBV_groupBoxTitle.ResumeLayout(false);
            flowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        private CheckBox CreateCheckBox(string text)
        {
            var cb = new CheckBox();
            cb.Name = text;
            cb.AutoSize = true;
            cb.UseVisualStyleBackColor = true;
            cb.Dock = DockStyle.Fill;
            cb.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cb.Text = text;
            cb.CheckedChanged += CheckBox_CheckedChanged;
            SetTooltip(ref cb);
            flowLayoutPanel.Controls.Add(cb);

            return cb;
        }

        private void SetTooltip(ref CheckBox cb)
        {
            var toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(cb, cb.Text);
        }

        public void ClearSelection()
        {
            foreach (var r in _checkBoxes)
            {
                r.CheckedChanged -= CheckBox_CheckedChanged;
                r.Checked = false;
                r.CheckedChanged += CheckBox_CheckedChanged;
            }
            _selectedItems.Clear();
            _selectedItem = default;
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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var r = sender as CheckBox;

            if (!MultiSelection)
            {
                _selectedItems.Clear();
                ClearOtherSelection(r.Name);
            }

            if (r.Checked)
            {
                _selectedItem = _array.Find(a => a.ToString() == r.Name);
                _selectedItems.Add(_selectedItem);
            }
            else
            {
                _selectedItems.Remove(_selectedItem);
                if (_selectedItems.Count >= 1)
                    _selectedItem = _selectedItems[0];
            }
            
            SelectionChanged?.Invoke(this);
        }

        public void Add(T elem)
        {
            _array.Add(elem);
            _checkBoxes.Add(CreateCheckBox(elem?.ToString()));
        }

        public void Remove(T elem)
        {
            var i = _checkBoxes.IndexOf(_checkBoxes.Where(c => c.Text == elem.ToString()).First());
            _checkBoxes[i].CheckedChanged -= CheckBox_CheckedChanged;
            _checkBoxes.RemoveAt(i);
            _array.Remove(elem);
            flowLayoutPanel.Refresh();
        }

        public void Hide(T elem)
        {
            flowLayoutPanel.Controls.OfType<CheckBox>().Where(c => c.Text == elem.ToString()).First().Visible = false;
        }

        public void Show(T elem)
        {
            flowLayoutPanel.Controls.OfType<CheckBox>().Where(c => c.Text == elem.ToString()).First().Visible = true;
        }

    } // public partial class RadioButtonsView<T> : UserControl, IArrayControlSingleSelection<T>
}     // namespace Regata.Core.UI.WinForms.Controls
