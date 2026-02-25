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
    public class RepositoryCategoria: IRepositoryCategoria
    {
        private readonly SubastaContext _context;

        public RepositoryCategoria(SubastaContext context)
        {
            _context = context;
        }

        public Task<Categoria> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Categoria>> ListAsync()
        {
            var collection = await _context.Set<Categoria>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
