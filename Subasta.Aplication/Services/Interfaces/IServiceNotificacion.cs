using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceNotificacion
    {
        Task<ICollection<NotificacionDTO>> ListAsync();
        Task<NotificacionDTO?> FindByIdAsync(int id);
    }
}
