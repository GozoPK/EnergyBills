using System.ComponentModel.DataAnnotations;
using AppApi.Helpers;

namespace AppApi.DTOs
{
    public class UserBillToCreateDto
    {
        [Required]
        public string BillNumber { get; set; }

        [Required]
        public Month Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Ammount { get; set; }
    }
}