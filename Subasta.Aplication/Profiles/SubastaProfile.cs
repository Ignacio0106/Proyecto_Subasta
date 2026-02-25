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
            CreateMap<Subastaa, SubastaDTO>()
    .ForMember(dest => dest.UsuarioCreador,
        opt => opt.MapFrom(src => src.IdUsuarioCreadorNavigation.NombreCompleto))
    .ForMember(dest => dest.Objeto,
        opt => opt.MapFrom(src => src.IdObjetoNavigation.Nombre))
    .ForMember(dest => dest.EstadoSubasta,
        opt => opt.MapFrom(src => src.IdEstadoSubastaNavigation.Descripcion))
    .ForMember(dest => dest.CantidadPujas,
        opt => opt.MapFrom(src => src.Puja.Count()))
    .ForMember(dest => dest.Imagen,
        opt => opt.MapFrom(src => src.IdObjetoNavigation.ImagenObjeto
            .OrderBy(i => i.IdImagen)
            .Select(i => i.Imagen)
            .FirstOrDefault()))
    .ForMember(dest => dest.Condicion,
        opt => opt.MapFrom(src => src.IdObjetoNavigation.IdCondicionNavigation.Descripcion));

            // DTO → ENTIDAD (crear / editar)
            CreateMap<SubastaDTO, Subastaa>()
                .ForMember(d => d.IdEstadoSubastaNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuarioCreadorNavigation, o => o.Ignore())
                .ForMember(d => d.IdObjetoNavigation, o => o.Ignore())
                .ForMember(d => d.Pago, o => o.Ignore())
                .ForMember(d => d.Puja, o => o.Ignore())
                .ForMember(d => d.ResultadoSubasta, o => o.Ignore());
        }
    }
}