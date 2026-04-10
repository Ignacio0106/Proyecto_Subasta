using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record SubastaDTO
    {
        public int IdSubasta { get; set; }
        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaHoraInicio { get; set; }
        [DisplayName("Fecha de Cierre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaHoraCierre { get; set; }
        [DisplayName("Precio Base")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal PrecioBase { get; set; }
        [DisplayName("Incremento Minimo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal IncrementoMinimo { get; set; }
        [DisplayName("Estado de la Subasta")]
        public string EstadoSubasta { get; set; } = string.Empty;
        [DisplayName("Propietario")]
        public string UsuarioCreador { get; set; } = "Sin propietario";

        [DisplayName("Objeto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int IdObjeto { get; set; }

        public string Objeto { get; set; } = "Objeto no definido";
        [DisplayName("Cantidad de Pujas")]
        public int CantidadPujas { get; set; } = 0;
        public byte[]? ImagenPrincipal { get; set; }
        public List<byte[]>? Imagenes { get; set; } = new List<byte[]>();

        public string Condicion { get; set; } = "Sin definir";
        public List<string> Categorias { get; set; } = new List<string>();

        public ObjetoDTO IdObjetoNavigation { get; set; } = new();
        public UsuarioDTO IdUsuarioCreadorNavigation { get; set; } = new();

        public List<PujaDTO> Pujas { get; set; } = new List<PujaDTO>();

        //public PujaDTO? PujaActual { get; set; }
    }
}
