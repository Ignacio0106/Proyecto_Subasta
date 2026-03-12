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
                // Recopilar todos los errores del ModelState
                var errores = string.Join("<br>",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                );

                // Notificación SweetAlert con el detalle de errores
                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}",
                    SweetAlertMessageType.warning
                );
                return View(dto);
            }

            await _serviceUsuario.UpdateAsync(id, dto);
            //Notificar creación
            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                  "Usuario actualizado",
                  $"El usuario {dto.NombreCompleto} ha sido modificado exitosamente.",
                  SweetAlertMessageType.success
              );
            return RedirectToAction(nameof(Index));
        }
    }
}
