
using Microsoft.EntityFrameworkCore;
using App.Entities;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(o =>
            {
                o.HasKey(e => e.Id);
                o.HasIndex(e => e.UserName).IsUnique();
                o.HasIndex(e => e.Email).IsUnique();
                o.Property(e => e.PasswordHash).IsRequired();
                o.Property(e => e.Role).IsRequired();
            });
        }

    }
}