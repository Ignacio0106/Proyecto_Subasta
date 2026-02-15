using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  NotificacionDTO
    {
        public int IdNotificacion { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public bool Leida { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;
    }
}
