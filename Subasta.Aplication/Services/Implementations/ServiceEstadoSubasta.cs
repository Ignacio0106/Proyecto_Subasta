using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Repository.Interfaces;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServiceEstadoSubasta: IServiceEstadoSubasta
    {
        private readonly IRepositoryEstadoSubasta _repository;
        private readonly IMapper _mapper;

        public ServiceEstadoSubasta(IRepositoryEstadoSubasta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<EstadoSubastaDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<EstadoSubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<EstadoSubastaDTO>>(list);
        }
    }
}
