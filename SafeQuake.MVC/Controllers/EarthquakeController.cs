using Microsoft.AspNetCore.Mvc;
using SafeQuake.MVC.Models;

namespace SafeQuake.MVC.Controllers
{
    public class EarthquakeController : Controller
    {
        private readonly HttpClient _client;

        public EarthquakeController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("SafeQuakeApi");
        }

        public async Task<IActionResult> Index()
        {
            var earthquakes = await _client.GetFromJsonAsync<List<EarthquakeViewModel>>("/api/earthquakes");
            return View(earthquakes);
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
                var response = await _client.PostAsJsonAsync("/api/earthquakes", model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
} 