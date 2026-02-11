using System;
using System.Collections.Generic;

namespace Subasta.Infraestructure.Models;

public partial class ImagenObjeto
{
    public int IdImagen { get; set; }

    public byte[] Imagen { get; set; } = null!;

    public int IdObjeto { get; set; }

    public virtual Objeto IdObjetoNavigation { get; set; } = null!;
}
