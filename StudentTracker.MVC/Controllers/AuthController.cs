using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;
using System.Security.Claims;
using System.Text.Json;

namespace StudentTracker.MVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // الـ AuthController في الـ API الحالي بياخد كائن Teacher كامل
            // ويقارن Email و PasswordHash كما هما (بدون تشفير حالياً في الـ API).
            var payload = new { Email = model.Email, PasswordHash = model.Password };
            var response = await _apiService.PostAsync("Auth/login", payload);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو كلمة المرور غير صحيحة");
                return View(model);
            }

            var json = await response.Content.ReadAsStringAsync();
            string? token = null;
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    token = tokenElement.GetString();
                }
            }
            catch (JsonException)
            {
                // تجاهل، هيتعامل معاها الكود تحت كـ فشل دخول
            }

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError(string.Empty, "تعذر الحصول على بيانات الدخول من السيرفر");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, model.Email),
                new(ClaimTypes.Role, "Teacher"),
                new("access_token", token)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
