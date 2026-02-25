using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Subastaa
{
    public int IdSubasta { get; set; }

    public DateTime FechaHoraInicio { get; set; }

    public DateTime FechaHoraCierre { get; set; }

    public decimal PrecioBase { get; set; }

    public decimal IncrementoMinimo { get; set; }

    public int IdEstadoSubasta { get; set; }

    public int IdUsuarioCreador { get; set; }

    public int IdObjeto { get; set; }

    public virtual EstadoSubasta IdEstadoSubastaNavigation { get; set; } = null!;

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual Pago? Pago { get; set; }

    public virtual ICollection<Puja> Puja { get; set; } = new List<Puja>();

    public virtual ResultadoSubasta? ResultadoSubasta { get; set; }
}
