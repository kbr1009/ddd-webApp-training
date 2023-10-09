using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "work_input",
                columns: table => new
                {
                    work_input_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    work_item_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_input", x => x.work_input_id);
                });

            migrationBuilder.CreateTable(
                name: "work_item",
                columns: table => new
                {
                    work_item_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    work_item_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_item", x => x.work_item_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "work_input");

            migrationBuilder.DropTable(
                name: "work_item");
        }
    }
}
