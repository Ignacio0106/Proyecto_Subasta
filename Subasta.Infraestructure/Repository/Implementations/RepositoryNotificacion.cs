using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subasta.Infraestructure.Data;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Interfaces;

namespace Subasta.Infraestructure.Repository.Implementations
{
    public class RepositoryNotificacion: IRepositoryNotificacion
    {
        private readonly SubastaContext _context;

        public RepositoryNotificacion(SubastaContext context)
        {
            _context = context;
        }

        public Task<Notificacion> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Notificacion>> ListAsync()
        {
            var collection = await _context.Set<Notificacion>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
