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
        public async Task<Usuario?> FindByIdAsync(int id)
        {
            return await _context.Usuario
            .Include(u => u.IdRolNavigation)
            .Include(u => u.IdEstadoNavigation)
            .Include(u => u.Subasta)
                .Include(u => u.Puja)

            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            return await _context.Usuario
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdEstadoNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Usuario entity)
        {
            // entity DEBE venir trackeado 
            // Igual se reestablece 
            if (_context.Entry(entity).State == EntityState.Detached) //Detached no sabe si es nuevo o existente, pero se asume existente porque viene con Id
            {
                _context.Attach(entity);
            }

            // Si el mapping ya actualizó propiedades escalares, esto garantiza update 
            _context.Entry(entity).State = EntityState.Modified;

            // Si por alguna razón la navigation del autor viene nula: 
            // (No es obligatorio para guardar FK) 
            //if (entity.IdAutorNavigation == null && entity.IdAutor > 0)
            //{
            //    var autor = await _context.Autor.FindAsync(entity.IdAutor);

            //    if (autor != null)
            //    {
            //        entity.IdAutorNavigation = autor;
            //    }
            //}

            await _context.SaveChangesAsync();
        }
    }
}
