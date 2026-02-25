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
    public class RepositoryEstado: IRepositoryEstado
    {
        private readonly SubastaContext _context;

        public RepositoryEstado(SubastaContext context)
        {
            _context = context;
        }

        public Task<Estado> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Estado>> ListAsync()
        {
            var collection = await _context.Set<Estado>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
