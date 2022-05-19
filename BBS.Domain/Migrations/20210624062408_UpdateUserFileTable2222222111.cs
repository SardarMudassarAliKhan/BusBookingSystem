using Microsoft.EntityFrameworkCore.Migrations;

namespace CVBank.Domain.Migrations
{
    public partial class UpdateUserFileTable2222222111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PipelineName",
                table: "UserFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PipelineTag",
                table: "UserFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PipelineName",
                table: "UserFiles");

            migrationBuilder.DropColumn(
                name: "PipelineTag",
                table: "UserFiles");
        }
    }
}
