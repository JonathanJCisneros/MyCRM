using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCRM.Migrations
{
    public partial class Two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Staff",
                newName: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Staff",
                newName: "PhoneNumber");
        }
    }
}
