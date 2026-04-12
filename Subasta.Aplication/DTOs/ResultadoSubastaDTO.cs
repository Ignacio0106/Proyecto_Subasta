using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  ResultadoSubastaDTO
    {
        public int IdResultado { get; set; }

        public decimal MontoFinal { get; set; }

        public DateTime FechaCierre { get; set; }

        public string Subasta { get; set; } = string.Empty;

        public int IdUsuarioGanador { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;

    }
}
