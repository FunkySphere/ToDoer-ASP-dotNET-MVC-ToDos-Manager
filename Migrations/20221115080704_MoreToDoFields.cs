using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoerASPdotNETMVCToDosManager.Migrations
{
    /// <inheritdoc />
    public partial class MoreToDoFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ToDos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "ToDos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "ToDos");
        }
    }
}
