using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    /// <summary>Configuración JWT (debe coincidir con el API de Seguridad que emite el token).</summary>
    public class TokenConfiguracion
    {
        [Required]
        [StringLength(100, MinimumLength = 32)]
        public string key { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        public double Expires { get; set; }
    }
}
