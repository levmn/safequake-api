using SafeQuake.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SafeQuake.Domain.Requests
{
    public class EarthquakeRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "A latitude é necessária.")]
        [Range(-90, 90, ErrorMessage = "A latitude deve estar entre -90 e 90 graus.")]
        public required double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é necessária.")]
        [Range(-180, 180, ErrorMessage = "A longitude deve estar entre -180 e 180 graus.")]
        public required double Longitude { get; set; }

        [Required(ErrorMessage = "A profundidade é necessária.")]
        [Range(0, double.MaxValue, ErrorMessage = "A profundidade deve ser um valor positivo.")]
        public required double Depth { get; set; }

        [Required(ErrorMessage = "A magnitude é necessária.")]
        [Range(0, double.MaxValue, ErrorMessage = "A magnitude deve ser um valor positivo.")]
        public required double Magnitude { get; set; }

        [Required(ErrorMessage = "A data e hora são necessárias.")]
        public required DateTime DateTime { get; set; }

        [Required(ErrorMessage = "A localização é necessária.")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "A localização deve ter entre 3 e 1000 caracteres.")]
        public required string Location { get; set; }

        [Required(ErrorMessage = "O ID do usuário é necessário.")]
        public required int UserId { get; set; }

        public EarthquakeEntity GetEarthquakeEntity()
        {
            return new EarthquakeEntity
            {
                Id = this.Id ?? 0,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Depth = this.Depth,
                Magnitude = this.Magnitude,
                DateTime = this.DateTime,
                Location = this.Location,
                UserId = this.UserId
            };
        }
    }
}