using Microsoft.EntityFrameworkCore;
using ThriftBooks.Data.Entities;

namespace ThriftBooks.Data
{
    public class ThriftBooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookUser> BookUsers { get; set; }

        public ThriftBooksContext() : base()
        {

        }

        public ThriftBooksContext(DbContextOptions<ThriftBooksContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-A9PMVFUV\\MSSQLLocalDB;Initial Catalog=ThriftBooksDB;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=true") ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookUser>()
                .HasKey(bu => new { bu.UserId, bu.BookId });

            modelBuilder.Entity<BookUser>()
                .HasOne(x => x.Book)
                .WithMany(b => b.BookUsers)
                .HasForeignKey(bu => bu.BookId);

            modelBuilder.Entity<BookUser>()
                .HasOne(x => x.User)
                .WithMany(u => u.BookUsers)
                .HasForeignKey(bu => bu.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
