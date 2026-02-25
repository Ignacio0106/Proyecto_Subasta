using Microsoft.AspNetCore.Mvc;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        public UsuarioController(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Usuario no encontrado",
                    "No existe un usuario sin ID.",
                    SweetAlertMessageType.error
                );

                return RedirectToAction("Index");
            }

            var usuario = await _serviceUsuario.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Usuario no encontrado",
                    "El usuario solicitado no existe.",
                    SweetAlertMessageType.error
                );

                return RedirectToAction("Index");
            }

            ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                "Detalle de Usuario",
                $"Mostrando información de: {usuario.NombreCompleto}",
                SweetAlertMessageType.info
            );

            return View(usuario);
        }
    }
}
