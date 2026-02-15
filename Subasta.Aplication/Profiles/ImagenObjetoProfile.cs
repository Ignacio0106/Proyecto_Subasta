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
    public  class ImagenObjetoProfile: Profile
    {
        public ImagenObjetoProfile()
        {
            CreateMap<ImagenObjeto, ImagenObjetoDTO >();
            /*CreateMap<Libro, LibroDTO>(); 
             CreateMap<Autor, AutorDTO>() 
                 .ForMember(d => d.Libros, opt => opt.MapFrom(s => s.Libro));*/
        }
    }
}
