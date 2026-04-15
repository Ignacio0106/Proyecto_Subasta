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
    public class ServicePago : IServicePago
    {
        private readonly IRepositoryPago _repository;
        private readonly IMapper _mapper;
        private readonly IRepositoryResultadoSubasta _repositoryResultado;

        public ServicePago(IRepositoryPago repository, IMapper mapper, IRepositoryResultadoSubasta repositoryResultado)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryResultado = repositoryResultado;
        }

        public async Task<PagoDTO?> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PagoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<PagoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<PagoDTO>>(list);
        }
        public async Task<ICollection<PagoDTO>> ListPagosByUserAsync(int id)
        {
            var pagos = await _repository.ListAsync();

            var pagosPendientes = new List<Pago>();

            foreach (var pago in pagos)
            {
                // Buscar resultado de la subasta
                var resultado = await _repositoryResultado.FindBySubastaIdAsync(pago.IdSubasta);

                if (resultado != null &&
                    resultado.IdUsuarioGanador == id)
                {
                    pagosPendientes.Add(pago);
                }
            }

            return _mapper.Map<ICollection<PagoDTO>>(pagosPendientes);
        }

        public async Task RegistrarPagoAsync(PagoDTO dto, int idUsuario)
        {
            var entity = await _repository.FindByIdAsync(dto.IdPago);

            if (entity == null)
                throw new Exception("Pago no encontrado");

            entity.IdEstadoPago = 2; // Pagado

            await _repository.UpdateAsync(entity);
        }

        public async Task<PagoDTO?> FindBySubastaAsync(int id)
        {
            var pago = await _repository.FindBySubastaAsync(id);

            if (pago == null)
                return null;

            var dto = _mapper.Map<PagoDTO>(pago);

            var nombreGanador = _repositoryResultado.FindBySubastaIdAsync(id).Result?.IdUsuarioGanadorNavigation.NombreCompleto;

            var idGanador = _repositoryResultado.FindBySubastaIdAsync(id).Result?.IdUsuarioGanadorNavigation.IdUsuario;

            dto.NombreUsuarioGanador = string.IsNullOrWhiteSpace(nombreGanador)
                ? "Sin ganador"
                : nombreGanador;
            dto.IdUsuarioGanador = (int)idGanador;
            return dto;
        }
    }
}
