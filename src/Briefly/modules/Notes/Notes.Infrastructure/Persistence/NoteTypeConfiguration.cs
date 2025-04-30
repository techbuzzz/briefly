using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastructure.Persistence
{
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
            //builder.HasIndex(x => x.Name).IsUnique();
            //builder.HasIndex(x => x.IsActive);
            //builder.HasIndex(x => x.CreatedAt);

        }
    }
}
