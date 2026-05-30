using AuthPlatform.Mvc.Services;
using AuthPlatform.Mvc.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthPlatform.Mvc.Controllers.Auth
{


    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _authService.LoginAsync(model);

            if (response?.Success != true)
            {
                ViewBag.Error =response?.Message ?? "Invalid username or password.";

                return View(model);
            }

            return RedirectToAction(
                "Index",
                "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}