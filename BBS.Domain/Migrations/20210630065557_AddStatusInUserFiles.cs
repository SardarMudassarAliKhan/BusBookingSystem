using Microsoft.EntityFrameworkCore.Migrations;

namespace CVBank.Domain.Migrations
{
    public partial class AddStatusInUserFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserFiles");
        }
    }
}
