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
    public class RepositoryEstadoPago: IRepositoryEstadoPago
    {
        private readonly SubastaContext _context;

        public RepositoryEstadoPago(SubastaContext context)
        {
            _context = context;
        }

        public Task<EstadoPago> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<EstadoPago>> ListAsync()
        {
            var collection = await _context.Set<EstadoPago>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
