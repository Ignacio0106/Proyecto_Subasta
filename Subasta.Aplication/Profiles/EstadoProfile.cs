using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile()
        {
            // ENTIDAD → DTO (mostrar)
            CreateMap<Estado, EstadoDTO>();

            // DTO → ENTIDAD (crear / editar)
            CreateMap<EstadoDTO, Estado>()
                .ForMember(d => d.Objeto, o => o.Ignore())
                .ForMember(d => d.Usuario, o => o.Ignore());
        }
    }
}