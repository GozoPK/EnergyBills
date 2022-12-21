namespace BankApi.DTOs
{
    public class AccountDto
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public ICollection<TransactionToReturnDto> Transactions { get; set; }
    }
}