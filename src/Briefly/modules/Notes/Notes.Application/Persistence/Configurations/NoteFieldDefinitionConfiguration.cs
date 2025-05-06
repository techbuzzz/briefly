using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Application.Domain;

namespace Notes.Application.Persistence.Configurations;

public class NoteFieldDefinitionConfiguration : IEntityTypeConfiguration<NoteFieldDefinition>
{
    public void Configure(EntityTypeBuilder<NoteFieldDefinition> builder)
    {
        builder.ToTable("note_field_definitions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataType)
            .HasConversion<string>();
        builder.HasMany(x => x.Options)
            .WithOne()
            .HasForeignKey(o => o.FieldDefinitionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}