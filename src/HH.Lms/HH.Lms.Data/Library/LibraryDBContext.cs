using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Library.FluentConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HH.Lms.Data.Library;

public class LibraryDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }

    public LibraryDBContext() { }

    public LibraryDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}
