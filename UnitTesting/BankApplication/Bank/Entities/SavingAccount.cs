namespace UnitTestingProj.Bank.Entities
{
    public class SavingAccount : Account
    {
        private decimal _interestRate;
        public SavingAccount(decimal rate, decimal balance) : base(balance)
        {
            _interestRate = rate;
        }
        public void ApplyMonthlyInterest()
        {
            var result = Balance * _interestRate;
            Deposit(result);
        }

        public void SetInterest(decimal newRate)
        {
            _interestRate = newRate;
        }

        public decimal GetInterest() => _interestRate;
    }
}
