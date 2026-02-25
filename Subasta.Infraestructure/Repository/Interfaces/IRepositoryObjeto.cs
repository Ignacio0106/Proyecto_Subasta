
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryObjeto
    {
        Task<ICollection<Objeto>> ListAsync();
        Task<Objeto?> FindByIdAsync(int id);
    }
}
