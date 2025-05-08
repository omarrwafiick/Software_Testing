
namespace UnitTestingProj.Bank
{
    public class Account
    {
        public bool IsFrozen { get; set; }
        public Guid AccountNumber { get; set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; } = new();

        public Account(decimal balance)
        {
            AccountNumber = Guid.NewGuid();
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            if (IsFrozen) throw new InvalidOperationException("Account is frozen.");
            if (amount <= 0) throw new ArgumentException("Deposit must be positive.");
            Balance += amount;
            Transactions.Add(new Transaction(amount, TransactionType.Deposit, ""));
        }

        public void Withdraw(decimal amount)
        {
            if (IsFrozen) throw new InvalidOperationException("Account is frozen.");
            if (amount <= 0) throw new ArgumentException("Withdraw must be positive.");
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
            Transactions.Add(new Transaction(amount, TransactionType.Withdraw, ""));
        }
    }
}
