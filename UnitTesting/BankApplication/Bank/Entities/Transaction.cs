namespace UnitTestingProj.Bank.Entities
{
    public class Transaction
    {
        public Transaction(decimal amount, TransactionType type, string description)
        {
            Amount = amount;
            Type = type;
            Description = description;
        }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
