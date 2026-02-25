
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryNotificacion
    {
        Task<ICollection<Notificacion>> ListAsync();
        Task<Notificacion> FindByIdAsync(int id);
    }
}
