namespace AppApi.DTOs
{
    public class UserToReturnDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Afm { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public decimal AnnualIncome { get; set; }
        public string Iban { get; set; }
        public IEnumerable<UserBillToReturnDto> UserBills { get; set; }
    }
}