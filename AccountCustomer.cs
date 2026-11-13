using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System
{
    public class AccountCustomer
    {
        // Composite Primary Key + Foreign Keys
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }

        // Relationship Attributes
        public string AccountStatus { get; set; } = null!;
        public DateTime OwnershipStartDate { get; set; }
        public string OwnershipType { get; set; } = null!;

        // Navigation Properties
        public Account Account { get; set; } = null!;
        public Customer Customer { get; set; } = null!;     
    }
}
