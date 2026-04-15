using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.DTOs
{
    public record PagoDTO
    {
        [DisplayName("Identificador Estado del Pago")]
        public int IdPago { get; set; }
        [DisplayName("Monto")]
        public decimal Monto { get; set; }
        [DisplayName("Fecha de Pago")]
        public DateTime? FechaPago { get; set; }
        [DisplayName("Estado Pago")]
        public int IdEstadoPago { get; set; }

        public EstadoPagoDTO IdEstadoPagoNavigation { get; set; } = new();

        public int IdSubasta { get; set; }
        public SubastaDTO IdSubastaNavigation { get; set; } = new();

        public string NombreUsuarioGanador { get; set; } = string.Empty;
        public int IdUsuarioGanador { get; set; }

    }
}
