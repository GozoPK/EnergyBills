using System.ComponentModel.DataAnnotations;

namespace BankApi.Entities
{
    public class Account
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(27)]
        public string IBAN { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(0, 9999999999999.99)]
        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}