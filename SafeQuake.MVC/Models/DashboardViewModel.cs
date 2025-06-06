namespace SafeQuake.MVC.Models
{
    public class DashboardViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public List<EarthquakeViewModel> RecentEarthquakes { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }
} 