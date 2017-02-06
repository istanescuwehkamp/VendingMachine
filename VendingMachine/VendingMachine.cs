using System;
using System.Collections.Generic;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<int> _choices = new List<int>();
        private Dictionary<int, int> _quantities = new Dictionary<int, int>();
        private double t;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _cc;
        private bool _valid;
        private int ccc;

        public double T { get { return t; } }

        public Can Deliver(int value)
        {
            var price = _prices.ContainsKey(value) ? _prices[value] : 0;
            if (!_choices.Contains(value) || _quantities[value] < 1 || t < price)
            {
                return null;
            }
            _quantities[value] -= 1;
            t -= price;
            return new Can { Type = value };
        }

        public void AddChoice(int c, int n = int.MaxValue)
        {
            _quantities.Add(c, n);
            _choices.Add(c);
        }

        public void AddCoin(int v)
        {
            t += v;
        }

        public double PayOutChange()
        {
            var v = t;
            t = 0;
            return v;
        }

        public void AddPrice(int i, double v)
        {
            _prices[i] = v;
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
            if (_valid && _choices.IndexOf(c) > -1 && _quantities[c] > 0)
            {
                _quantities[c] -= 1;
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