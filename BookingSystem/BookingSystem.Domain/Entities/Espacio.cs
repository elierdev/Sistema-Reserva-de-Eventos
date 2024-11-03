using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Espacio
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("capacidad")]
        public int Capacidad { get; set; }

        [JsonPropertyName("precioPorHora")]
        public decimal PrecioPorHora { get; set; }

        [JsonPropertyName("disponible")]
        public bool Disponible { get; set; }
    }

}
