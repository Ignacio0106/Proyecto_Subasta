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
    public class ServiceSubasta: IServiceSubasta
    {
        private readonly IRepositorySubasta _repository;
        private readonly IMapper _mapper;

        public ServiceSubasta(IRepositorySubasta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SubastaDTO?> FindByIdAsync(int id)
        {

            var subasta = await _repository.FindByIdAsync(id);

            if (subasta == null)
                return null;


            var dto = _mapper.Map<SubastaDTO>(subasta);


            dto.CantidadPujas = subasta.Puja.Count();
            dto.Categorias = subasta.IdObjetoNavigation?.IdCategoria?.Select(c => c.Nombre).ToList()
                             ?? new List<string>();

            return dto;
        }

        public async Task<ICollection<SubastaDTO>> ListActivas()
        {
            var all = await _repository.ListAsync();

            var activas = all
                .Where(s => s.IdEstadoSubastaNavigation != null &&
                            s.IdEstadoSubastaNavigation.Descripcion == "Activa")
                .ToList();

            return _mapper.Map<ICollection<SubastaDTO>>(activas);
        }

        public async Task<ICollection<SubastaDTO>> ListFinalizadas()
        {
            var all = await _repository.ListAsync();

            var activas = all
                .Where(s => s.IdEstadoSubastaNavigation != null &&
                            s.IdEstadoSubastaNavigation.Descripcion == "Finalizada")
                .ToList();


            return _mapper.Map<ICollection<SubastaDTO>>(activas);
        }


    }
}
