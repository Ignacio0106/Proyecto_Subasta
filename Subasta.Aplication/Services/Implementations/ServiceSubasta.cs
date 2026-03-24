using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Solo lo que NO está en el Profile
            dto.Categorias = subasta.IdObjetoNavigation?.IdCategoria?
                                .Select(c => c.Nombre)
                                .ToList() ?? new List<string>();

            return dto;
        }
        public async Task<ICollection<SubastaDTO>> ListAsync()
        {
            var all = await _repository.ListAsync();

            return _mapper.Map<ICollection<SubastaDTO>>(all);
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

        public async Task<int> AddAsync(SubastaDTO dto, int idUsuario, int idEstado)
        {
            try
            {
                var entity = _mapper.Map<Subastaa>(dto);
                entity.IdUsuarioCreador = idUsuario;
                entity.IdEstadoSubasta = idEstado;

                return await _repository.AddAsync(entity);
            }
            catch (AutoMapperMappingException ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(int id, SubastaDTO dto)
        {
                var entity = await _repository.FindByIdAsync(id);

                if (entity == null)
                    throw new Exception("Objeto no encontrado");

                var idUsuario = entity.IdUsuarioCreador;
                var idEstado = entity.IdEstadoSubasta;
                var idObjeto = entity.IdObjeto;


                entity.IdUsuarioCreadorNavigation = null;
                entity.IdObjetoNavigation = null;
                entity.IdEstadoSubastaNavigation = null;


                _mapper.Map(dto, entity);


                entity.IdUsuarioCreador= idUsuario;
                entity.IdEstadoSubasta = idEstado;
                entity.IdObjeto= idObjeto;

      


                await _repository.UpdateAsync(entity);
        }

        public async Task ToggleEstadoAsync(int id)
        {
            var subasta = await _repository.FindByIdAsync(id);

            if (subasta == null)
                throw new Exception("Subasta no encontrado");


            subasta.IdEstadoSubasta = subasta.IdEstadoSubasta == 1 ? 3 : 1;

            await _repository.UpdateEstadoAsync(subasta);
        }
    }
}
