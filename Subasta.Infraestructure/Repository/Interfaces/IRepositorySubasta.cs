
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySubasta
    {
        Task<ICollection<Subastaa >> ListAsync();
        Task<Subastaa? > FindByIdAsync(int id);

        Task<int> AddAsync(Subastaa entity);
        Task UpdateAsync(Subastaa entity);

        Task UpdateEstadoAsync(Subastaa entity);
    }
}
