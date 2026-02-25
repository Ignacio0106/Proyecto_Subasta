using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class PujaProfile : Profile
    {
        public PujaProfile()
        {
            CreateMap<Puja, PujaDTO>()
     .ForMember(d => d.NombreUsuario,
         o => o.MapFrom(s => s.IdUsuarioNavigation.NombreCompleto));

            CreateMap<PujaDTO, Puja>()
                .ForMember(d => d.IdUsuarioNavigation, o => o.Ignore())
                .ForMember(d => d.IdSubastaNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuario, o => o.Ignore());

        }
    }
}