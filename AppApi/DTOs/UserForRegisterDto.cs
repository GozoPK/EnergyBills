using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "To πεδίο «Χρήστης» είναι απαραίτητο")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Email» είναι απαραίτητο")]
        [EmailAddress(ErrorMessage = "Μη έγκυρη διεύθυνση e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Μη έγκυρος αριθμός τηλεφώνου")]
        [RegularExpression(@"^69\d+$", ErrorMessage = "Μη έγκυρος αριθμός τηλεφώνου")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Ονομα» είναι απαραίτητο")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Επώνυμο» είναι απαραίτητο")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Μη έγκυρο Α.Φ.Μ.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Μη έγκυρο Α.Φ.Μ.")]
        public string Afm { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Οδός» είναι απαραίτητο")]
        public string AddressStreet { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Αριθμός» είναι απαραίτητο")]
        [StringLength(4, MinimumLength = 1, ErrorMessage = "Μη έγκυρος αριθμός οδού")]
        public string AddressNumber { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Τ.Κ.» είναι απαραίτητο")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Μη έγκυρο Τ.Κ.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Πόλη» είναι απαραίτητο")]
        public string City { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Εισόδημα» είναι απαραίτητο")]
        [Range(0, 9999999999999.99, ErrorMessage = "Μη έγκυρο εισόδημα")]
        public decimal AnnualIncome { get; set; }

        [Required(ErrorMessage = "Το πεδίο «IBAN» είναι απαραίτητο")]
        [StringLength(27, MinimumLength = 27, ErrorMessage = "Μη έγκυρο IBAN.")]
        [RegularExpression(@"^GR\d+$", ErrorMessage = "Μη έγκυρο IBAN.")]
        public string Iban { get; set; }

        [Required]
        public string TaxisnetToken { get; set; }
    }
}