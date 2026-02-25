
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryResultadoSubasta
    {
        Task<ICollection<ResultadoSubasta>> ListAsync();
        Task<ResultadoSubasta> FindByIdAsync(int id);
    }
}
