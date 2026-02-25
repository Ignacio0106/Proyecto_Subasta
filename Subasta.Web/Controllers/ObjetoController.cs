using Microsoft.AspNetCore.Mvc;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class ObjetoController : Controller
    {
        private readonly IServiceObjeto _serviceObjeto;
        public ObjetoController(IServiceObjeto serviceObjeto)
        {
            _serviceObjeto = serviceObjeto;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceObjeto.ListAsync();
            return View(collection);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Objeto no encontrado",
                    "No existe un Objeto sin ID.",
                    SweetAlertMessageType.error
                );

                return RedirectToAction("Index");
            }

            var Objeto = await _serviceObjeto.FindByIdAsync(id.Value);

            if (Objeto == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Objeto no encontrado",
                    "El Objeto solicitado no existe.",
                    SweetAlertMessageType.error
                );

                return RedirectToAction("Index");
            }

            ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
    "Detalle de Objeto",
    $"Mostrando información de: {Objeto.Nombre}",
    SweetAlertMessageType.info
);

            return View(Objeto);
        }
    }
}
