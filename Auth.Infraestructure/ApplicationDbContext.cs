using Auth.Domain.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
            });
        }
    }

}
