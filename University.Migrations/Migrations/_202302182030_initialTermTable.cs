using FluentMigrator;

namespace University.Migrations.Migrations;

[Migration(202302182030)]
public sealed class _202302182030_initialTermTable : Migration
{
    public override void Up()
    {
        Create.Table("Terms")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("UnitCount").AsByte().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Terms");
    }
}