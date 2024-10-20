using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Espacio
    {
        public int EspacioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioPorHora { get; set; }
        public bool Disponible { get; set; }
    }
}
