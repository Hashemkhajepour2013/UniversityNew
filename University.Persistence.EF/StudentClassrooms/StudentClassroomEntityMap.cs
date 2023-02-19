using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.StudentClassrooms;

public sealed class StudentClassroomEntityMap : IEntityTypeConfiguration<StudentClassroom>
{
    public void Configure(EntityTypeBuilder<StudentClassroom> builder)
    {
        builder.ToTable("StudentClassrooms")
            .HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.ClassroomId).IsRequired();
        builder.Property(_ => _.StudentId).IsRequired();
    }
}