using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  ObjetoDTO
    {
        public int IdObjeto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        public string Vendedor { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public string Condicion { get; set; } = string.Empty;

        public byte[]? ImagenPrincipal { get; set; }
        public List<string> Categorias { get; set; } = new List<string>();

        public List<byte[]>? Imagenes { get; set; } = new List<byte[]>();
        public List<SubastaDTO>? HistorialSubastas { get; set; } = new List<SubastaDTO>();

    }
}
