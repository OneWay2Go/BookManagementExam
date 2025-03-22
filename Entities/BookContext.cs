using Microsoft.EntityFrameworkCore;

namespace _6_modul_exam.Entities;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
    }

    public BookContext(DbContextOptions options) : base(options) { }
}
