using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "work_input");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "work_input",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
