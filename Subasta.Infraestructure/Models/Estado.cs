using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Objeto> Objeto { get; set; } = new List<Objeto>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
