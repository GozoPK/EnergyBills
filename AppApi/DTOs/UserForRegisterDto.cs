using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(9)]
        public string Afm { get; set; }

        [Required]
        public string AddressStreet { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(4)]
        public string AddressNumber { get; set; }

        [Required]
        [StringLength(5)]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Range(0, 9999999999999.99)]
        public decimal AnnualIncome { get; set; }

        [Required]
        [MinLength(27)]
        [MaxLength(27)]
        public string Iban { get; set; }

        [Required]
        public string TaxisnetToken { get; set; }
    }
}