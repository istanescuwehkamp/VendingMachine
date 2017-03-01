namespace CreditCardModule
{
    public class CreditCard
    {
        private readonly int _pinNumber;

        public CreditCard(int pinNumber)
        {
            _pinNumber = pinNumber;
        }

        public bool IsCorrectPinNumber(int pinNumber)
        {
            return _pinNumber == pinNumber;
        }
    }
}