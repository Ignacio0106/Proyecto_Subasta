using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class ResultadoSubasta
{
    public int IdResultado { get; set; }

    public int IdSubasta { get; set; }

    public int IdUsuarioGanador { get; set; }

    public decimal MontoFinal { get; set; }

    public DateTime FechaCierre { get; set; }

    public virtual Subastaa IdSubastaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioGanadorNavigation { get; set; } = null!;
}
