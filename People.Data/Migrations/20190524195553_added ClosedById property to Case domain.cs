using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleInformation.Data.Migrations
{
    public partial class addedClosedByIdpropertytoCasedomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClosedById",
                table: "Cases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedById",
                table: "Cases");
        }
    }
}
