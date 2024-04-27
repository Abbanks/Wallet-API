using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models.Entities;

namespace WalletApi.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.AppUser)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.AppUserId);

            modelBuilder.Entity<AppUser>()
           .Property(u => u.TotalBalance)
           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Wallet>()
            .Property(w => w.Balance)
            .HasColumnType("decimal(18,2)");
        }
    }
}

