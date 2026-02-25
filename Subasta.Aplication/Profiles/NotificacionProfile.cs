using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class NotificacionProfile : Profile
    {
        public NotificacionProfile()
        {
            // ENTIDAD → DTO
            CreateMap<Notificacion, NotificacionDTO>()
                .ForMember(d => d.NombreUsuario,
                           o => o.MapFrom(s => s.IdUsuarioNavigation.NombreCompleto));

            // DTO → ENTIDAD (si algún día lo usas para crear)
            CreateMap<NotificacionDTO, Notificacion>()
                .ForMember(d => d.IdUsuarioNavigation, o => o.Ignore())
                .ForMember(d => d.IdUsuario, o => o.Ignore());
        }
    }
}