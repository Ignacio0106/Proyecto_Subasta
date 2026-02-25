using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaDTO>()
            .ForMember(d => d.Objetos,
                       o => o.MapFrom(s => s.IdObjeto));

        CreateMap<CategoriaDTO, Categoria>()
            .ForMember(d => d.IdObjeto,
                       o => o.Ignore());
    }
}