using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Model.Models;

namespace Purchase.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PurchaseDetailModel> PurchaseDetails { get; set; }
        public DbSet<PurchaseHeaderModel> PurchaseHeaders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseDetailModel>(entity =>
            {
                entity.ToTable("PurchaseDetails");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PurchaseHeaderId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.Igv).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.SubTotal).HasColumnType("decimal(28,15)").IsRequired();
                entity.HasOne(c => c.PurchaseHeader)
                   .WithMany(h => h.PurchaseDetails)
                   .HasForeignKey(m => m.PurchaseHeaderId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PurchaseHeaderModel>(entity =>
            {
                entity.ToTable("PurchaseHeaders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Igv).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(28,15)").IsRequired();
                entity.Property(e => e.SubTotal).HasColumnType("decimal(28,15)").IsRequired();
            });
        }
    }
}
