using System;
using System.Collections.Generic;
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

        public DateTime FechaRegistro { get; set; }

        public string Vendedor { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public string Condicion { get; set; } = string.Empty;
    }
}
