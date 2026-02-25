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

    }
}
