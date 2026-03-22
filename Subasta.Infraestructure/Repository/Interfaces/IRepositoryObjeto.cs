
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryObjeto
    {
        Task<ICollection<Objeto>> ListAsync();
        Task<Objeto?> FindByIdAsync(int id);
        Task<int> AddAsync(Objeto entity, int[] selectedCategorias);
        Task UpdateAsync(Objeto entity, int[] selectedCategorias,
    List<int> idsImagenesEliminar);
        Task UpdateEstadoAsync(Objeto entity);
    }
}
