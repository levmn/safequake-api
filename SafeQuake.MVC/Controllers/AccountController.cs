using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SafeQuake.MVC.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SafeQuake.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5049/api";
        private readonly ILogger<AccountController> _logger;

        public AccountController(IHttpClientFactory httpClientFactory, ILogger<AccountController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/auth/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var userData = await response.Content.ReadAsStringAsync();
                        var user = JsonSerializer.Deserialize<UserViewModel>(userData, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (user == null)
                        {
                            ModelState.AddModelError("", "Erro ao processar dados do usuário.");
                            return View(model);
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("UserId", user.Id.ToString())
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        return RedirectToAction("Index", "Dashboard");
                    }

                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Login failed. Status: {response.StatusCode}, Response: {errorContent}");
                    ModelState.AddModelError("", "Email ou senha inválidos");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Login error: {ex.Message}");
                    ModelState.AddModelError("", $"Erro ao fazer login: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Set CreatedAt to current time
                    model.CreatedAt = DateTime.UtcNow;

                    var json = JsonSerializer.Serialize(model);
                    _logger.LogInformation($"Sending registration request: {json}");

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/User", content);

                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Registration response: Status={response.StatusCode}, Content={responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Cadastro realizado com sucesso! Faça login para continuar.";
                        return RedirectToAction(nameof(Login));
                    }

                    // Try to get a more specific error message from the API
                    try
                    {
                        var errorResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
                        if (errorResponse != null && errorResponse.ContainsKey("message"))
                        {
                            ModelState.AddModelError("", errorResponse["message"]);
                        }
                        else
                        {
                            ModelState.AddModelError("", $"Erro ao criar conta. Status: {response.StatusCode}");
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("", $"Erro ao criar conta. Status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Registration error: {ex.Message}");
                    ModelState.AddModelError("", $"Erro ao criar conta: {ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning($"Invalid model state: {string.Join(", ", errors)}");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
} 