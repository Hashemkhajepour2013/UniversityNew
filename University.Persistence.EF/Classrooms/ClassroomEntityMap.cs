using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.Classrooms;

public class ClassroomEntityMap: IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        
        builder.ToTable("Classrooms");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.LessonId).IsRequired();
        builder.Property(_ => _.ProfessorId).IsRequired();
        builder.Property(_ => _.StartDate).IsRequired();
        builder.Property(_ => _.EndDate).IsRequired();
        builder.Property(_ => _.Capacity).IsRequired();
        builder.Property(_ => _.TermId).IsRequired();
        
        builder.HasMany(_ => _.StudentClassrooms)
            .WithOne(_ => _.Classroom)
            .HasForeignKey(_ => _.ClassroomId);
    }
}