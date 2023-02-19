using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182046)]
public sealed class _202302182046_initialLessonTable : Migration
{
    public override void Up()
    {
        Create.Table("Lessons")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("Coefficient").AsByte().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Lessons");
    }
}