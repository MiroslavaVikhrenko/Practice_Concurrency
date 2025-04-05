using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Concurrency
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=April_PProducts;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>().Property(e => e.Description).IsConcurrencyToken();
            modelBuilder.Entity<Product>().Property(e => e.Price).IsConcurrencyToken();
            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "table", Description = "Japanese table", Price = 100.0m, Quantity = 2 });
        }
    }
}
