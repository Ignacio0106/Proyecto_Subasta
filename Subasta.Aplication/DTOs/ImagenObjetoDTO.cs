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

        public string ImagenBase64 { get; set; } = string.Empty;

        public int IdObjeto { get; set; }
    }
}
