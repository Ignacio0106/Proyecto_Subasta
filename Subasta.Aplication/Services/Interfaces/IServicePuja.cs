using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServicePuja
    {
        Task<ICollection<PujaDTO>> ListBySubastaAsync(int idSubasta);
        Task<PujaDTO?> FindByIdAsync(int id);
    }
}
