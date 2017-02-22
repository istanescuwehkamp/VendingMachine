using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<int> _choices = new List<int>();
        private int[] _quantityKeys = { };
        private int[] _quantityValues = { };
        private double _total;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _creditCard;
        private bool _valid;
        private int _choiceForCard;

        public double Total { get { return _total; } }

        public VendingMachine()
        {
        }

        public Can Deliver(int quantityKey)
        {
            var price = _prices.ContainsKey(quantityKey) ? _prices[quantityKey] : 0;
            if (!_choices.Contains(quantityKey) || GetValue(quantityKey) < 1 || _total < price)
            {
                return null;
            }

            SetValue(quantityKey, GetValue(quantityKey) - 1);
            _total -= price;
            return new Can { Type = quantityKey };
        }

        public void AddChoice(int c, int n = int.MaxValue)
        {
            Add(c, n);
            _choices.Add(c);
        }

        public void AddMultipleChoices(int[] choices, int[] counts)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                int c = choices[i];
                Add(c, counts[i]);
                _choices.Add(c);
            }
        }

        public void AddCoin(int value)
        {
            _total += value;
        }

        public double GiveChange()
        {
            var v = _total;
            _total = 0;
            return v;
        }

        public void AddPrice(int i, double v)
        {
            _prices[i] = v;
        }

        public void Stock(int choice, int quantity, double price)
        {
            Add(choice, quantity);
            _choices.Add(choice);
            _prices[choice] = price;
        }



        public double GetPrice(int choice)
        {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }

        public void GetPinNumber(int pinNumber)
        {
            _valid = new CreditCardModule.CreditCardModule(_creditCard).HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(int choice)
        {
            _choiceForCard = choice;
        }

        public Can DeliverChoiceForCard()
        {
            if (_valid && _choices.IndexOf(_choiceForCard) > -1 && GetValue(_choiceForCard) > 0)
            {
                SetValue(_choiceForCard, GetValue(_choiceForCard) - 1);

                return new Can { Type = _choiceForCard };
            }
            return null;
        }

        private int GetValue(int quantityKey)
        {
            return _quantityValues[GetKeyIndex(quantityKey)];
        }

        private int GetKeyIndex(int quantityKey)
        {
            return Array.IndexOf(_quantityKeys, quantityKey);
        }
        private void SetValue(int quantityKey, int quantityValue)
        {
            _quantityValues[GetKeyIndex(quantityKey)] = quantityValue;
        }
        private void Add(int key, int value)
        {
            Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            _quantityKeys[_quantityKeys.Length - 1] = key;
            _quantityValues[_quantityValues.Length - 1] = value;
        }

    }

    public class Can
    {
        public int Type { get; set; }
    }
}