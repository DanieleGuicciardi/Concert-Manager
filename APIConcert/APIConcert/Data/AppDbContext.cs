using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIConcert.Models;


namespace APIConcert.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Biglietto>()
                .HasOne(b => b.User)
                .WithMany(u => u.Biglietti)
                .HasForeignKey(b => b.UserId);

            builder.Entity<Biglietto>()
                .HasOne(b => b.Evento)
                .WithMany(e => e.Biglietti)
                .HasForeignKey(b => b.EventoId);
        }
    }
}
