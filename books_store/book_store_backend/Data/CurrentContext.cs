﻿namespace books
{
    using Microsoft.EntityFrameworkCore;

    public class CurrentContext : DbContext
    {
        public DbSet<payment_log> Payments { get; set; }
        public DbSet<Item> Cart { get; set; }
        public DbSet<Book> Books { get; set; }

        public CurrentContext(DbContextOptions<CurrentContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasIndex(item => item.Client).IsUnique(false);
            modelBuilder.Entity<payment_log>().HasIndex(item => item.Client).IsUnique(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    } 
}