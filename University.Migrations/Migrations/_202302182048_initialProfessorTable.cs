using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182048)]
public sealed class _202302182048_initialProfessorTable : Migration
{
    public override void Up()
    {
        Create.Table("Professors")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("FirstName").AsString(100).NotNullable()
            .WithColumn("LastName").AsString(100).NotNullable()
            .WithColumn("Mobile").AsString(11).NotNullable()
            .WithColumn("PersonnelId").AsString(10).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Professors");
    }
}