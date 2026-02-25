using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class CondicionProfile : Profile
    {
        public CondicionProfile()
        {
            // ENTIDAD → DTO (mostrar)
            CreateMap<Condicion, CondicionDTO>();

            // DTO → ENTIDAD (crear / editar)
            CreateMap<CondicionDTO, Condicion>()
                .ForMember(d => d.Objeto, o => o.Ignore());
        }
    }
}