using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Το πεδίο «Παλιός Κωδικός» είναι απαραίτητο")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Νέος Κωδικός» είναι απαραίτητο")]
        public string NewPassword { get; set; }
    }
}