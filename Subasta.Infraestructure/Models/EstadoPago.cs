using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class EstadoPago
{
    public int IdEstadoPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
