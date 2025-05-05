using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Application.Domain;

namespace Notes.Application.Persistence;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AuthorName).IsRequired();
        builder.Property(x => x.AuthorEmail).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Mood);
        builder.Property(x => x.Energy);
        builder.Property(x => x.Feeling);
        builder.Property(x => x.Summary);
        builder.Property(x => x.HtmlContent);
        builder.Property(x => x.RawData);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
    }
}