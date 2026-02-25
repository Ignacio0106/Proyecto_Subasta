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
    public class RepositoryCondicion: IRepositoryCondicion
    {
        private readonly SubastaContext _context;

        public RepositoryCondicion(SubastaContext context)
        {
            _context = context;
        }

        public Task<Condicion> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Condicion>> ListAsync()
        {
            var collection = await _context.Set<Condicion>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
