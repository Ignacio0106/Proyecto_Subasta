using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Notificacion
{
    public int IdNotificacion { get; set; }

    public int IdUsuario { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public bool Leida { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
