using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Implementations;
using Subasta.Infraestructure.Repository.Interfaces;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServicePuja: IServicePuja
    {
        private readonly IRepositoryPuja _repository;
        private readonly IRepositorySubasta _repositorySubasta;
        private readonly IMapper _mapper;

        public ServicePuja(IRepositoryPuja repository, IMapper mapper, IRepositorySubasta repositorySubasta)
        {
            _repository = repository;
            _mapper = mapper;
            _repositorySubasta = repositorySubasta;
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

        public async Task<(bool exito, string mensaje)> RegistrarPujaAsync(int idSubasta, decimal monto, int idUsuarioActual)
        {
            var subasta = await _repositorySubasta.FindByIdAsync(idSubasta);
            if (subasta == null)
                return (false, "Subasta no encontrada");

            var ahora = DateTime.Now; 

            bool activa = subasta.IdEstadoSubasta == 1
               && subasta.FechaHoraInicio <= ahora
               && subasta.FechaHoraCierre > ahora;

        

            if (!activa)
                return (false, "La subasta no está activa");

            if (subasta.IdUsuarioCreador == idUsuarioActual)
                return (false, "No puedes pujar en tu propia subasta");

            var pujaMaxima = await _repository.GetPujaMaximaEntidadAsync(idSubasta);
            decimal montoBase = pujaMaxima?.MontoOfertado ?? subasta.PrecioBase;
            decimal incrementoMinimo = subasta.IncrementoMinimo;

            if (monto <= montoBase)
                return (false, $"El monto debe ser mayor a {montoBase:C}");

            if (monto < montoBase + incrementoMinimo)
                return (false, $"El incremento mínimo es {incrementoMinimo:C}");

            var nuevaPuja = new Puja
            {
                MontoOfertado = monto,
                FechaHora = DateTime.Now,
                IdUsuario = idUsuarioActual,
                IdSubasta = idSubasta,
                IdUsuarioNavigation = null,
                IdSubastaNavigation = null
            };
            

            try
            {
                await _repository.AddAsync(nuevaPuja);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ ERROR REAL BD:");
                System.Diagnostics.Debug.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }

            return (true, "Puja registrada exitosamente");
        }


    }
}
