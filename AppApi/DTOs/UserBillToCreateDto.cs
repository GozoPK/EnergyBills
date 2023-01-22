using System.ComponentModel.DataAnnotations;
using AppApi.Helpers;

namespace AppApi.DTOs
{
    public class UserBillToCreateDto
    
    {
        [Required(ErrorMessage = "Το πεδίο «Αριθμός Λογαριασμού» είναι απαραίτητο")]
        [StringLength(10, ErrorMessage = "Μη έγκυρος αριθμός λογαριασμού")]
        public string BillNumber { get; set; }

        [Required]
        public Month Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public BillType Type { get; set; }

        [Required(ErrorMessage = "Το πεδίο «Ποσό» είναι απαραίτητο")]
        [Range(0, 9999999999999.99, ErrorMessage = "Μη έγκυρο ποσό")]
        public decimal Ammount { get; set; }

        [Required]
        public State State { get; set; }
    }
}