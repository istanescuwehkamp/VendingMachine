using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class MyDictionary<TKey, TValue>
    {
        public TKey[] _keys = { };
        public TValue[] _values = { };

        public new void Add(TKey key, TValue value)
        {
            Array.Resize(ref _keys, _keys.Length + 1);
            Array.Resize(ref _values, _values.Length + 1);
            _keys[_keys.Length - 1] = key;
            _values[_values.Length - 1] = value;
        }

        public void SetValue(TKey key, TValue value)
        {
            _values[Array.IndexOf(_keys, key)] = value;
        }

        public TValue GetValue(TKey key)
        {
            return _values[Array.IndexOf(_keys, key)];
        }

        public new bool ContainsKey(TKey key)
        {
            return Array.IndexOf(_keys, key) > -1;
        }
    }
    public class MyDictionary2<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TKey[] _keys = { };
        public TValue[] _values = { };

        public new void Add(TKey key, TValue value)
        {
            Array.Resize(ref _keys, _keys.Length + 1);
            Array.Resize(ref _values, _values.Length + 1);
            _keys[_keys.Length - 1] = key;
            _values[_values.Length - 1] = value;
        }

        public void SetValue(TKey key, TValue value)
        {
            _values[Array.IndexOf(_keys, key)] = value;
        }

        public TValue GetValue(TKey key)
        {
            return _values[Array.IndexOf(_keys, key)];
        }

    }
}