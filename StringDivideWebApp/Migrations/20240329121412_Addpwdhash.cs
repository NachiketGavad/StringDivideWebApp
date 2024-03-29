using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StringDivideWebApp.Migrations
{
    public partial class Addpwdhash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "StringDivideAppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "StringDivideAppUsers");
        }
    }
}
