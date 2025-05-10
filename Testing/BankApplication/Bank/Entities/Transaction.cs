using System.ComponentModel.DataAnnotations;

namespace UnitTestingProj.Bank.Entities
{
    public class Transaction
    { 
        [Key]
        public Guid TransactionNumber { get; set; } 
        public Account Account { get; set; }
        public Guid AccountNumber { get; set; }
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
