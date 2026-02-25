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
    public class ServicePuja: IServicePuja
    {
        private readonly IRepositoryPuja _repository;
        private readonly IMapper _mapper;

        public ServicePuja(IRepositoryPuja repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<PujaDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PujaDTO>> ListBySubastaAsync(int idSubasta)
        {
            var pujas = await _repository.ListBySubastaAsync(idSubasta);

            return _mapper.Map<ICollection<PujaDTO>>(pujas);
        }
    }
}
