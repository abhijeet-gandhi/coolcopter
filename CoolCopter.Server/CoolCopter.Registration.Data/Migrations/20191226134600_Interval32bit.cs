using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolCopter.Registration.Data.Migrations
{
    public partial class Interval32bit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "Copters",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Interval",
                table: "Copters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
