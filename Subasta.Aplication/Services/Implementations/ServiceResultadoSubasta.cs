using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Interfaces;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServiceResultadoSubasta: IServiceResultadoSubasta
    {
        private readonly IRepositoryResultadoSubasta _repository;
        private readonly IMapper _mapper;

        public ServiceResultadoSubasta(IRepositoryResultadoSubasta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ResultadoSubastaDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ResultadoSubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<ResultadoSubastaDTO>>(list);
        }
        

        public async Task<ResultadoSubastaDTO?> ObtenerResultadoAsync(int idSubasta)
        {
            var entity = await _repository.FindBySubastaIdAsync(idSubasta);

            if (entity == null)
                return null;

            return new ResultadoSubastaDTO
            {
                IdResultado = entity.IdResultado,
                MontoFinal = entity.MontoFinal,
                FechaCierre = entity.FechaCierre,
                IdUsuarioGanador = entity.IdUsuarioGanador,
                NombreUsuario = entity.IdUsuarioGanadorNavigation?.NombreCompleto ?? ""
            };
        }

        
    }
}
