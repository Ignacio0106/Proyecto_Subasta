using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Subasta.Aplication.DTOs;
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

        // GET: LibroController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dto = await _serviceUsuario.FindByIdAsync(id);

            //var selected = dto.IdCategoria
            //    .Select(c => c.IdCategoria.ToString())
            //    .ToList();

            //await LoadCombosAsync(selected);
            return View(dto);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _serviceUsuario.UpdateAsync(id, dto);

            TempData["Mensaje"] = $"El usuario {dto.NombreCompleto} fue actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleEstado(int id)
        {
            await _serviceUsuario.ToggleEstadoAsync(id);

            TempData["Mensaje"] = "Estado actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
