using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceObjeto
    {
        Task<ICollection<ObjetoDTO>> ListAsync();
        Task<ObjetoDTO?> FindByIdAsync(int id);
    }
}
