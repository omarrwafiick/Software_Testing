using System.ComponentModel.DataAnnotations;

namespace UnitTestingProj.Bank.Entities
{
    public class Account
    {
        [Key]
        public Guid AccountNumber { get; set; }
        public bool IsFrozen { get; set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; } = new();

        public Account(decimal balance)
        {
            AccountNumber = Guid.NewGuid();
            Deposit(balance);
        }
        public Account()
        { 
        }
        public void SetBalance(decimal newBalance)
        {
            Balance = newBalance;
        }

        public Account(Guid accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            AccountNumber = Guid.NewGuid();
            Deposit(balance);
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
