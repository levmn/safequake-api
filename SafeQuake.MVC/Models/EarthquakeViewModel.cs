using System.ComponentModel.DataAnnotations;

namespace SafeQuake.MVC.Models
{
    public class EarthquakeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Magnitude")]
        public decimal Magnitude { get; set; }

        [Required]
        [Display(Name = "Location")]
        public required string Location { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        public DateTime OccurredAt { get; set; }

        [Display(Name = "Depth (km)")]
        public decimal? Depth { get; set; }

        [Display(Name = "Latitude")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal Longitude { get; set; }

        public Guid? UserId { get; set; }
    }
} 