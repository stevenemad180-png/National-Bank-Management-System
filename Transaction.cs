using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System
{
    public class Transaction
    {
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;

        public string note { get; set; } = null!;

        public Account AccountTrans { get; set; } = null!;
        public int AccountNumber { get; set; } 
    }
}
