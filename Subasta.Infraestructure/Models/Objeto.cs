using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class Objeto
{
    public int IdObjeto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int IdUsuarioVendedor { get; set; }

    public int IdEstado { get; set; }

    public int IdCondicion { get; set; }

    public virtual Condicion IdCondicionNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioVendedorNavigation { get; set; } = null!;

    public virtual ICollection<ImagenObjeto> ImagenObjeto { get; set; } = new List<ImagenObjeto>();

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    public virtual ICollection<Categoria> IdCategoria { get; set; } = new List<Categoria>();
}
