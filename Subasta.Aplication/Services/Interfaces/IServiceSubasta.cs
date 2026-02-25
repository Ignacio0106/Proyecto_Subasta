using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<ICollection<SubastaDTO>> ListActivas();
        Task<ICollection<SubastaDTO>> ListFinalizadas();
        Task<SubastaDTO?> FindByIdAsync(int id);
    }
}
