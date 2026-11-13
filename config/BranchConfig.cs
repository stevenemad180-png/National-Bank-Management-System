using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace National_Bank_Management_System.config
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>

    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
           builder.HasKey(b => b.Code);
            builder.HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Manager>(m => m.BranchCode);
            builder.Property(b => b.Code)
    .ValueGeneratedNever();
        }
    }
}
