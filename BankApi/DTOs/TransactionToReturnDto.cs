using BankApi.Entities;

namespace BankApi.DTOs
{
    public class TransactionToReturnDto
    {
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string SourceAccountName { get; set; }
        public DateTime Date { get; set; }
    }
}