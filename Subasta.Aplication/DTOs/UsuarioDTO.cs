using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.DTOs
{
    public record UsuarioDTO
    {
        public int IdUsuario { get; set; }
        [DisplayName("Correo Electronico")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [DisplayName("Contraseña")]
        public string Contrasenna { get; set; } = string.Empty;
        [DisplayName("Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;
        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        [DisplayName("Rol")]
        public int IdRol { get; set; }
        [DisplayName("Estado")]
        public int IdEstado { get; set; }
        public string Rol { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        [DisplayName("Cantidad de Subastas")]
        public int? CantidadSubastas { get; set; }
        [DisplayName("Cantidad de Pujas")]
        public int? CantidadPujas { get; set; }

    }
}
