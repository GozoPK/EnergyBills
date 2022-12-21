using System.ComponentModel.DataAnnotations;

namespace AppApi.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(9)]
        public string Afm { get; set; }

        public string AddressStreet { get; set; }

        [MinLength(1)]
        [MaxLength(4)]
        public string AddressNumber { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }
        public string City { get; set; }

        [Range(0, 9999999999999.99)]
        public decimal AnnualIncome { get; set; }

        [MinLength(27)]
        [MaxLength(27)]
        public string Iban { get; set; }
    }
}