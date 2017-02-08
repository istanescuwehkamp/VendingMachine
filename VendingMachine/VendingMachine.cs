using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<int> _choices = new List<int>();
        private int[] _quantityKeys = {};
        private int[] _quantityValues = {};
        private double t;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _cc;
        private bool _valid;
        private int ccc;

        public double T { get { return t; } }

        public VendingMachine()
        {
        }

        public Can Deliver(int value)
        {
            var price = _prices.ContainsKey(value) ? _prices[value] : 0;
            if (!_choices.Contains(value) || _quantityValues[Array.IndexOf(_quantityKeys, value)] < 1 || t < price)
            {
                return null;
            }

            _quantityValues[Array.IndexOf(_quantityKeys, value)] = _quantityValues[Array.IndexOf(_quantityKeys, value)]-1;
            t -= price;
            return new Can { Type = value };
        }

        public void AddChoice(int c, int n = int.MaxValue)
        {
            Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            _quantityKeys[_quantityKeys.Length - 1] = c;
            _quantityValues[_quantityValues.Length - 1] = n;
            _choices.Add(c);
        }

        public void AddMultipleChoices(int[] choices, int[] counts)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                int c = choices[i];
                Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
                Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
                _quantityKeys[_quantityKeys.Length - 1] = c;
                _quantityValues[_quantityValues.Length - 1] = counts[i];
                _choices.Add(c);
            }
        }

        public void AddCoin(int v)
        {
            t += v;
        }

        public double Change()
        {
            var v = t;
            t = 0;
            return v;
        }

        public void AddPrice(int i, double v)
        {
            _prices[i] = v;
        }

        public void Stock(int choice, int quantity, double price)
        {
            Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            _quantityKeys[_quantityKeys.Length - 1] = choice;
            _quantityValues[_quantityValues.Length - 1] = quantity;
            _choices.Add(choice);
            _prices[choice] = price;
        }


        public double GetPrice(int choice)
        {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard myCC)
        {
            _cc = myCC;
        }

        public void GetPinNumber(int pinNumber)
        {
            _valid = new CreditCardModule.CreditCardModule(_cc).HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(int choice)
        {
            ccc = choice;
        }

        public Can DeliverChoiceForCard()
        {
            var c = ccc;
            if (_valid && _choices.IndexOf(c) > -1 && _quantityValues[Array.IndexOf(_quantityKeys, c)] > 0)
            {
                _quantityValues[Array.IndexOf(_quantityKeys, c)] = _quantityValues[Array.IndexOf(_quantityKeys, c)]-1;
                return new Can {Type = c};
            }
            else
            {
                return null;
            }
        }
    }

    public class Can
    {
        public int Type { get; set; }
    }
}