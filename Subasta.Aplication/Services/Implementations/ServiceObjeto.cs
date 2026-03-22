using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            dto.ImagenesIds = objeto.ImagenObjeto
    .Select(i => i.IdImagen)
    .ToList();


            dto.HistorialSubastas = objeto.Subasta?
                .Select(s => _mapper.Map<SubastaDTO>(s))
                .ToList();
            dto.CategoriasIds = objeto.IdCategoria
    .Select(c => c.IdCategoria)
    .ToList();

            return dto;
        }

        public async Task<ICollection<ObjetoDTO>> ListAsync()
        {
          var list = await _repository.ListAsync();
          return _mapper.Map<ICollection<ObjetoDTO>>(list);

        }

        public async Task<int> AddAsync(ObjetoDTO dto, string[] selectedCategorias, int idUsuario)
        {
            try
            {
                var entity = _mapper.Map<Objeto>(dto);

                if (dto.Imagenes != null && dto.Imagenes.Any())
                {
                    entity.ImagenObjeto = dto.Imagenes
                        .Select(img => new ImagenObjeto
                        {
                            Imagen = img
                        })
                        .ToList();
                }
                entity.IdUsuarioVendedor = idUsuario;

                entity.IdEstado = 1;

                var categoriaIds = selectedCategorias?
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(x => x.HasValue)
                    .Select(x => x.Value)
                    .ToArray() ?? Array.Empty<int>();


                return await _repository.AddAsync(entity, categoriaIds);
            }
            catch (AutoMapperMappingException ex)
            {
                throw;
            }
        }
        public async Task UpdateAsync(
    int id,
    ObjetoDTO dto,
    string[] selectedCategorias,
    string[] imagenesAEliminar,
    List<byte[]> nuevasImagenes)
        {
            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                throw new Exception("Objeto no encontrado");

            if (entity.Subasta != null &&
                entity.Subasta.Any(s => s.IdEstadoSubastaNavigation != null &&
                                        s.IdEstadoSubastaNavigation.Descripcion == "Activa"))
            {
                throw new Exception("No se puede editar un objeto en subasta activa");
            }

            var idUsuario = entity.IdUsuarioVendedor;
            var idEstado = entity.IdEstado;
            var fecha = entity.FechaRegistro;


            entity.IdUsuarioVendedorNavigation = null;
            entity.IdEstadoNavigation = null;
            entity.IdCondicionNavigation = null;


            _mapper.Map(dto, entity);


            entity.IdUsuarioVendedor = idUsuario;
            entity.IdEstado = idEstado;
            entity.FechaRegistro = fecha;

            var idsEliminar = imagenesAEliminar?
                .Select(x => int.Parse(x))
                .ToList() ?? new List<int>();


            if (nuevasImagenes != null && nuevasImagenes.Count > 0)
            {
                foreach (var img in nuevasImagenes)
                {
                    entity.ImagenObjeto.Add(new ImagenObjeto
                    {
                        Imagen = img
                    });
                }
            }


            var categoriaIds = selectedCategorias?
                .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();


            await _repository.UpdateAsync(entity, categoriaIds, idsEliminar);
        }

        public async Task ToggleEstadoAsync(int id)
        {
            var objeto = await _repository.FindByIdAsync(id);

            if (objeto == null)
                throw new Exception("Objeto no encontrado");

   
            objeto.IdEstado = objeto.IdEstado == 1 ? 2 : 1;

            await _repository.UpdateEstadoAsync(objeto);
        }
    }
}

