using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "To πεδίο «Χρήστης» είναι απαραίτητο")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Κωδικός» είναι απαραίτητο")]
        public string Password { get; set; }
    }
}