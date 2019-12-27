using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolCopter.Registration.Data.Migrations
{
    public partial class UniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Copters_LastConnectedTimestamp",
                table: "Copters",
                column: "LastConnectedTimestamp",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Copters_LastConnectedTimestamp",
                table: "Copters");
        }
    }
}
