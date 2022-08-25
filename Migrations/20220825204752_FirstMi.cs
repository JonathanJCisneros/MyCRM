using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCRM.Migrations
{
    public partial class FirstMi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POC",
                table: "Staff");

            migrationBuilder.AddColumn<int>(
                name: "PocId",
                table: "Businesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocId",
                table: "Businesses");

            migrationBuilder.AddColumn<bool>(
                name: "POC",
                table: "Staff",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
