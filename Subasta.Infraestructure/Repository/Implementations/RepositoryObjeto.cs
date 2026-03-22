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
                .Include(o => o.Subasta)
                    .ThenInclude(s => s.IdEstadoSubastaNavigation)
                .Include(o => o.IdCondicionNavigation) 
                .Include(o => o.ImagenObjeto)             
                .Include(o => o.IdCategoria)                
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }
        public async Task<int> AddAsync(Objeto entity, int[] selectedCategorias)
        {
            await ApplyCategoriasAsync(entity, selectedCategorias);

            await _context.Set<Objeto>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.IdObjeto;
        }

        public async Task UpdateAsync(
    Objeto entity,
    int[] selectedCategorias,
    List<int> idsImagenesEliminar)
        {
            var objetoBD = await _context.Objeto
                .Include(o => o.ImagenObjeto)
                .Include(o => o.IdCategoria)
                .FirstOrDefaultAsync(o => o.IdObjeto == entity.IdObjeto);

            if (objetoBD == null)
                throw new Exception("Objeto no encontrado");


            if (idsImagenesEliminar.Any())
            {
                var eliminar = objetoBD.ImagenObjeto
                    .Where(i => idsImagenesEliminar.Contains(i.IdImagen))
                    .ToList();

                _context.ImagenObjeto.RemoveRange(eliminar);
            }


            var nuevas = entity.ImagenObjeto
                .Where(i => i.IdImagen == 0)
                .ToList();

            foreach (var img in nuevas)
            {
                objetoBD.ImagenObjeto.Add(img);
            }


            _context.Entry(objetoBD).CurrentValues.SetValues(entity);


            objetoBD.IdCategoria.Clear();

            if (selectedCategorias.Length > 0)
            {
                var categorias = await _context.Categoria
                    .Where(c => selectedCategorias.Contains(c.IdCategoria))
                    .ToListAsync();

                foreach (var cat in categorias)
                {
                    objetoBD.IdCategoria.Add(cat);
                }
            }

            await _context.SaveChangesAsync();
        }
        private async Task ApplyCategoriasAsync(Objeto objetoToUpdate, int[] selectedCategorias)
        {
            if (selectedCategorias == null || selectedCategorias.Length == 0)
            {
                objetoToUpdate.IdCategoria = new List<Categoria>();
                return;
            }

            var ids = selectedCategorias
                .Distinct()
                .ToList();

            if (ids.Count == 0)
            {
                objetoToUpdate.IdCategoria = new List<Categoria>();
                return;
            }

            var categorias = await _context.Categoria
                .Where(c => ids.Contains(c.IdCategoria))
                .ToListAsync();

            objetoToUpdate.IdCategoria = categorias;
        }

        public async Task UpdateEstadoAsync(Objeto entity)
        {
            var objetoBD = await _context.Objeto.FindAsync(entity.IdObjeto);

            if (objetoBD == null)
                throw new Exception("Objeto no encontrado");

            objetoBD.IdEstado = entity.IdEstado;

            await _context.SaveChangesAsync();
        }

    }
}
