using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleInformation.Data.Migrations
{
    public partial class SchemaUpdate5419 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Admin",
                table: "Users",
                newName: "IsAdmin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Users",
                newName: "Admin");
        }
    }
}
