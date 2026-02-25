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
    public class RepositorySubasta: IRepositorySubasta
    {
        private readonly SubastaContext _context;

        public RepositorySubasta(SubastaContext context)
        {
            _context = context;
        }

        public async Task<Subastaa?> FindByIdAsync(int id)
        {
            return await _context.Subasta
                .Include(s => s.IdObjetoNavigation)
                    .ThenInclude(o => o.IdCondicionNavigation)
                .Include(s => s.IdObjetoNavigation)
                    .ThenInclude(o => o.IdCategoria)
                .Include(s => s.IdObjetoNavigation)
                    .ThenInclude(o => o.ImagenObjeto)
                .Include(s => s.IdUsuarioCreadorNavigation)
                .Include(s => s.IdEstadoSubastaNavigation)
                .Include(s => s.Puja)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.IdSubasta == id);
        }

        public async Task<ICollection<Subastaa>> ListAsync()
        {
            return await _context.Subasta
        .Include(s => s.IdUsuarioCreadorNavigation)      
        .Include(s => s.Puja)                 
        .Include(s => s.IdObjetoNavigation)
        .ThenInclude(o => o.ImagenObjeto)
        .Include(s => s.IdEstadoSubastaNavigation)   
        .AsNoTracking()
        .ToListAsync();
        }

    }
}
