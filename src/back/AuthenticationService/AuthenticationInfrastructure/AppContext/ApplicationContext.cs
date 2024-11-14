using AuthenticationDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationInfrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CreaterModel> Creater { get; set; } = null!;
        public DbSet<UserModel> User { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=kartofel12341");
            }
        }
    }
}
