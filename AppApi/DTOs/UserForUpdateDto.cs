using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "Το πεδίο «Email» είναι απαραίτητο")]
        [EmailAddress(ErrorMessage = "Μη έγκυρη διεύθυνση e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Μη έγκυρος αριθμός τηλεφώνου")]
        [RegularExpression(@"^69\d+$", ErrorMessage = "Μη έγκυρος αριθμός τηλεφώνου")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Το πεδίο «IBAN» είναι απαραίτητο")]
        [StringLength(27, MinimumLength = 27, ErrorMessage = "Μη έγκυρο IBAN.")]
        [RegularExpression(@"^GR\d+$", ErrorMessage = "Μη έγκυρο IBAN.")]
        public string Iban { get; set; }
    }
}