using BankApi.Entities;

namespace BankApi.DTOs
{
    public class TransactionForCreationDto
    {
        public decimal Amount { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string SourceAccount { get; set; }
        public string SourceAccountName { get; set; }
        public bool IsExternalBank { get; set; }
    }
}