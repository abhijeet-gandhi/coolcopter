using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolCopter.Registration.Data.Migrations
{
    public partial class Interval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Interval",
                table: "Copters",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "Copters");
        }
    }
}
