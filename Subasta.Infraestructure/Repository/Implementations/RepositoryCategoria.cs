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

        public async Task<Categoria?> FindByIdAsync(int id)
        {
            return await _context.Set<Categoria>()
               .AsNoTracking()
               .FirstOrDefaultAsync(a => a.IdCategoria == id);
        }

        public async Task<ICollection<Categoria>> ListAsync()
        {
            //Select * from Categoria
            var collection = await _context.Set<Categoria>()
                .AsNoTracking()
                .ToListAsync();
            //throw new Exception("Error de prueba en RepositoryCategoria.ListAsync");
            return collection;
        }

    }
}
