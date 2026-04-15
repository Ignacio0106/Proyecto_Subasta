using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class PagoProfile : Profile
    {
        public PagoProfile()
        {
            CreateMap<EstadoPago, EstadoPagoDTO>();
            // ENTIDAD → DTO
            CreateMap<Pago, PagoDTO>()
            .ForMember(d => d.IdEstadoPagoNavigation, o => o.MapFrom(s => s.IdEstadoPagoNavigation));

            // DTO → ENTIDAD
            CreateMap<PagoDTO, Pago>()
            .ForMember(d => d.IdEstadoPagoNavigation, o => o.Ignore())
            .ForMember(d => d.IdPago, o => o.Ignore());
        }
    }
}