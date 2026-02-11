using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.DTOs
{
    public record UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string CorreoElectronico { get; set; } = string.Empty!;

        public string Contrasenna { get; set; } = string.Empty!;

        public string NombreCompleto { get; set; } = string.Empty!;

        public DateTime FechaRegistro { get; set; }

        public int IdRol { get; set; }

        public int IdEstado { get; set; }

        // public virtual Estado IdEstadoNavigation { get; set; } = null!;

        // public virtual Rol IdRolNavigation { get; set; } = null!;

    }
}
