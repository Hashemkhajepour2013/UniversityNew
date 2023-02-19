using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Entities;

namespace University.Persistence.EF.Lessons;

public class LessonEntityMap: IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(_ => _.Title).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.Coefficient).IsRequired();
    }
}