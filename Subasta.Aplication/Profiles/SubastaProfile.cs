using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class SubastaProfile : Profile
    {
        public SubastaProfile()
        {
            // ENTIDAD → DTO (para mostrar)
            CreateMap<Objeto, ObjetoDTO>();
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<Subastaa, SubastaDTO>()
    .ForMember(dest => dest.UsuarioCreador,
        opt => opt.MapFrom(src => src.IdUsuarioCreadorNavigation.NombreCompleto))
    .ForMember(dest => dest.Objeto,
        opt => opt.MapFrom(src => src.IdObjetoNavigation.Nombre))
    .ForMember(dest => dest.EstadoSubasta,
        opt => opt.MapFrom(src => src.IdEstadoSubastaNavigation.Descripcion))
    .ForMember(dest => dest.CantidadPujas,
        opt => opt.MapFrom(src => src.Puja.Count()))
    .ForMember(d => d.ImagenPrincipal, o => o.MapFrom(s =>
        s.IdObjetoNavigation.ImagenObjeto
            .OrderBy(i => i.IdImagen)
            .Select(i => i.Imagen)
            .FirstOrDefault()))
    .ForMember(d => d.Imagenes, o => o.MapFrom(s =>
        s.IdObjetoNavigation.ImagenObjeto
            .OrderBy(i => i.IdImagen)
            .Select(i => i.Imagen)
            .ToList()))
    .ForMember(d => d.Condicion,
        opt => opt.MapFrom(src => src.IdObjetoNavigation.IdCondicionNavigation.Descripcion));
    //.ForMember(o => o.IdObjetoNavigation.IdUsuarioNavigation);

            // DTO → ENTIDAD (crear / editar)
            CreateMap<SubastaDTO, Subastaa>()
                .ForMember(d => d.IdEstadoSubastaNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuarioCreadorNavigation, o => o.Ignore())
                .ForMember(d => d.IdObjetoNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuarioCreadorNavigation, o => o.Ignore())
                .ForMember(d => d.Pago, o => o.Ignore())
                .ForMember(d => d.Puja, o => o.Ignore())
                .ForMember(d => d.ResultadoSubasta, o => o.Ignore());
        }
    }
}