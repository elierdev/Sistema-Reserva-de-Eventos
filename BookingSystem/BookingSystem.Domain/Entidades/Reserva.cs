using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EventoId { get; set; }
        public int EspacioId { get; set; }
        public List<int> ServiciosIds { get; set; }
        public DateTime FechaReserva { get; set; }
        public decimal CostoTotal { get; set; }
        public bool Confirmada { get; set; }
    }
}
