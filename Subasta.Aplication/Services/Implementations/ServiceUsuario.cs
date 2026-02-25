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
    public class ServiceUsuario: IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO?> FindByIdAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);

            if (usuario == null)
                return null;

            var dto = _mapper.Map<UsuarioDTO>(usuario);

            // Lógica condicional según rol
            if (usuario.IdRolNavigation.NombreRol == "Vendedor") // o usuario.Rol == "Vendedor"
            {
                dto.CantidadSubastas = usuario.Subasta.Count(s => s.IdUsuarioCreador == usuario.IdUsuario);
                dto.CantidadPujas = null; // no mostrar
            }
            else if (usuario.IdRolNavigation.NombreRol == "Comprador") // o usuario.Rol == "Comprador"
            {
                dto.CantidadPujas = usuario.Puja.Count(p => p.IdUsuario == usuario.IdUsuario);
                dto.CantidadSubastas = null; // no mostrar
            }
            else
            {
                dto.CantidadSubastas = usuario.Subasta.Count(s => s.IdUsuarioCreador == usuario.IdUsuario); ;
                dto.CantidadPujas = usuario.Puja.Count(p => p.IdUsuario == usuario.IdUsuario); ;
            }

            return dto;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<UsuarioDTO>>(list);
        }
    }
}
