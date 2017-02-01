namespace CreditCardModule
{
    public class CreditCardModule
    {
        private CreditCard _creditCard;

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