using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Web.Helpers;
using Subasta.Web.Hubs;
using System.Security.Claims;

namespace Subasta.Web.Controllers
{
    public class SubastaaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;
        private readonly IServiceObjeto _serviceObjeto;
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IHubContext<SubastaHub> _hubContext;

        private readonly int idUsuario = 3;

        public SubastaaController(IServiceSubasta serviceSubasta, IServiceObjeto serviceObjeto, IServiceUsuario serviceUsuario, IHubContext<SubastaHub> hubContext)
        {
            _serviceSubasta = serviceSubasta;
            _serviceObjeto = serviceObjeto;
            _serviceUsuario = serviceUsuario;
            _hubContext = hubContext;
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
        public async Task<IActionResult> Borradores()
        {
            var borradores = await _serviceSubasta.ListBorradores();
            return View(borradores);
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
        private async Task LoadCombosAsync()
        {
            // Objetos
            var activas = await _serviceObjeto.ListActivas();
            var subastasA = await _serviceSubasta.ListActivas();
            var subastasF = await _serviceSubasta.ListFinalizadas();
            var subastasC = await _serviceSubasta.ListBorradores();

            var objetosEnSubasta = subastasA
            .Select(s => s.IdObjeto)
            .Concat(subastasF.Select(s => s.IdObjeto))
            .Concat(subastasC.Select(s => s.IdObjeto))
            .ToHashSet();

            // Filtrar objetos que NO estén en subastas
            var objetosDisponibles = activas
                .Where(o => !objetosEnSubasta.Contains(o.IdObjeto))
                .ToList();

            ViewBag.ListObjetos = objetosDisponibles
    .Select(o => new {
        o.IdObjeto,
        Nombre = o.Nombre + " - " + o.Descripcion
    }).ToList();
        }

        public async Task<IActionResult> Create()
        {
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

                if (!ModelState.IsValid)
                {
                    await LoadCombosAsync();
                    return View(dto);
                }

                await _serviceSubasta.AddAsync(dto, idUsuario, estado);

                TempData["Mensaje"] = $"La subasta del objeto fue creada correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _serviceSubasta.FindByIdAsync(id);
            await LoadCombosAsync();
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
                if (!ModelState.IsValid)
                {
                    await LoadCombosAsync();
                    dto.Objeto = subastaActual.Objeto;
                    dto.Imagenes = subastaActual.Imagenes;
                    dto.Categorias = subastaActual.Categorias;
                    dto.Condicion = subastaActual.Condicion;

                    return View(dto);
                }
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

        public async Task<IActionResult> Pujar(int id)
        {
            var dto = await _serviceSubasta.FindByIdAsync(id);

            if (dto == null)
            {
                // Esto evita que la vista reciba un modelo nulo
                return NotFound();
            }

            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pujar(int idSubasta, decimal monto)
        {
            try
            {
                var subasta = await _serviceSubasta.FindByIdAsync(idSubasta);

                if (subasta == null)
                    return BadRequest("Subasta no existe");

                // 🔥 VALIDACIÓN CLAVE
                if (monto <= subasta.Pujas[0].MontoOfertado)
                {
                    TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                        "Puja inválida",
                        "El monto debe ser mayor a la puja actual",
                        SweetAlertMessageType.warning
                    );

                    return RedirectToAction("Details", new { id = idSubasta });
                }
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int idUsuario = int.Parse(userIdString);

                // 🔥 CREAR PUJA (aquí deberías tener un servicio real)
                await _serviceSubasta.RegistrarPuja(idSubasta, idUsuario, monto);

                // 🔥 SIGNALR (AQUÍ ESTÁ LA MAGIA)
                await _hubContext.Clients.Group($"Subasta-{idSubasta}")
                    .SendAsync("NuevaPuja", new
                    {
                        usuario = "Usuario " + idUsuario, // luego lo mejoras
                        monto = monto,
                        fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                    });

                return Ok(); // ⚠️ IMPORTANTE para AJAX
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
