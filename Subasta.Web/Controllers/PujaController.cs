using Microsoft.AspNetCore.Mvc;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class PujaController : Controller
    {
        private readonly IServicePuja _servicePuja;
        public PujaController(IServicePuja servicePuja)
        {
            _servicePuja = servicePuja;
        }
        [HttpGet]

        public async Task<IActionResult> HistorialPujas(int id)
        {
            var pujas = await _servicePuja.ListBySubastaAsync(id);
            ViewBag.IdSubasta = id;
            return View(pujas);
        }

    }
}
