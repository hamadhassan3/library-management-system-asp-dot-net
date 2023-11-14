using HH.Lms.Data.Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HH.Lms.Data.Library.FluentConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Isbn).HasMaxLength(20).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(255).IsRequired();
            builder.Property(b => b.Author).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Description).HasMaxLength(1000);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
