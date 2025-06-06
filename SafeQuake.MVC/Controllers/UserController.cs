using Microsoft.AspNetCore.Mvc;
using SafeQuake.MVC.Models;
using System.Text.Json;
using System.Text;

namespace SafeQuake.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5000/api"; // Adjust this to match your API port

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/users");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<UserViewModel>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(users);
                }
                
                TempData["Error"] = "Erro ao carregar usuários.";
                return View(new List<UserViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro: {ex.Message}";
                return View(new List<UserViewModel>());
            }
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/users", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Usuário criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Error"] = "Erro ao criar usuário.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Erro: {ex.Message}";
                }
            }
            return View(user);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserViewModel>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(user);
                }

                TempData["Error"] = "Usuário não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 