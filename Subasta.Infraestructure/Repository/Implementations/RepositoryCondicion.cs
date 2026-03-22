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

        public async Task<Condicion?> FindByIdAsync(int id)
        {
            return await _context.Set<Condicion>()
               .AsNoTracking()
               .FirstOrDefaultAsync(a => a.IdCondicion == id);
        }

        public async Task<ICollection<Condicion>> ListAsync()
        {
            //Select * from Condicion
            var collection = await _context.Set<Condicion>()
                .AsNoTracking()
                .ToListAsync();
            //throw new Exception("Error de prueba en RepositoryCondicion.ListAsync");
            return collection;
        }

    }
}
