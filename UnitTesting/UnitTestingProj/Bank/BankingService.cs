
namespace UnitTestingProj.Bank
{
    public static class BankingService
    {
        public static void TransferFunds(Account from, Account to, decimal amount)
        {
            from.Withdraw(amount);
            to.Deposit(amount);
        }
    }
}
