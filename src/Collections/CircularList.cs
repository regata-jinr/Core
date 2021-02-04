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

using System.Collections.Generic;
using System;
using System.Collections;

namespace Regata.Core.Collections
{
    public class CircularList<T> : IEnumerable<T>
    {
        private List<T> _list;

        public CircularList(IEnumerable<T> source)
        {
            _list = new List<T>(source);

            if (_list.Count == 0)
                throw new ArgumentException("Input collection should have elements.");
        }

        private int _curIndex = 0;

        public T CurrentItem => _list[_curIndex];

        public T NextItem
        {
            get
            {
                _curIndex++;

                if (_curIndex >= _list.Count)
                    _curIndex = 0;

                return CurrentItem;
            }
        }

        public T PrevItem
        {
            get
            {
                _curIndex--;

                if (_curIndex < 0)
                    _curIndex = _list.Count - 1;

                return CurrentItem;
            }
        }

        public T FirstItem => _list[0];
        public T LastItem => _list[_list.Count - 1];

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

    } // public class CircularList<T>
}     // namespace Regata.Core.Collections
