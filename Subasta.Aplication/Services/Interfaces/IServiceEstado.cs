using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceEstado
    {
        Task<ICollection<EstadoDTO>> ListAsync();
        Task<EstadoDTO?> FindByIdAsync(int id);
    }
}
