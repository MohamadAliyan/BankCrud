using BankCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCrud.Repository.Maps;

public class BankConfiguration : BaseConfiguration<Bank>
{
    public override void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.ToTable("Banks");
        builder.Property(e => e.Address)
            .HasColumnType("nvarchar(100)")
            .IsRequired(true);

        builder.HasMany(p => p.Branches)
            .WithOne(p => p.Bank)
            .HasForeignKey(p => p.BankId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);


        base.Configure(builder);

    }

}
