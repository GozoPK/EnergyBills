namespace TaxisNetSimApi.DTOs
{
    public class UserToReturnDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Afm { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public decimal AnnualIncome { get; set; }
    }
}