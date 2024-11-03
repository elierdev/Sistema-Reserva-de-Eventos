using System.Text.Json.Serialization;

namespace BookingSystem.Domain.Entities
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("contraseña")]
        public string? Contraseña { get; set; }

        [JsonPropertyName("esAdministrador")]
        public bool EsAdministrador { get; set; }
    }
}
