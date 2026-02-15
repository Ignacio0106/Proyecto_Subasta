using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  EstadoDTO
    {
        public int IdEstado { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    }
}
