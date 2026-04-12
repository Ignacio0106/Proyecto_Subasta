using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<ICollection<SubastaDTO>> ListAsync();
        Task<ICollection<SubastaDTO>> ListActivas();
        Task<ICollection<SubastaDTO>> ListFinalizadas();
        Task<ICollection<SubastaDTO>> ListBorradores();
        Task<SubastaDTO?> FindByIdAsync(int id);

        Task<int> AddAsync(SubastaDTO dto, int idUsuario, int idEstado);

        Task UpdateAsync(int id, SubastaDTO dto);

        Task ToggleEstadoAsync(int id);

        Task<List<int>> CerrarSubastasVencidasAsync();
        Task CerrarSubastaAsync(int idSubasta);

        Task<ResultadoSubasta?> ObtenerResultadoAsync(int idSubasta);
    }
}
