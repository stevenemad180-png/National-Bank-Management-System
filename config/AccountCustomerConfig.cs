
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace National_Bank_Management_System.config
{
    public class AccountCustomerConfig
        : IEntityTypeConfiguration<AccountCustomer>
    {
        public void Configure(
            EntityTypeBuilder<AccountCustomer> builder)
        {
            // Composite Primary Key
            builder.HasKey(ac => new
            {
                ac.CustomerId,
                ac.AccountNumber
            });

            // Customer -> AccountCustomer
            builder.HasOne(ac => ac.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(ac => ac.CustomerId);

            // Account -> AccountCustomer
            builder.HasOne(ac => ac.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ac => ac.AccountNumber);
        }
    }
}
