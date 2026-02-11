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
    public class RepositoryUsuario: IRepositoryUsuario
    {
        private readonly SubastaContext _context;

        public RepositoryUsuario(SubastaContext context)
        {
            _context = context;
        }

        public Task<Usuario> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
