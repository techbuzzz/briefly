using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Application.Domain;

namespace Notes.Application.Persistence.Configurations;

public class NoteFieldOptionConfiguration : IEntityTypeConfiguration<NoteFieldOption>
{
    public void Configure(EntityTypeBuilder<NoteFieldOption> builder)
    {
        builder.ToTable("note_field_options");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Key).IsRequired();
        builder.Property(x => x.Value).IsRequired();
    }
}