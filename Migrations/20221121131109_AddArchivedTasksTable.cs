using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoerASPdotNETMVCToDosManager.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedTasksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedTasks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedTasks");
        }
    }
}
