using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Services.Interfaces
{
    public interface IServiceObjeto
    {
        Task<ICollection<ObjetoDTO>> ListAsync();
        Task<ObjetoDTO?> FindByIdAsync(int id);
        Task<int> AddAsync(ObjetoDTO dto, string[] selectedCategorias, int idUsuario);
        Task UpdateAsync(int id, ObjetoDTO dto, string[] selectedCategorias, string[] imagenesAEliminar,
    List<byte[]> nuevasImagenes);

        Task ToggleEstadoAsync(int id);
    }

}
