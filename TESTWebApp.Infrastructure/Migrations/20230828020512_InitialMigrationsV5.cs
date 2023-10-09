using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "middle_work_item_id",
                table: "work_input",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "minor_work_item_id",
                table: "work_input",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "middle_work_item_id",
                table: "work_input");

            migrationBuilder.DropColumn(
                name: "minor_work_item_id",
                table: "work_input");
        }
    }
}
