using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleInformation.Data.Migrations
{
    public partial class updatedMessageandCasedomainsdeletedalldomainsassociatedwithemployeers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ServiceProviderEmployees_ServiceProviderEmployeeId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "ServiceProviderEmployees");

            migrationBuilder.DropTable(
                name: "UserEmployeers");

            migrationBuilder.DropTable(
                name: "Employeers");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ServiceProviderEmployeeId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Resolved",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ServiceProviderEmployeeId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "OriginatorId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeToResolution",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginatorId",
                table: "Messages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeToResolution",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Resolved",
                table: "Cases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ServiceProviderEmployeeId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employeers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AddressId = table.Column<int>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employeers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviderEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LogIn = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviderEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEmployeers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    EmployeerId = table.Column<int>(nullable: false),
                    EmployeerId1 = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmployeers", x => new { x.UserId, x.EmployeerId });
                    table.ForeignKey(
                        name: "FK_UserEmployeers_Employeers_EmployeerId1",
                        column: x => x.EmployeerId1,
                        principalTable: "Employeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEmployeers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ServiceProviderEmployeeId",
                table: "Addresses",
                column: "ServiceProviderEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employeers_AddressId",
                table: "Employeers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmployeers_EmployeerId1",
                table: "UserEmployeers",
                column: "EmployeerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ServiceProviderEmployees_ServiceProviderEmployeeId",
                table: "Addresses",
                column: "ServiceProviderEmployeeId",
                principalTable: "ServiceProviderEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
