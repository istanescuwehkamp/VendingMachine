using System.Collections.Generic;
using CreditCardModule;

namespace VendingMachine {
    public class VendingMachine {
        private readonly List<int> _choices = new List<int>();
        private double _totalAmount;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _creditCard;
        private bool _valid;
        private int _choiceForCard;
      
        public double TotalAmount { get { return _totalAmount; } }
        private readonly Dictionary<int, int> _quantity  = new Dictionary<int, int>();

        public Can Deliver(int value) {
            var price = _prices.ContainsKey(value) ? _prices[value] : 0;
            if (!_choices.Contains(value) || _quantity[value] < 1 || _totalAmount < price) {
                return null;
            }
            var quantityValue = _quantity[value] - 1;
            _quantity[value] = quantityValue;
            _totalAmount -= price;
            return new Can { Type = value };
        }

        public void AddChoice(int choice, int quantity = int.MaxValue) {
            _quantity.Add(choice, quantity);
            _choices.Add(choice);
        }

        public void AddMultipleChoices(int[] choices, int[] quantities) {
            for (int i = 0; i < choices.Length; i++) {
                int choice = choices[i];
                _quantity.Add(choice, quantities[i]);
                _choices.Add(choice);
            }
        }

        public void AddCoin(int amount) {
            _totalAmount += amount;
        }

        public double Change() {
            var change = _totalAmount;
            _totalAmount = 0;
            return change;
        }

        public void AddPrice(int choice, double price) {
            _prices[choice] = price;
        }

        public void Stock(int choice, int quantity, double price) {
            _quantity.Add(choice, quantity);
            _choices.Add(choice);
            _prices[choice] = price;
        }


        public double GetPrice(int choice) {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard creditCard) {
            _creditCard = creditCard;
        }

        public void GetPinNumber(int pinNumber) {
            _valid = new CreditCardModule.CreditCardModule(_creditCard).HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(int choice) {
            _choiceForCard = choice;
        }

        public Can DeliverChoiceForCard() {

            if (_valid && _choices.IndexOf(_choiceForCard) > -1 && _quantity[_choiceForCard] > 0) {
                var quantityValue = _quantity[_choiceForCard] - 1;
                _quantity[_choiceForCard] = quantityValue;

                return new Can { Type = _choiceForCard };
            }

            return null;
        }
    }

    public class Can {
        public int Type { get; set; }
    }
}