using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "work_item");

            migrationBuilder.CreateTable(
                name: "major_work_item",
                columns: table => new
                {
                    major_work_item_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    major_work_item_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major_work_item", x => x.major_work_item_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "major_work_item");

            migrationBuilder.CreateTable(
                name: "work_item",
                columns: table => new
                {
                    work_item_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    work_item_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_item", x => x.work_item_id);
                });
        }
    }
}
