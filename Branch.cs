using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System
{
    public class Branch
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Manager Manager { get; set; } = null!;
        // Navigation property for the related accounts
       public ICollection<Account> Accounts { get; set; } = new List<Account>();
       
       
    }
}
