using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  ImagenObjetoDTO
    {
        public int IdImagen { get; set; }

        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        public int IdObjeto { get; set; }
    }
}
