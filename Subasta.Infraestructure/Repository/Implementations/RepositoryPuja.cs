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
    public class RepositoryPuja : IRepositoryPuja
    {
        private readonly SubastaContext _context;

        public RepositoryPuja(SubastaContext context)
        {
            _context = context;
        }

        public Task<Puja> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Puja>> ListBySubastaAsync(int idSubasta)
        {
            return await _context.Puja
                .Include(p => p.IdUsuarioNavigation)
                .Where(p => p.IdSubasta == idSubasta)
                .OrderByDescending(p => p.FechaHora)
                .AsNoTracking()
                .ToListAsync();
        }

        

        public async Task<int> AddAsync(Puja puja)
        {
            _context.Puja.Add(puja);

            var result = await _context.SaveChangesAsync();

            Console.WriteLine("Filas afectadas: " + result);

            return result;
        }    

    public async Task<Puja?> GetPujaMaximaEntidadAsync(int idSubasta)
        {
            return await _context.Puja
                .Where(p => p.IdSubasta == idSubasta)
                .OrderByDescending(p => p.MontoOfertado)
                .FirstOrDefaultAsync();
        }
    }
    }
