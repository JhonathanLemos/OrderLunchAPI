using Lanches.Core.Entities;
using Lanches.Infraestructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lanches.Infraestructure.Context
{
    public class LanchesDbContext : IdentityDbContext<User>
    {
        public LanchesDbContext(DbContextOptions<LanchesDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Lunch> Lunch { get; set; }
        public DbSet<LunchItem> LunchItem { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IngredientsConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
