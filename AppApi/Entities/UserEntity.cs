using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AppApi.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(9)]
        public string Afm { get; set; }

        public string AddressStreet { get; set; }

        [MinLength(1)]
        [MaxLength(4)]
        public string AddressNumber { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public decimal AnnualIncome { get; set; }

        [MinLength(27)]
        [MaxLength(27)]
        public string Iban { get; set; }
        public string RoleId { get; set; }
        public UserRole Role { get; set; }
        public ICollection<UserBill> UserBills { get; set; } = new List<UserBill>();
    }
}