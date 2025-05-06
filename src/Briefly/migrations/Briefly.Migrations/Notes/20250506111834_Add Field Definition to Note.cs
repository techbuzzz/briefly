using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Briefly.Migrations.Notes
{
    /// <inheritdoc />
    public partial class AddFieldDefinitiontoNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Energy",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Feeling",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "HtmlContent",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Mood",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "RawData",
                schema: "note",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Summary",
                schema: "note",
                table: "notes");

            migrationBuilder.AddColumn<JsonDocument>(
                name: "CustomFields",
                schema: "note",
                table: "notes",
                type: "jsonb",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "note_field_definitions",
                schema: "note",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldKey = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    DataType = table.Column<string>(type: "text", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note_field_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_note_field_definitions_note_types_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalSchema: "note",
                        principalTable: "note_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note_field_options",
                schema: "note",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note_field_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_note_field_options_note_field_definitions_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "note",
                        principalTable: "note_field_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_note_field_definitions_NoteTypeId",
                schema: "note",
                table: "note_field_definitions",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_note_field_options_FieldDefinitionId",
                schema: "note",
                table: "note_field_options",
                column: "FieldDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "note_field_options",
                schema: "note");

            migrationBuilder.DropTable(
                name: "note_field_definitions",
                schema: "note");

            migrationBuilder.DropColumn(
                name: "CustomFields",
                schema: "note",
                table: "notes");

            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                schema: "note",
                table: "notes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Energy",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feeling",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtmlContent",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "RawData",
                schema: "note",
                table: "notes",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                schema: "note",
                table: "notes",
                type: "text",
                nullable: true);
        }
    }
}
