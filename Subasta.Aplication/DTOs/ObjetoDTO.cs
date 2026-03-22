using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record ObjetoDTO
    {
        public int IdObjeto { get; set; }

        [DisplayName("Nombre del objeto")]
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "{0} es requerida")]
        [MinLength(20, ErrorMessage = "La {0} debe tener mínimo {1} caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Condición")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int IdCondicion { get; set; }

        public string Condicion { get; set; } = string.Empty;

        [DisplayName("Vendedor")]
        public string Vendedor { get; set; } = string.Empty;
        public int IdVendedor { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; } = string.Empty;

        public byte[]? ImagenPrincipal { get; set; }

        public List<string> Categorias { get; set; } = new();
        public List<int> CategoriasIds { get; set; } = new();
        public List<int> ImagenesIds { get; set; } = new();

        [DisplayName("Imágenes")]
        public List<byte[]>? Imagenes { get; set; } = new();

        public List<SubastaDTO>? HistorialSubastas { get; set; } = new();

    }
}