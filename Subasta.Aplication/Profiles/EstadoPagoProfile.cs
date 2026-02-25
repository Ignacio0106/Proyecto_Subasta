using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class EstadoPagoProfile : Profile
    {
        public EstadoPagoProfile()
        {
            // ENTIDAD → DTO (mostrar)
            CreateMap<EstadoPago, EstadoPagoDTO>();

            // DTO → ENTIDAD (crear / editar)
            CreateMap<EstadoPagoDTO, EstadoPago>()
                .ForMember(d => d.Pago, o => o.Ignore());
        }
    }
}