using System.Collections.Generic;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<Choice> _choices = new List<Choice>();
        private Dictionary<Choice, int> _choiceQuantities = new Dictionary<Choice, int>();
        private double _total;
        private double _colaPrice;
        private Dictionary<Choice, double> _prices = new Dictionary<Choice, double>();
        private CreditCard _creditCard;
        private bool _isValidCard;
        private Choice _cardChoice;

        public double Total { get { return _total; } }

        public Can Deliver(Choice choice)
        {
            var price = _prices.ContainsKey(choice) ? _prices[choice] : 0;
            if (!_choices.Contains(choice) || _choiceQuantities[choice] < 1 || _total < price)
            {
                return null;
            }
            _choiceQuantities[choice] -= 1;
            _total -= price;
            return new Can { Type = choice };
        }

        public void AddChoice(Choice choice, int count = int.MaxValue)
        {
            _choiceQuantities.Add(choice, count);
            _choices.Add(choice);
        }

        public void AddCoin(int value)
        {
            _total += value;
        }

        public double PayOutChange()
        {
            var payout = _total;
            _total = 0;
            return payout;
        }

        public void AddPrice(Choice choice, double price)
        {
            _prices[choice] = price;
        }

        public double GetPrice(Choice choice)
        {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }

        public void GetPinNumber(int pinNumber)
        {
            var cardReader = new CreditCardModule.CreditCardModule(_creditCard);
            _isValidCard = cardReader.HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(Choice choice)
        {
            _cardChoice = choice;
        }

        public Can DeliverChoiceForCard()
        {
            var choice = _cardChoice;
            if (_isValidCard&&_choices.Contains(choice) && _choiceQuantities[choice] > 0)
            {
                _choiceQuantities[choice] -= 1;
                return new Can { Type = choice };
            }
            return null;
        }
    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}