using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Repository.Interfaces;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServiceObjeto: IServiceObjeto
    {
        private readonly IRepositoryObjeto _repository;
        private readonly IMapper _mapper;

        public ServiceObjeto(IRepositoryObjeto repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ObjetoDTO?> FindByIdAsync(int id)
        {
            var objeto = await _repository.FindByIdAsync(id);
            if (objeto == null) return null;

            var dto = _mapper.Map<ObjetoDTO>(objeto);

            dto.Imagenes = objeto.ImagenObjeto
                                .OrderBy(i => i.IdImagen)
                                .Select(i => i.Imagen)
                                .ToList();


            dto.HistorialSubastas = objeto.Subasta?
                .Select(s => _mapper.Map<SubastaDTO>(s))
                .ToList();

            return dto;
        }

        public async Task<ICollection<ObjetoDTO>> ListAsync()
        {
          var list = await _repository.ListAsync();
          return _mapper.Map<ICollection<ObjetoDTO>>(list);
        }
    }
}

