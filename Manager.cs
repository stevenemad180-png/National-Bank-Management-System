using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System
{
    public class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }

        public int BranchCode { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
