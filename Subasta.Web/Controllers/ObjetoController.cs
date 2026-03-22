using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.Helpers;

namespace Subasta.Web.Controllers
{
    public class ObjetoController : Controller
    {
        private readonly IServiceObjeto _serviceObjeto;
        private readonly IServiceCategoria _serviceCategoria;
        private readonly IServiceCondicion _serviceCondicion;
        private readonly IServiceUsuario _serviceUsuario;

        private readonly int idUsuario = 3;

        public ObjetoController(
            IServiceObjeto serviceObjeto,
            IServiceCategoria serviceCategoria,
            IServiceCondicion serviceCondicion,
            IServiceUsuario serviceUsuario)
        {
            _serviceObjeto = serviceObjeto;
            _serviceCategoria = serviceCategoria;
            _serviceCondicion = serviceCondicion;
            _serviceUsuario = serviceUsuario;
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

            

            return View(Objeto);
        }
        private async Task LoadCombosAsync(IEnumerable<string>? selectedCategorias = null)
        {
            // Condiciones
            ViewBag.ListCondicion = await _serviceCondicion.ListAsync();

            // Categorías
            var categorias = await _serviceCategoria.ListAsync();

            ViewBag.ListCategorias = new MultiSelectList(
                categorias,
                nameof(CategoriaDTO.IdCategoria),
                nameof(CategoriaDTO.Nombre),
                selectedCategorias
            );
        }
        public async Task<IActionResult> Create()
        {
            var usuario = await _serviceUsuario.FindByIdAsync(idUsuario);

            ViewBag.UsuarioActual = usuario?.NombreCompleto;
            ViewBag.Estado = "Activo";

            await LoadCombosAsync();
            return View(new ObjetoDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ObjetoDTO dto, string[] selectedCategorias, List<IFormFile> imageFiles)
        {
            try
            {
                var usuario = await _serviceUsuario.FindByIdAsync(idUsuario);

                ViewBag.UsuarioActual = usuario?.NombreCompleto ?? "Usuario";
                ViewBag.Estado = "Activo";

                if (selectedCategorias == null || selectedCategorias.Length == 0)
                {
                    ModelState.AddModelError("selectedCategorias", "Debe seleccionar al menos una categoría.");
                }

                if (imageFiles == null || imageFiles.Count == 0)
                {
                    ModelState.AddModelError("Imagenes", "Debe subir al menos una imagen.");
                }

                if (!ModelState.IsValid)
                {
                    await LoadCombosAsync(selectedCategorias);
                    return View(dto);
                }


                dto.FechaRegistro = DateTime.Now;

                dto.Imagenes = new List<byte[]>();

                foreach (var file in imageFiles)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        dto.Imagenes.Add(ms.ToArray());
                    }
                }

                await _serviceObjeto.AddAsync(dto, selectedCategorias, idUsuario);

                TempData["Mensaje"] = $"El objeto {dto.Nombre} fue creado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _serviceObjeto.FindByIdAsync(id);

            var selected = dto.CategoriasIds
                .Select(x => x.ToString())
                .ToList();

            await LoadCombosAsync(selected);
            
            return View(dto);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
     ObjetoDTO dto,
     string[] selectedCategorias,
     string[] imagenesAEliminar,
     List<IFormFile> nuevasImagenes)
        {
            try
            {
                selectedCategorias ??= Array.Empty<string>();
                imagenesAEliminar ??= Array.Empty<string>();
                nuevasImagenes ??= new List<IFormFile>();

                int id = dto.IdObjeto;

                var objetoActual = await _serviceObjeto.FindByIdAsync(id);

                if (objetoActual == null)
                {
                    return Content("Error: Objeto no encontrado (ID: " + id + ")");
                }

                if (selectedCategorias.Length == 0)
                {
                    ModelState.AddModelError("selectedCategorias", "Debe seleccionar al menos una categoría.");
                }

                int actuales = objetoActual.ImagenesIds?.Count ?? 0;
                int eliminar = imagenesAEliminar.Length;
                int nuevas = nuevasImagenes.Count;

                if (actuales > 0 && eliminar == actuales && nuevas == 0)
                {
                    ModelState.AddModelError("Imagenes", "Debe mantener al menos una imagen o subir una nueva.");
                }

                if (!ModelState.IsValid)
                {
                    dto.Vendedor = objetoActual.Vendedor;
                    dto.Estado = objetoActual.Estado;
                    dto.Imagenes = objetoActual.Imagenes;
                    dto.ImagenesIds = objetoActual.ImagenesIds;

                    await LoadCombosAsync(selectedCategorias);
                    return View(dto);
                }


                var nuevasImagenesBytes = new List<byte[]>();

                foreach (var file in nuevasImagenes)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        nuevasImagenesBytes.Add(ms.ToArray());
                    }
                }

                await _serviceObjeto.UpdateAsync(
                    id,
                    dto,
                    selectedCategorias,
                    imagenesAEliminar,
                    nuevasImagenesBytes
                );

                TempData["Mensaje"] = $"El objeto {dto.Nombre} fue actualizado correctamente.";
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
            await _serviceObjeto.ToggleEstadoAsync(id);

            TempData["Mensaje"] = "Estado actualizado correctamente";

            return RedirectToAction(nameof(Index));
        }
    }
}
