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
    public class RepositoryRol: IRepositoryRol
    {
        private readonly SubastaContext _context;

        public RepositoryRol(SubastaContext context)
        {
            _context = context;
        }

        public Task<Rol> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Rol>> ListAsync()
        {
            var collection = await _context.Set<Rol>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
