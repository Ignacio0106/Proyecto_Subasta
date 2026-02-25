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
    public class ServiceImagenObjeto: IServiceImagenObjeto
    {
        private readonly IRepositoryImagenObjeto _repository;
        private readonly IMapper _mapper;

        public ServiceImagenObjeto(IRepositoryImagenObjeto repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ImagenObjetoDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ImagenObjetoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<ImagenObjetoDTO>>(list);
        }
    }
}
