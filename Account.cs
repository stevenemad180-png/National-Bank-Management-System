
using System;
using System.Collections.Generic;

namespace National_Bank_Management_System
{
    public class Account
    {
        // Primary Key
        public int AccountNumber { get; set; }

        public decimal CurrentBalance { get; set; }

        public string AccountType { get; set; } = null!;

        public DateTime OpeningDate { get; set; }

        // One-to-Many: Branch -> Accounts
        public int BranchCode { get; set; } 
        public Branch Branch { get; set; } = null!;

        // One-to-Many: Account -> Transactions
        public ICollection<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        // Many-to-Many: Account <-> Customer
        public ICollection<AccountCustomer> CustomerAccounts { get; set; }
            = new List<AccountCustomer>();
    }
}
