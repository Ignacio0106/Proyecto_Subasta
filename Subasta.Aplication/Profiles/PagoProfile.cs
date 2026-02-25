using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class PagoProfile : Profile
    {
        public PagoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<Pago, PagoDTO>()
                .ForMember(d => d.EstadoPago,
                    o => o.MapFrom(s => s.IdEstadoPagoNavigation.Descripcion));

            // DTO → ENTIDAD
            CreateMap<PagoDTO, Pago>()
                .ForMember(d => d.IdEstadoPagoNavigation, o => o.Ignore())
                .ForMember(d => d.IdSubastaNavigation, o => o.Ignore());
        }
    }
}