#nullable disable

using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Briefly.Migrations.Notes;

/// <inheritdoc />
public partial class Initialmigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "note");

        migrationBuilder.CreateTable(
            "note_types",
            schema: "note",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                Description = table.Column<string>("text", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false, defaultValue: true),
                Created = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                CreatedBy = table.Column<Guid>("uuid", nullable: false),
                LastModified = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                LastModifiedBy = table.Column<Guid>("uuid", nullable: true),
                Deleted = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true),
                DeletedBy = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_note_types", x => x.Id); });

        migrationBuilder.CreateTable(
            "notes",
            schema: "note",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                AuthorName = table.Column<string>("text", nullable: false),
                AuthorEmail = table.Column<string>("text", nullable: false),
                NoteTypeId = table.Column<Guid>("uuid", nullable: false),
                Date = table.Column<DateOnly>("date", nullable: false),
                Mood = table.Column<string>("text", nullable: true),
                Energy = table.Column<string>("text", nullable: true),
                Feeling = table.Column<string>("text", nullable: true),
                Summary = table.Column<string>("text", nullable: true),
                HtmlContent = table.Column<string>("text", nullable: true),
                RawData = table.Column<JsonDocument>("jsonb", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false,
                    defaultValueSql: "now()"),
                Created = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                CreatedBy = table.Column<Guid>("uuid", nullable: false),
                LastModified = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                LastModifiedBy = table.Column<Guid>("uuid", nullable: true),
                Deleted = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true),
                DeletedBy = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_notes", x => x.Id);
                table.ForeignKey(
                    "FK_notes_note_types_NoteTypeId",
                    x => x.NoteTypeId,
                    principalSchema: "note",
                    principalTable: "note_types",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_notes_NoteTypeId",
            schema: "note",
            table: "notes",
            column: "NoteTypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "notes",
            "note");

        migrationBuilder.DropTable(
            "note_types",
            "note");
    }
}