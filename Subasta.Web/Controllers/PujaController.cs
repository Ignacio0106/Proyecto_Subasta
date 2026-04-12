using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Web.Helpers;
using Subasta.Web.Hubs;

namespace Subasta.Web.Controllers
{
    public class PujaController : Controller
    {
        private readonly IServicePuja _servicePuja;
        private readonly IHubContext<SubastaHub> _hubContext;
        private const int UsuarioActualId = 2; // Cambia este valor para simular usuarios

        public PujaController(IServicePuja servicePuja, IHubContext<SubastaHub> hubContext)
        {
            _servicePuja = servicePuja;
            _hubContext = hubContext;
        }
        [HttpGet]

        public async Task<IActionResult> HistorialPujas(int id)
        {
            var pujas = await _servicePuja.ListBySubastaAsync(id);
            ViewBag.IdSubasta = id;
            return View(pujas);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> RegistrarPuja(int idSubasta, decimal monto)
        {
            try
            {
                var (exito, mensaje) = await _servicePuja.RegistrarPujaAsync(idSubasta, monto, UsuarioActualId);

                if (!exito)
                    return Json(new { exito = false, mensaje });

                var pujas = await _servicePuja.ListBySubastaAsync(idSubasta);
                var pujaLider = pujas.OrderByDescending(p => p.MontoOfertado).First();

                await _hubContext.Clients.Group($"Subasta-{idSubasta}")
                    .SendAsync("NuevaPuja", new
                    {
                        montoLider = pujaLider.MontoOfertado,
                        usuarioLider = pujaLider.NombreUsuario,
                        historial = pujas.Select(p => new
                        {
                            nombreUsuario = p.NombreUsuario,
                            montoOfertado = p.MontoOfertado,
                            fechaHora = p.FechaHora.ToString("s")
                        })
                    });

                return Json(new { exito = true });
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
    }
}
