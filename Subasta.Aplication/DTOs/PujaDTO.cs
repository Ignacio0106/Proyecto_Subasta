using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  PujaDTO
    {
        public int IdPuja { get; set; }

        public decimal MontoOfertado { get; set; }

        public DateTime FechaHora { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public int IdSubasta { get; set; }
    }
}
