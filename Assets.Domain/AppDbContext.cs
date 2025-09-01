using Assets.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assets.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                       .IsUnique();
            });

            // AssetType <-> Asset relationship
            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.Property(at => at.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(at => at.Assets)
                      .WithOne(a => a.AssetType)
                      .HasForeignKey(a => a.AssetTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}
