using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class ResultadoSubastaProfile : Profile
    {
        public ResultadoSubastaProfile()
        {
            // ENTIDAD → DTO
            CreateMap<ResultadoSubasta, ResultadoSubastaDTO>()
                .ForMember(d => d.Subasta,
                    o => o.MapFrom(s => s.IdSubastaNavigation.IdSubasta))
                .ForMember(d => d.UsuarioGanador,
                    o => o.MapFrom(s => s.IdUsuarioGanadorNavigation.NombreCompleto));

            // DTO → ENTIDAD
            CreateMap<ResultadoSubastaDTO, ResultadoSubasta>()
                .ForMember(d => d.IdSubastaNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuarioGanadorNavigation, o => o.Ignore())
                .ForMember(d => d.IdSubasta, o => o.Ignore())
                .ForMember(d => d.IdUsuarioGanador, o => o.Ignore());
        }
    }
}