using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  EstadoSubasta_DTO
    {
        public int IdEstadoSubasta { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        // public List<SubastaDTO> Subastas { get; set; } = new();
    }
}
