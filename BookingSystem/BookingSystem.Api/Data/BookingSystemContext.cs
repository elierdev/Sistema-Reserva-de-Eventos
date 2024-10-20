using Microsoft.EntityFrameworkCore;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
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

        
    }
}
