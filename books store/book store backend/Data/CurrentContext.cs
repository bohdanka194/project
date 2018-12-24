namespace books
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CurrentContext : DbContext
    {
        public DbSet<Item> Cart { get; set; }
        public DbSet<Book> Books { get; set; }

        public CurrentContext(DbContextOptions<CurrentContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    } 
}