using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeQuake.MVC.Models;
using System.Text.Json;

namespace SafeQuake.MVC.Controllers
{
    [Authorize]
    public class EarthquakeController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<EarthquakeController> _logger;

        public EarthquakeController(IHttpClientFactory clientFactory, ILogger<EarthquakeController> logger)
        {
            _client = clientFactory.CreateClient("SafeQuakeApi");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var earthquakes = await _client.GetFromJsonAsync<List<EarthquakeViewModel>>("api/Earthquake");
                return View(earthquakes ?? new List<EarthquakeViewModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching earthquakes");
                TempData["Error"] = "Não foi possível carregar os dados dos terremotos. Por favor, tente novamente mais tarde.";
                return View(new List<EarthquakeViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EarthquakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _client.PostAsJsonAsync("api/Earthquake", model);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Evento sísmico registrado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Erro ao registrar evento: {error}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating earthquake");
                    ModelState.AddModelError("", "Erro ao registrar evento sísmico. Por favor, tente novamente mais tarde.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var earthquake = await _client.GetFromJsonAsync<EarthquakeViewModel>($"api/Earthquake/{id}");
                if (earthquake == null)
                {
                    return NotFound();
                }
                return View(earthquake);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching earthquake details");
                return NotFound();
            }
        }
    }
} 