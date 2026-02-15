using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record  PagoDTO
    {
        public int IdPago { get; set; }

        public decimal Monto { get; set; }

        public DateTime? FechaPago { get; set; }

        public string EstadoPago { get; set; } = string.Empty;

        public int IdSubasta { get; set; }
    }
}
