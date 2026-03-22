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
    public class ServiceCondicion: IServiceCondicion
    {
        private readonly IRepositoryCondicion _repository;
        private readonly IMapper _mapper;

        public ServiceCondicion(IRepositoryCondicion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CondicionDTO?> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<CondicionDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<CondicionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<CondicionDTO>>(list);
        }
    }
}
