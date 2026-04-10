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
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly SubastaContext _context;

        public RepositoryUsuario(SubastaContext context)
        {
            _context = context;
        }
        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.CorreoElectronico;
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
        public async Task<Usuario> LoginAsync(string correo, string password)
        {
            var @object = await _context.Set<Usuario>()
                                        .Include(b => b.IdRolNavigation)
                                        .Where(p => p.CorreoElectronico == correo && p.Contrasenna == password)
                                        .FirstOrDefaultAsync();
            return @object!;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            var usuarioBD = await _context.Usuario.FindAsync(entity.IdUsuario);

            if (usuarioBD == null)
                throw new Exception("Usuario no encontrado");
            usuarioBD.NombreCompleto = entity.NombreCompleto;
            usuarioBD.CorreoElectronico = entity.CorreoElectronico;

            await _context.SaveChangesAsync();
        }
        public async Task UpdateEstadoAsync(Usuario entity)
        {
            var usuarioBD = await _context.Usuario.FindAsync(entity.IdUsuario);

            if (usuarioBD == null)
                throw new Exception("Usuario no encontrado");

            usuarioBD.IdEstado = entity.IdEstado;

            await _context.SaveChangesAsync();
        }
    }
}
