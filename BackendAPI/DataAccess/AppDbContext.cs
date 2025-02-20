using BackendAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Tasinmaz> Tasinmazlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // İlişkileri tanımlayalım
            modelBuilder.Entity<Ilce>()
                .HasOne(i => i.Il)
                .WithMany(i => i.Ilceler)
                .HasForeignKey(i => i.IlId);

            modelBuilder.Entity<Mahalle>()
                .HasOne(m => m.Ilce)
                .WithMany(i => i.Mahalleler)
                .HasForeignKey(m => m.IlceId);

            modelBuilder.Entity<Tasinmaz>()
                .HasOne(t => t.Mahalle)
                .WithMany(m => m.Tasinmazlar)
                .HasForeignKey(t => t.MahalleId);
        }
    }
}
