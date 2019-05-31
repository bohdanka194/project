namespace books.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CurrentContext : DbContext
    {
        public DbSet<payment> Payments { get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Book> Books { get; set; }

        public CurrentContext(DbContextOptions<CurrentContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasIndex(item => item.Client).IsUnique(false);
            modelBuilder.Entity<payment>().HasIndex(item => item.Client).IsUnique(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        } 
    } 
}