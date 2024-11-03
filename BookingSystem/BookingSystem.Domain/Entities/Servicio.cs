﻿using System.Text.Json.Serialization;

namespace BookingSystem.Domain.Entities
{
    public class Servicio
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("disponible")]
        public bool Disponible { get; set; }
    }
}
