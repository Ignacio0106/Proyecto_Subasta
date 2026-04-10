using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.ViewModels;
using Libreria.Web.Util;


namespace Subasta.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IServiceUsuario serviceUsuario, ILogger<LoginController> logger)
        {
            _serviceUsuario = serviceUsuario;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
        {
            if (!ModelState.IsValid)
            {
                string errores = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                        ? "Error de validación no especificado"
                        : e.ErrorMessage));

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene los siguientes errores: {errores}",
                    SweetAlertMessageType.warning
                );

                _logger.LogWarning("Error de validación en login para usuario {Usuario}. Detalle: {Errores}",
                    viewModelLogin.User, errores);

                return View("Index", viewModelLogin);
            }

            var usuarioLog = await _serviceUsuario.LoginAsync(viewModelLogin.User, viewModelLogin.Password);

            if (usuarioLog == null)
            {
                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Acceso denegado",
                    "Usuario o contraseña incorrectos.",
                    SweetAlertMessageType.warning
                );

                _logger.LogWarning("Intento de acceso fallido para usuario {Usuario}", viewModelLogin.User);

                return View("Index", viewModelLogin);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{usuarioLog.NombreCompleto}"),
                new Claim(ClaimTypes.Role, usuarioLog.IdRolNavigation.NombreRol),
                new Claim(ClaimTypes.NameIdentifier, usuarioLog.IdUsuario.ToString()) 
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            _logger.LogInformation("Inicio de sesión correcto para usuario {Usuario}", viewModelLogin.User);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Bienvenido",
                $"Inicio de sesión correcto. Hola, {usuarioLog.NombreCompleto}.",
                SweetAlertMessageType.success
            );

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            _logger.LogInformation("Cierre de sesión correcto de {Usuario}", User.Identity?.Name);

            await HttpContext.SignOutAsync();

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Sesión finalizada",
                "Has cerrado sesión correctamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}