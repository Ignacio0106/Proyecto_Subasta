using Microsoft.AspNetCore.Mvc;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class SubastaaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;
        public SubastaaController(IServiceSubasta serviceSubasta)
        {
            _serviceSubasta = serviceSubasta;
        }
        [HttpGet]
        //public async Task<IActionResult> Index()
        //{
            
        //}
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
    }
}
