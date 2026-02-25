using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class ImagenObjetoProfile : Profile
    {
        public ImagenObjetoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<ImagenObjeto, ImagenObjetoDTO>();

            // DTO → ENTIDAD
            CreateMap<ImagenObjetoDTO, ImagenObjeto>()
                .ForMember(d => d.IdObjetoNavigation, o => o.Ignore());
        }
    }
}