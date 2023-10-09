using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "middle_work_item",
                columns: table => new
                {
                    middle_work_item_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    middle_work_item_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    major_work_item_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_middle_work_item", x => x.middle_work_item_id);
                });

            migrationBuilder.CreateTable(
                name: "minor_work_item",
                columns: table => new
                {
                    minor_work_item_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    minor_work_item_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    major_work_item_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middle_work_item_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_minor_work_item", x => x.minor_work_item_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "middle_work_item");

            migrationBuilder.DropTable(
                name: "minor_work_item");
        }
    }
}
