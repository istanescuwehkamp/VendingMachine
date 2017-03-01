namespace CreditCardModule
{
    public class CreditCardModule
    {
        private readonly CreditCard _creditCard;

        public CreditCardModule(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }

        public bool HasValidPinNumber(int pinNumber)
        {
            return _creditCard.IsCorrectPinNumber(pinNumber);
        }
    }
}