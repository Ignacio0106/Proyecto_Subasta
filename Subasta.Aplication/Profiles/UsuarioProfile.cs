using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // ENTIDAD → DTO (mostrar)
            CreateMap<Usuario, UsuarioDTO>()
    .ForMember(d => d.Rol,
        o => o.MapFrom(s => s.IdRolNavigation.NombreRol))
    .ForMember(d => d.Estado,
        o => o.MapFrom(s => s.IdEstadoNavigation.Descripcion));



            // DTO → ENTIDAD (crear / editar)
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(d => d.IdRolNavigation, o => o.Ignore())
                .ForMember(d => d.IdEstadoNavigation, o => o.Ignore())
                .ForMember(d => d.Notificacion, o => o.Ignore())
                .ForMember(d => d.Objeto, o => o.Ignore())
                .ForMember(d => d.Puja, o => o.Ignore())
                .ForMember(d => d.ResultadoSubasta, o => o.Ignore())
                .ForMember(d => d.Subasta, o => o.Ignore());

        }
    }
}