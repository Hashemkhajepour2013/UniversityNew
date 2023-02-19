using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.Students;

public sealed class StudentEntityMap : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.LastName).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.Mobile).HasMaxLength(11).IsRequired();
        builder.Property(_ => _.StudentNumber).HasMaxLength(10).IsRequired();
        
        builder.HasMany(_ => _.StudentClassrooms)
            .WithOne(_ => _.Student)
            .HasForeignKey(_ => _.StudentId);
    }
}