using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Condicion
{
    public int IdCondicion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Objeto> Objeto { get; set; } = new List<Objeto>();
}
