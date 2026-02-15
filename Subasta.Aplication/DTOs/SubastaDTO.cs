using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public  record SubastaDTO
    {
        public int IdSubasta { get; set; }

        public DateTime FechaHoraInicio { get; set; }

        public DateTime FechaHoraCierre { get; set; }

        public decimal PrecioBase { get; set; }

        public decimal IncrementoMinimo { get; set; }

        public string EstadoSubasta { get; set; } = string.Empty;

        public string UsuarioCreador { get; set; } = string.Empty;

        public string Objeto { get; set; } = string.Empty;
    }
}
