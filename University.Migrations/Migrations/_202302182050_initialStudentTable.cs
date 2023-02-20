using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182050)]
public sealed class _202302182050_initialStudentTable : Migration
{
    public override void Up()
    {
        Create.Table("Students")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("FirstName").AsString(100).NotNullable()
            .WithColumn("LastName").AsString(100).NotNullable()
            .WithColumn("Mobile").AsString(11).NotNullable()
            .WithColumn("studentNumber").AsString(10).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Students");
    }
}