using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookingSystem.Domain.Entities
{
    public class Reserva
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("eventoId")]
        public int EventoId { get; set; }

        [JsonPropertyName("espacioId")]
        public int EspacioId { get; set; }

        [JsonPropertyName("serviciosIds")]
        public List<int> ServiciosIds { get; set; } = new List<int>(); // Inicializamos la lista

        [JsonPropertyName("fechaReserva")]
        public DateTime FechaReserva { get; set; }

        [JsonPropertyName("costoTotal")]
        public decimal CostoTotal { get; set; }

        [JsonPropertyName("confirmada")]
        public bool Confirmada { get; set; }
    }
}
