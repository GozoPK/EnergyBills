using System.ComponentModel.DataAnnotations;
using AppApi.Helpers;

namespace AppApi.DTOs
{
    public class BillStatusUpdateDto
    {
        [Required]
        public Status Status { get; set; }
    }
}