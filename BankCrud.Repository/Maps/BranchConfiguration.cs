using BankCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCrud.Repository.Maps;

public class BranchConfiguration : BaseConfiguration<Branch>
{
    public override void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");
        builder.Property(e => e.Address)
            .HasColumnType("nvarchar(100)")
            .IsRequired(true);

        base.Configure(builder);

    }

}
