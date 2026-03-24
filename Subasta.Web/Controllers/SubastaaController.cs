using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class SubastaaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;
        private readonly IServiceObjeto _serviceObjeto;
        private readonly IServiceUsuario _serviceUsuario;

        private readonly int idUsuario = 2;

        public SubastaaController(IServiceSubasta serviceSubasta, IServiceObjeto serviceObjeto, IServiceUsuario serviceUsuario)
        {
            _serviceSubasta = serviceSubasta;
            _serviceObjeto = serviceObjeto;
            _serviceUsuario = serviceUsuario;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string filtro)
        {
            var subastas = await _serviceSubasta.ListAsync();

            if (filtro == "Activas")
                subastas = subastas.Where(s => s.EstadoSubasta == "Activa").ToList();

            if (filtro == "Finalizadas")
                subastas = subastas.Where(s => s.EstadoSubasta == "Finalizada").ToList();

            return View(subastas);
        }
        public async Task<IActionResult> Activas()
        {
            var activas = await _serviceSubasta.ListActivas();
            return View(activas); 
        }

        // Vista de subastas finalizadas
        public async Task<IActionResult> Finalizadas()
        {
            var finalizadas = await _serviceSubasta.ListFinalizadas();
            return View(finalizadas); 
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Subasta no encontrada",
                    "No existe una subasta sin ID.",
                    SweetAlertMessageType.error
                );
                return RedirectToAction("Activas");
            }

            var Subasta = await _serviceSubasta.FindByIdAsync(id.Value);

            if (Subasta == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Subasta no encontrada",
                    "La subasta solicitada no existe.",
                    SweetAlertMessageType.error
                );
                return RedirectToAction("Activas");
            }

            ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                "Detalle de Subasta",
                $"Mostrando información de: {Subasta.Objeto}",
                SweetAlertMessageType.info
            );

            return View(Subasta);
        }

        // -------------------------
        // Helpers para combos
        // -------------------------
        private async Task LoadCombosAsync(IEnumerable<string>? selectedCategoriaIds = null)
        {
            // Objetos
            var activas = await _serviceObjeto.ListActivas();
            var subastasA = await _serviceSubasta.ListActivas();
            var subastasF = await _serviceSubasta.ListFinalizadas();

            var objetosEnSubasta = subastasA
            .Select(s => s.IdObjeto)
            .Concat(subastasF.Select(s => s.IdObjeto))
            .ToHashSet();

            // Filtrar objetos que NO estén en subastas
            var objetosDisponibles = activas
                .Where(o => !objetosEnSubasta.Contains(o.IdObjeto))
                .ToList();

            ViewBag.ListObjetos = objetosDisponibles;
        }

        public async Task<IActionResult> Create()
        {
            //var usuario = await _serviceSubasta.AddAsync(dto);

            //ViewBag.UsuarioActual = usuario?.NombreCompleto;
            //ViewBag.Estado = "Activo";

            await LoadCombosAsync();

            var usuario = await _serviceUsuario.FindByIdAsync(idUsuario);

            ViewBag.UsuarioActual = usuario?.NombreCompleto ?? "Usuario";

            return View(new SubastaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaDTO dto, int estado)
        {
            try
            {
                var usuario = await _serviceUsuario.FindByIdAsync(idUsuario);

                ViewBag.UsuarioActual = usuario?.NombreCompleto ?? "Usuario";

                if (dto.FechaHoraCierre <= dto.FechaHoraInicio)
                {
                    ModelState.AddModelError("FechaHoraCierre", "La fecha de cierre debe ser mayor que la fecha de inicio");
                }

                dto.EstadoSubasta = "Activa"; 

                await _serviceSubasta.AddAsync(dto, idUsuario, estado);

                TempData["Mensaje"] = $"La subasta del objeto{dto.Objeto} fue creada correctamente.";

                return RedirectToAction(nameof(Activas));
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _serviceSubasta.FindByIdAsync(id);

            return View(dto);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubastaDTO dto)
        {
            try
            {
                int id = dto.IdSubasta;

                var subastaActual = await _serviceSubasta.FindByIdAsync(id);

                if (subastaActual == null)
                {
                    return Content("Error: Subasta no encontrada (ID: " + id + ")");
                }

                if (dto.FechaHoraCierre <= dto.FechaHoraInicio)
                {
                    ModelState.AddModelError("FechaHoraCierre", "La fecha de cierre debe ser mayor que la fecha de inicio");
                }
                //if (!ModelState.IsValid)
                //{
                //    await LoadCombosAsync();
                //    return View(dto);
                //}
                await _serviceSubasta.UpdateAsync(id, dto);

                TempData["Mensaje"] = $"La subasta{dto.IdSubasta} fue actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleEstado(int id)
        {
            await _serviceSubasta.ToggleEstadoAsync(id);

            TempData["Mensaje"] = "Estado actualizado correctamente";

            return RedirectToAction(nameof(Index));
        }
    }
}
