using Microsoft.EntityFrameworkCore;
using Products.Domain.Services.Models;

namespace Products.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SalePrice).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.Cost).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
