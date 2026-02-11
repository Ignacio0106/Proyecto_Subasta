using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string Contrasenna { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int IdRol { get; set; }

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacion { get; set; } = new List<Notificacion>();

    public virtual ICollection<Objeto> Objeto { get; set; } = new List<Objeto>();

    public virtual ICollection<Puja> Puja { get; set; } = new List<Puja>();

    public virtual ICollection<ResultadoSubasta> ResultadoSubasta { get; set; } = new List<ResultadoSubasta>();

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();
}
