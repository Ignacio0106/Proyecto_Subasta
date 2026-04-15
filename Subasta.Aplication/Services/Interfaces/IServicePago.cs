using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServicePago
    {
        Task<ICollection<PagoDTO>> ListAsync();
        Task<PagoDTO?> FindByIdAsync(int id);
        Task<ICollection<PagoDTO>> ListPagosByUserAsync(int id);
        Task RegistrarPagoAsync(PagoDTO dto, int idUsuario);
        Task<PagoDTO?> FindBySubastaAsync(int id);
    }
}
