using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Implementations;
using Subasta.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServiceSubasta : IServiceSubasta
    {
        private readonly IRepositorySubasta _repository;
        private readonly IRepositoryPuja _repositoryPuja;
        private readonly IRepositoryResultadoSubasta _repositoryResultado;
        private readonly IRepositoryPago _repositoryPago;
        private readonly IMapper _mapper;

        public ServiceSubasta(
            IRepositorySubasta repository,
            IRepositoryPuja repositoryPuja,
            IRepositoryResultadoSubasta repositoryResultado,
            IRepositoryPago repositoryPago,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryPuja = repositoryPuja;
            _repositoryResultado = repositoryResultado;
            _repositoryPago = repositoryPago;
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

            var allSinBorradores = all.Where(s => s.IdEstadoSubasta != 4);

            return _mapper.Map<ICollection<SubastaDTO>>(allSinBorradores);
        }

        public async Task<ICollection<SubastaDTO>> ListActivas()
        {
            var all = await _repository.ListAsync();

            var finalizadas = all
                .Where(s => s.IdEstadoSubastaNavigation != null &&
                            s.IdEstadoSubastaNavigation.Descripcion == "Activa")
                .ToList();


            return _mapper.Map<ICollection<SubastaDTO>>(finalizadas);
        }



        public async Task<ICollection<SubastaDTO>> ListFinalizadas()
        {
            var all = await _repository.ListAsync();

            var finalizadas = all
                .Where(s => s.IdEstadoSubastaNavigation != null &&
                            s.IdEstadoSubastaNavigation.Descripcion == "Finalizada")
                .ToList();


            return _mapper.Map<ICollection<SubastaDTO>>(finalizadas);
        }

        public async Task<ICollection<SubastaDTO>> ListBorradores()
        {
            var all = await _repository.ListAsync();

            var borradores = all
                .Where(s => s.IdEstadoSubastaNavigation != null &&
                            s.IdEstadoSubastaNavigation.Descripcion == "Borrador")
                .ToList();


            return _mapper.Map<ICollection<SubastaDTO>>(borradores);
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


            entity.IdUsuarioCreador = idUsuario;
            entity.IdEstadoSubasta = idEstado;
            entity.IdObjeto = idObjeto;




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

        

        public async Task<List<int>> CerrarSubastasVencidasAsync()
        {
            var todasActivas = await _repository.ListAsync();
            var vencidas = todasActivas
                .Where(s => s.IdEstadoSubastaNavigation?.Descripcion == "Activa"
                         && s.FechaHoraCierre <= DateTime.Now)
                .ToList();

            var idsCerradas = new List<int>();

            foreach (var subasta in vencidas)
            {
           
                subasta.IdEstadoSubasta = 2; 
                await _repository.UpdateEstadoAsync(subasta);

           
                var pujaGanadora = await _repositoryPuja.GetPujaMaximaEntidadAsync(subasta.IdSubasta);

               
                var resultado = new ResultadoSubasta
                {
                    IdSubasta = subasta.IdSubasta,
                    IdUsuarioGanador = pujaGanadora?.IdUsuario ?? 0, 
                    MontoFinal = pujaGanadora?.MontoOfertado ?? 0,
                    FechaCierre = DateTime.Now
                };
                await _repositoryResultado.AddAsync(resultado);

     
                if (pujaGanadora != null)
                {
                    var pago = new Pago
                    {
                        IdSubasta = subasta.IdSubasta,
                        Monto = pujaGanadora.MontoOfertado,
                        FechaPago = DateTime.Now,   
                        IdEstadoPago = 1           
                    };
                    await _repositoryPago.AddAsync(pago);
                }

                idsCerradas.Add(subasta.IdSubasta);
            }

            return idsCerradas;
        }

        public async Task CerrarSubastaAsync(int idSubasta)
        {
            var subasta = await _repository.FindByIdAsync(idSubasta);

            if (subasta == null)
                return;

           
            if (subasta.IdEstadoSubasta == 2)
                return;

         
            subasta.IdEstadoSubasta = 2; 


            await _repository.UpdateAsync(subasta);

            await DeterminarGanadorAsync(idSubasta);
        }

        public async Task DeterminarGanadorAsync(int idSubasta)
        {
            var subasta = await _repository.FindByIdAsync(idSubasta);

            if (subasta == null)
                return;

            var mejorPuja = subasta.Puja
                .OrderByDescending(p => p.MontoOfertado)
                .FirstOrDefault();

            if (mejorPuja == null)
            {
              
                await _repositoryResultado.AddAsync(new ResultadoSubasta
                {
                    IdSubasta = idSubasta,
                    IdUsuarioGanador = 0,
                    MontoFinal = 0
                });

                return;
            }

            
            var resultado = new ResultadoSubasta
            {
                IdSubasta = idSubasta,
                IdUsuarioGanador = mejorPuja.IdUsuario,
                MontoFinal = mejorPuja.MontoOfertado
            };

            await _repositoryResultado.AddAsync(resultado);
        }
        public async Task<ResultadoSubasta?> ObtenerResultadoAsync(int idSubasta)
        {
            return await _repositoryResultado.FindBySubastaIdAsync(idSubasta);
        }



    }
}
