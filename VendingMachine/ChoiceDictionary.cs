using System;

namespace VendingMachine
{
    public class ChoiceDictionary
    {
        private int[] _quantityKeys = { };
        private int[] _quantityValues = { };

        public ChoiceDictionary()
        {
        }

        public int GetValue(int quantityKey)
        {
            return _quantityValues[GetKeyIndex(quantityKey)];
        }

        private int GetKeyIndex(int quantityKey)
        {
            return Array.IndexOf(_quantityKeys, quantityKey);
        }

        public void SetValue(int quantityKey, int quantityValue)
        {
            _quantityValues[GetKeyIndex(quantityKey)] = quantityValue;
        }

        public void Add(int key, int value)
        {
            Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            _quantityKeys[_quantityKeys.Length - 1] = key;
            _quantityValues[_quantityValues.Length - 1] = value;
        }
    }
}