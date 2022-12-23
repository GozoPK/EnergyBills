using System.ComponentModel.DataAnnotations;

namespace BankApi.Entities
{
    public class Transaction
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string TransactionNumber { get; set; } = (Guid.NewGuid()).ToString().Replace("-", "").Substring(1, 20).ToUpper();

        [Required]
        [Range(0, 9999999999999.99)]
        public decimal Amount { get; set; }

        [Required]
        public TransactionTypes TransactionType { get; set; }

        [Required]
        [StringLength(27)]
        public string SourceAccount { get; set; }

        [Required]
        public string SourceAccountName { get; set; }

        [Required]
        public bool IsExternalBank { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(2);

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }

    public enum TransactionTypes
    {
        Debit,
        Credit
    }
}