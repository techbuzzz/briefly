using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Application.Domain;

namespace Notes.Application.Persistence.Configurations;

public class NoteTypeConfiguration : IEntityTypeConfiguration<NoteType>
{
    public void Configure(EntityTypeBuilder<NoteType> builder)
    {
        builder.ToTable("note_types");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.HasMany(x => x.Notes)
            .WithOne(x => x.NoteType)
            .HasForeignKey(x => x.NoteTypeId);
        builder.HasMany(x => x.FieldDefinitions)
            .WithOne()
            .HasForeignKey(fd => fd.NoteTypeId)
            .OnDelete(DeleteBehavior.Cascade);
        //builder.HasIndex(x => x.Name).IsUnique();
        //builder.HasIndex(x => x.IsActive);
        //builder.HasIndex(x => x.CreatedAt);
    }
}