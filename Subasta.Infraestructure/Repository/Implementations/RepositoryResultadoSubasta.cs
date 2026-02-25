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
    public class RepositoryResultadoSubasta: IRepositoryResultadoSubasta
    {
        private readonly SubastaContext _context;

        public RepositoryResultadoSubasta(SubastaContext context)
        {
            _context = context;
        }

        public Task<ResultadoSubasta> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ResultadoSubasta>> ListAsync()
        {
            var collection = await _context.Set<ResultadoSubasta>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
