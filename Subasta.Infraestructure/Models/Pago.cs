using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public decimal Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public int IdEstadoPago { get; set; }

    public int IdSubasta { get; set; }

    public virtual EstadoPago IdEstadoPagoNavigation { get; set; } = null!;

    public virtual Subastaa IdSubastaNavigation { get; set; } = null!;
}
