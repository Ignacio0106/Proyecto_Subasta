using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {

            CreateMap<Rol, RolDTO>();

            // DTO → ENTIDAD (crear / editar)
            CreateMap<RolDTO, Rol>()
                .ForMember(d => d.Usuario, o => o.Ignore());
        }
    }
}