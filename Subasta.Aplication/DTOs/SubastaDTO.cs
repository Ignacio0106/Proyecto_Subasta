using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record SubastaDTO
    {
        public int IdSubasta { get; set; }
        [DisplayName("Fecha de Inicio")]
        public DateTime FechaHoraInicio { get; set; }
        [DisplayName("Fecha de Cierre")]
        public DateTime FechaHoraCierre { get; set; }
        [DisplayName("Precio Base")]
        public decimal PrecioBase { get; set; }
        [DisplayName("Incremento Minimo")]
        public decimal IncrementoMinimo { get; set; }
        [DisplayName("Estado de la Subasta")]
        public string EstadoSubasta { get; set; } = string.Empty;
        [DisplayName("Propietario")]
        public string UsuarioCreador { get; set; } = "Sin propietario";
        public string Objeto { get; set; } = "Objeto no definido";
        [DisplayName("Cantidad de Pujas")]
        public int CantidadPujas { get; set; } = 0;
        public byte[]? Imagen { get; set; }


        public string Condicion { get; set; } = "Sin definir";
        public List<string> Categorias { get; set; } = new List<string>();
    }
}
