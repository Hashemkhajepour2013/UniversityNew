using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182052)]
public sealed class _202302182052_initialStudentClassroomTable : Migration
{
    public override void Up()
    {
        Create.Table("StudentClassrooms")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("StudentId").AsInt32().NotNullable()
            .ForeignKey("FK_StudentClassrooms_Students",
                "Students",
                "Id")
            .WithColumn("ClassroomId").AsInt32().NotNullable()
            .ForeignKey(
                "FK_StudentClassrooms_Classrooms",
                "Classrooms",
                "Id");
    }

    public override void Down()
    {
        Delete.Table("StudentClassrooms");
    }
}