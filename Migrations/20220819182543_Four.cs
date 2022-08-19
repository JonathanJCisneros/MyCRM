using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCRM.Migrations
{
    public partial class Four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallContact",
                table: "Staff");

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "Staff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Staff");

            migrationBuilder.AddColumn<int>(
                name: "CallContact",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
