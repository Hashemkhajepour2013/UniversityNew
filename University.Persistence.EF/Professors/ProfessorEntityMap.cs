using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.Professors;

public sealed class ProfessorEntityMap :  IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("Professors");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.LastName).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.Mobile).HasMaxLength(11).IsRequired();
        builder.Property(_ => _.PersonnelId).HasMaxLength(10).IsRequired();
    }
}