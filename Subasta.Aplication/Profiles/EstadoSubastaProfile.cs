using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class EstadoSubastaProfile : Profile
    {
        public EstadoSubastaProfile()
        {
            // ENTIDAD → DTO (mostrar)
            CreateMap<EstadoSubasta, EstadoSubastaDTO>();

            // DTO → ENTIDAD (crear / editar)
            CreateMap<EstadoSubastaDTO, EstadoSubasta>()
                .ForMember(d => d.Subasta, o => o.Ignore());
        }
    }
}