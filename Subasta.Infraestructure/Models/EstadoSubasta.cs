using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class EstadoSubasta
{
    public int IdEstadoSubasta { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Subastaa> Subasta { get; set; } = new List<Subastaa>();
}
