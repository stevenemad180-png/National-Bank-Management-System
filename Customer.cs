using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string NationalId { get; set; } = null!;
   public string cusstomerType { get; set; } = null!;

        // Navigation property for the related accounts
        public ICollection<AccountCustomer> Accounts { get; set; } = new List<AccountCustomer>();

    }
}
