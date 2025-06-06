using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeQuake.MVC.Models;
using System.Text.Json;

namespace SafeQuake.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IHttpClientFactory clientFactory, ILogger<DashboardController> logger)
        {
            _client = clientFactory.CreateClient("SafeQuakeApi");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get recent earthquakes (last 7 days with magnitude >= 4.0)
                var recentEarthquakes = await _client.GetFromJsonAsync<List<EarthquakeViewModel>>("api/Earthquake/alertas");
                
                var dashboardViewModel = new DashboardViewModel
                {
                    RecentEarthquakes = recentEarthquakes ?? new List<EarthquakeViewModel>(),
                    UserName = User.Identity?.Name ?? "Usuário"
                };

                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching dashboard data");
                return View(new DashboardViewModel 
                { 
                    RecentEarthquakes = new List<EarthquakeViewModel>(),
                    UserName = User.Identity?.Name ?? "Usuário",
                    ErrorMessage = "Não foi possível carregar os dados dos terremotos. Por favor, tente novamente mais tarde."
                });
            }
        }
    }
} 