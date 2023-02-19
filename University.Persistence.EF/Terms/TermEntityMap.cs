using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.Terms;

public class TermEntityMap: IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.ToTable("Terms");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.Title).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.UnitCount).IsRequired();


        builder.HasMany(_ => _.Classrooms)
            .WithOne(_ => _.Term)
            .HasForeignKey(_ => _.TermId);
    }
}