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
    }
}
