using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Objeto> IdObjeto { get; set; } = new List<Objeto>();
}
