using Microsoft.EntityFrameworkCore;
using Movement.Domain.Model.Models;

namespace Movement.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MovementDetailModel> MovementDetails { get; set; }
        public DbSet<MovementHeaderModel> MovementHeaders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovementDetailModel>(entity =>
            {
                entity.ToTable("MovementDetails");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MovementHeaderId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Quantity).HasColumnType("decimal(18,4)").IsRequired();

                entity.HasOne(c => c.MovementHeader)
                   .WithMany(h => h.MovementDetails)
                   .HasForeignKey(m => m.MovementHeaderId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MovementHeaderModel>(entity =>
            {
                entity.ToTable("MovementHeaders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OriginDocumentId).IsRequired();
                entity.Property(e => e.MovementType).IsRequired();
            });
        }
    }
}
