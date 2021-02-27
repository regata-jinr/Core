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
using System.Collections.Generic;
using System.Linq;

namespace Regata.Core.Collections
{
    public class CircleArray<T>
    {
        private T[] _arr;
        private int _ind;

        public CircleArray(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            _arr = collection.ToArray();
        }

        public int Length => _arr.Length;

        public T Current => _arr[_ind];
        
        public event Action Moved;
        
        public T[] ToArray() => _arr;

        public bool MoveForward()
        {
            _ind++;
            if (_ind >= _arr.Length)
                _ind = 0;
            
            Moved?.Invoke();
            
            return true;
        }

        public bool MoveBack()
        {
            _ind--;
            if (_ind < 0)
                _ind = Length - 1;
            
            Moved?.Invoke();

            return true;
        }

        public void Reset()
        {
            _ind = 0;
        }

        public void Dispose()
        {
            _ind = 0;
        }

    } // public class CircleArray<T>
}     // namespace Regata.Core.Collections
