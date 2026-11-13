using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System.config
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
    

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode);


            builder.Property(a => a.CurrentBalance)
                .HasPrecision(18, 2);

            builder.Property(a => a.AccountNumber)
    .ValueGeneratedNever();
        }
    }
}
