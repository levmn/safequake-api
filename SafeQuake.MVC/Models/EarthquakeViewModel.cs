using System.ComponentModel.DataAnnotations;

namespace SafeQuake.MVC.Models
{
    public class EarthquakeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Magnitude (escala Richter)")]
        public double Magnitude { get; set; }

        [Display(Name = "Localização")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Data e Hora")]
        public DateTime OccurredAt { get; set; }

        [Display(Name = "Profundidade (km)")]
        public double Depth { get; set; }

        [Display(Name = "Nível de Alerta")]
        public string AlertLevel { get; set; } = string.Empty;

        public bool IsNearby(double userLat, double userLong, double radiusKm = 100)
        {
            var earthRadius = 6371; // Raio da Terra em km
            
            var latDiff = ToRad(userLat - Latitude);
            var lonDiff = ToRad(userLong - Longitude);
            
            var a = Math.Sin(latDiff/2) * Math.Sin(latDiff/2) +
                    Math.Cos(ToRad(Latitude)) * Math.Cos(ToRad(userLat)) *
                    Math.Sin(lonDiff/2) * Math.Sin(lonDiff/2);
            
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            var distance = earthRadius * c;
            
            return distance <= radiusKm;
        }

        private double ToRad(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
} 