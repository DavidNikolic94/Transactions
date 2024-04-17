using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Transactions.Models;

namespace Transactions.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().Property(u => u.Balance).HasPrecision(18, 4);

            modelBuilder.Entity<User>().HasData(
            new User { Id = "1", UserName = "Alice", Currency = "EUR", Balance = 1000.00m },
            new User { Id = "2", UserName = "Bob", Currency = "USD", Balance = 2000.00m },
            new User { Id = "3", UserName = "Charlie", Currency = "CAD", Balance = 3000.00m },
            new User { Id = "4", UserName = "Dave", Currency = "USD", Balance = 4000.00m },
            new User { Id = "5", UserName = "Eve", Currency = "EUR", Balance = 5000.00m }
            );
        }

    }
}
