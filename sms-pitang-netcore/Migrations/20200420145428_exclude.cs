using Microsoft.EntityFrameworkCore.Migrations;

namespace sms_pitang_netcore.Migrations
{
    public partial class exclude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Email",
                table: "Users",
                columns: new[] { "Username", "Email" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username_Email",
                table: "Users");
        }
    }
}
