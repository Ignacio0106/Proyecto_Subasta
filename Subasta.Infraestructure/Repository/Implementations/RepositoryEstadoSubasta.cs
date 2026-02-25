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
    public class RepositoryEstadoSubasta: IRepositoryEstadoSubasta
    {
        private readonly SubastaContext _context;

        public RepositoryEstadoSubasta(SubastaContext context)
        {
            _context = context;
        }

        public Task<EstadoSubasta> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<EstadoSubasta>> ListAsync()
        {
            var collection = await _context.Set<EstadoSubasta>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
