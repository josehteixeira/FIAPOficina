using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FIAPOficina.Infrastructure.Database.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<ServiceOrders> ServiceOrders { get; set; }
        public DbSet<ServiceOrderMaterials> ServiceOrderMaterials { get; set; }
        public DbSet<ServiceOrderServices> ServiceOrderServices { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}