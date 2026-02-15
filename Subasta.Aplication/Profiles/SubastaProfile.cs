using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Infraestructure.Models;

namespace Subasta.Aplication.Profiles
{
    public class SubastaProfile : Profile
    {
        public SubastaProfile()
        {
           // CreateMap<Subasta, SubastaDTO>(); no se xq no lee subasta 
            /*CreateMap<Libro, LibroDTO>(); 
             CreateMap<Autor, AutorDTO>() 
                 .ForMember(d => d.Libros, opt => opt.MapFrom(s => s.Libro));*/
        }
    }
}
