namespace UnitTestingProj.Bank.Entities
{
    public class SavingAccount : Account
    {
        public decimal InterestRate {  get; set; }
        public SavingAccount()
        { 
        }
        public SavingAccount(decimal rate, decimal balance) : base(balance)
        {
            InterestRate = rate;
        }
        public void ApplyMonthlyInterest()
        {
            var result = Balance * InterestRate;
            Deposit(result);
        }
         
    }
}
