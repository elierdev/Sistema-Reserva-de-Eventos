using Microsoft.EntityFrameworkCore;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Domain
{
    public class BookingSystemContext : DbContext
    {
        public BookingSystemContext(DbContextOptions<BookingSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Espacio> Espacios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para Espacio
            modelBuilder.Entity<Espacio>()
                .Property(e => e.PrecioPorHora)
                .HasColumnType("decimal(18,2)");

            // Configuración para Reserva
            modelBuilder.Entity<Reserva>()
                .Property(r => r.CostoTotal)
                .HasColumnType("decimal(18,2)");

            // Configuración para Servicio
            modelBuilder.Entity<Servicio>()
                .Property(s => s.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }
}
