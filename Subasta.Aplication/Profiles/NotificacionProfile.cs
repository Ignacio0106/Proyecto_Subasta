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
    public  class NotificacionProfile : Profile
    {
        public NotificacionProfile()
        {
            CreateMap<Notificacion, NotificacionDTO>();
            /*CreateMap<Libro, LibroDTO>(); 
             CreateMap<Autor, AutorDTO>() 
                 .ForMember(d => d.Libros, opt => opt.MapFrom(s => s.Libro));*/
        }
    }
}
