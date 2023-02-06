using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class AdminForCreationDto
    {
        [Required(ErrorMessage = "To πεδίο «Χρήστης» είναι απαραίτητο")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Email» είναι απαραίτητο")]
        [EmailAddress(ErrorMessage = "Μη έγκυρη διεύθυνση e-mail")]
        public string Email { get; set; }
    }
}