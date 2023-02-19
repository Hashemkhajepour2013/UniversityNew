using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182041)]
public sealed class _202302182041_initialClassroomTable : Migration
{
    public override void Up()
    {
        Create.Table("Classrooms")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("LessonId").AsInt32().NotNullable()
            .WithColumn("ProfessorId").AsInt32().NotNullable()
            .WithColumn("StartDate").AsDateTime().NotNullable()
            .WithColumn("EndDate").AsDateTime().NotNullable()
            .WithColumn("Capacity").AsByte().NotNullable()
            .WithColumn("TermId").AsInt32().NotNullable()
            .ForeignKey("FK_Classrooms_Terms",
                "Terms",
                "Id");
    }

    public override void Down()
    {
        Delete.Table("Classrooms");
    }
}