using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Briefly.Migrations.Notes
{
    /// <inheritdoc />
    public partial class AddTitleToNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "note",
                table: "notes");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "note",
                table: "notes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "note",
                table: "notes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }
    }
}
