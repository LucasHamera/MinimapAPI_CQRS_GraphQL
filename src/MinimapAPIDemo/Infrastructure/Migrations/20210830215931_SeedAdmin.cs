using Microsoft.EntityFrameworkCore.Migrations;

namespace MinimapAPIDemo.Infrastructure.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Login", "Description" },
                values: new object[] { "admin", "AQAAAAEAACcQAAAAELibfyhEQ34pzbtFEsXax3A6gkWiF0sHXeZ+EiaPHcLX9yG7eVjoK3+phXvHIyKJhw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "admin");
        }
    }
}
