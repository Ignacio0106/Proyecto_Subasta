
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryRol
    {
        Task<ICollection<Rol>> ListAsync();
        Task<Rol> FindByIdAsync(int id);
    }
}
