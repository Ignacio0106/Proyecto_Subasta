using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  RolDTO
    {
        public int IdRol { get; set; }

        public string NombreRol { get; set; } = string.Empty;

       // public List<UsuarioDTO> Usuarios { get; set; } = new(); si se quieren ver los usuarios de un rol
    }
}
