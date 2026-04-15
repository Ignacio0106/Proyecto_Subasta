using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Web.Hubs;
using System.Security.Claims;
using Libreria.Web.Util;

namespace Subasta.Web.Controllers
{
    public class PagoController : Controller
    {
        private readonly IServicePago _servicePago;
        private readonly IHubContext<SubastaHub> _hubContext;

        public PagoController(IServicePago servicePago, IHubContext<SubastaHub> hubContext)
        {
            _servicePago = servicePago;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _servicePago.ListAsync();
            return View(collection);
        }

        [HttpGet]
        public async Task<IActionResult> ListPagosByUser()
        {
            var usuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var collection = await _servicePago.ListPagosByUserAsync(usuario);
            return View(collection);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarPago(int id)
        {
            try
            {
                var dto = await _servicePago.FindBySubastaAsync(id);

                var usuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (dto != null && dto.IdUsuarioGanador != usuario)
                {

                    TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                        "Error",
                        $"No eres el ganador de esta subasta.",
                        SweetAlertMessageType.error
                    );

                    return RedirectToAction("ListPagosByUser", "Pago");
                }


                return View(dto);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    exito = false,
                    mensaje = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPago(PagoDTO dto)
        {
            try
            {
                var usuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var pago = await _servicePago.FindByIdAsync(dto.IdPago);


                if (pago.IdEstadoPago == 2)
                {
                    TempData["Error"] = "Este pago ya está confirmado.";
                    return RedirectToAction("ListPagosByUser", "Pago");
                }


                await _servicePago.RegistrarPagoAsync(dto, usuario);

                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Pago exitoso",
                    $"El pago fue confirmado correctamente.",
                    SweetAlertMessageType.success
                );

                return RedirectToAction("ListPagosByUser", "Pago");
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
