using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System.config
{
    
    public class TransactionConfig:IEntityTypeConfiguration<Transaction>
    {
       
    
public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionNumber);
            builder.HasOne(t => t.AccountTrans)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

            builder.Property(t => t.Amount)
    .HasPrecision(18, 2); 
        }
    }
}
