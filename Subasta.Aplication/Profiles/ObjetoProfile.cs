using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class ObjetoProfile : Profile
    {
        public ObjetoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<Objeto, ObjetoDTO>()
    .ForMember(d => d.Vendedor, o => o.MapFrom(s => s.IdUsuarioVendedorNavigation.NombreCompleto))
    .ForMember(d => d.Estado, o => o.MapFrom(s => s.IdEstadoNavigation.Descripcion))
    .ForMember(d => d.Condicion, o => o.MapFrom(s => s.IdCondicionNavigation.Descripcion))
    .ForMember(d => d.ImagenPrincipal, o => o.MapFrom(s => s.ImagenObjeto
        .OrderBy(i => i.IdImagen)
        .Select(i => i.Imagen)
        .FirstOrDefault()))
    .ForMember(d => d.Categorias, o => o.MapFrom(s => s.IdCategoria.Select(c => c.Nombre).ToList()))
    .ForMember(d => d.HistorialSubastas, o => o.MapFrom(s => s.Subasta));

            // DTO → ENTIDAD (si algún día lo usas para crear)
            CreateMap<ObjetoDTO, Objeto>()
                .ForMember(d => d.IdUsuarioVendedorNavigation, o => o.Ignore())
                .ForMember(d => d.IdEstadoNavigation, o => o.Ignore())
                .ForMember(d => d.IdCondicionNavigation, o => o.Ignore())
                .ForMember(d => d.ImagenObjeto, o => o.Ignore())
                .ForMember(d => d.Subasta, o => o.Ignore())
                .ForMember(d => d.IdCategoria, o => o.Ignore());
        }
    }
}