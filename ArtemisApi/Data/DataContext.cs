using ArtemisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtemisApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }
    public DbSet<ReadingSession> ReadingSessions { get; set; }
    public DbSet<ChapterNote> ChapterNotes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.CreatedAt).HasDefaultValueSql("NOW()");
        modelBuilder.Entity<Book>().HasIndex(b => b.ExternalId).IsUnique();
        
        modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Shelf>().Property(s => s.Id).HasConversion<string>();

        // UserBook → Book (Many-to-One)
        modelBuilder.Entity<UserBook>()
            .HasOne(ub => ub.Book)
            .WithMany(b => b.UserBooks)
            .HasForeignKey(ub => ub.BookId);
        // UserBook → User (Many-to-One)
        modelBuilder.Entity<UserBook>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(ub => ub.UserId);
        // UserBook → Shelf (Many-to-One)
        modelBuilder.Entity<UserBook>()
            .HasOne(ub => ub.Shelf)
            .WithMany(s => s.UserBooks)
            .HasForeignKey(ub => ub.ShelfId);
        modelBuilder.Entity<UserBook>().Property(ub => ub.CreatedAt).HasDefaultValueSql("NOW()");
        modelBuilder.Entity<UserBook>().HasKey(ub => new { ub.UserId, ub.ShelfId, ub.BookId });
        
        modelBuilder.Entity<ReadingSession>().HasOne(rs => rs.Book).WithMany(b => b.ReadingSessions);
        modelBuilder.Entity<ReadingSession>().HasOne(rs => rs.User).WithMany(u => u.ReadingSessions);
        
        modelBuilder.Entity<ChapterNote>().Property(c => c.CreatedAt).HasDefaultValueSql("NOW()");
        modelBuilder.Entity<ChapterNote>().HasOne(cn => cn.User).WithMany(u => u.ChapterNotes);
        modelBuilder.Entity<ChapterNote>().HasOne(cn => cn.Book).WithMany(b => b.ChapterNotes);
        
        base.OnModelCreating(modelBuilder);
    }
}
