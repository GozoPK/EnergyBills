using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppApi.Helpers;

namespace AppApi.Entities
{
    [Table("Bills")]
    public class UserBill
    {
        public Guid Id { get; set; }
        public string BillNumber { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public decimal Ammount { get; set; }
        public decimal AmmountToReturn { get; set; } = 0;
        public Status Status { get; set; } = Status.Pending;
        public BillType Type { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public UserEntity UserEntity { get; set; }
        public string UserEntityId { get; set; }
    }
}