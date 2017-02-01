namespace CreditCardModule
{
    public class CreditCard
    {
        private string _holderName;
        private string _serialNumber;
        private int _pinNumber;

        public CreditCard(string holderName, string serialNumber, string cardCompany, int pinNumber)
        {
            _holderName = holderName;
            _serialNumber = serialNumber;
            _pinNumber = pinNumber;
        }

        public bool IsCorrectPinNumber(int pinNumber)
        {
            return _pinNumber == pinNumber;
        }
    }
}