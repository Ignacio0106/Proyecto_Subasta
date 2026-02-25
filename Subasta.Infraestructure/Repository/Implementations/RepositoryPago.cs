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
    public class RepositoryPago: IRepositoryPago
    {
        private readonly SubastaContext _context;

        public RepositoryPago(SubastaContext context)
        {
            _context = context;
        }

        public Task<Pago> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Pago>> ListAsync()
        {
            var collection = await _context.Set<Pago>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
