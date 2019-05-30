using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<ConsoleDbContext>
    {
        public ConsoleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConsoleDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=Blogging;Integrated Security=True");

            return new ConsoleDbContext(optionsBuilder.Options);
        }
    }

    public class ConsoleDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ConsoleDbContext(DbContextOptions<ConsoleDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType == null)
                    continue;
                // delete all foreign key
                foreach (var foreignKey in mutableEntityType.GetForeignKeys().ToList())
                {
                    foreignKey.DeclaringEntityType.RemoveForeignKey(foreignKey.Properties, foreignKey.PrincipalKey,
                        foreignKey.PrincipalEntityType);
                }
            }

            modelBuilder.Entity<Order>().HasData(new Order() { Id = Guid.NewGuid(), Code = "TEST" });
        }
    }
}