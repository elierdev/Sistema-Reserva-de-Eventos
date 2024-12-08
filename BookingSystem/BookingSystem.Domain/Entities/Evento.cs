using System;
using System.Text.Json.Serialization;

namespace BookingSystem.Domain.Entities
{
    public class Evento
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("fechaInicio")]
        public DateTime FechaInicio { get; set; }

        [JsonPropertyName("fechaFin")]
        public DateTime FechaFin { get; set; }

        [JsonPropertyName("usuarioId")]
        public int? UsuarioId { get; set; }

        [JsonPropertyName("espacioId")]
        public int? EspacioId { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }
    }
}
