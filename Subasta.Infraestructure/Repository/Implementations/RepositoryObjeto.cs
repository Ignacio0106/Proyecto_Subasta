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
    public class RepositoryObjeto: IRepositoryObjeto
    {
        private readonly SubastaContext _context;

        public RepositoryObjeto(SubastaContext context)
        {
            _context = context;
        }

        public async Task<Objeto?> FindByIdAsync(int id)
        {
            return await _context.Objeto
                .Include(o => o.IdUsuarioVendedorNavigation)
                .Include(o => o.IdEstadoNavigation)
                .Include(o => o.IdCondicionNavigation)
                .Include(o => o.ImagenObjeto)
                .Include(o => o.IdCategoria)
                .Include(o => o.Subasta)
                    .ThenInclude(s => s.IdEstadoSubastaNavigation)
                .Include(o => o.Subasta)
                    .ThenInclude(s => s.IdUsuarioCreadorNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.IdObjeto == id);
        }

        public async Task<ICollection<Objeto>> ListAsync()
        {
            var collection = await _context.Objeto
                .Include(o => o.IdUsuarioVendedorNavigation) 
                .Include(o => o.IdEstadoNavigation)     
                .Include(o => o.IdCondicionNavigation) 
                .Include(o => o.ImagenObjeto)             
                .Include(o => o.IdCategoria)                
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

    }
}
