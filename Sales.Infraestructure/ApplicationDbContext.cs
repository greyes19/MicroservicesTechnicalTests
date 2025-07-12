using Microsoft.EntityFrameworkCore;
using Sales.Domain.Model.Models;

namespace Sales.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SalesDetailModel> SalesDetails { get; set; }
        public DbSet<SalesHeaderModel> SalesHeaders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesDetailModel>(entity =>
            {
                entity.ToTable("SalesDetails");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SalesHeaderId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.Igv).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.SubTotal).HasColumnType("decimal(28,15)").IsRequired();
                entity.HasOne(c => c.SalesHeader)
                   .WithMany(h => h.SalesDetails)
                   .HasForeignKey(m => m.SalesHeaderId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalesHeaderModel>(entity =>
            {
                entity.ToTable("SalesHeaders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Igv).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(e => e.SubTotal).HasColumnType("decimal(28,15)").IsRequired();
            });
        }
    }
}
