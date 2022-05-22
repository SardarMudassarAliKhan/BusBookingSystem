using Microsoft.EntityFrameworkCore.Migrations;

namespace CVBank.Domain.Migrations
{
    public partial class BBSM6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationMinHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationMaxHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate13Seat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate23Seat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate35Seat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate53Seat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
