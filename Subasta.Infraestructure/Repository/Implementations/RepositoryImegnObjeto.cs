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
    public class RepositoryImagenObjeto: IRepositoryImagenObjeto
    {
        private readonly SubastaContext _context;

        public RepositoryImagenObjeto(SubastaContext context)
        {
            _context = context;
        }

        public Task<ImagenObjeto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ImagenObjeto>> ListAsync()
        {
            var collection = await _context.Set<ImagenObjeto>()
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
