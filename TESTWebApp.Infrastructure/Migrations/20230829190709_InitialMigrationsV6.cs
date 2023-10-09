using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "web_session",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "web_session",
                table: "users");
        }
    }
}
