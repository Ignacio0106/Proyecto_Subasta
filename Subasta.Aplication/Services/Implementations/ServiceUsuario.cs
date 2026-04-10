using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Subasta.Aplication.DTOs;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Application.Config;
using Subasta.Application.Utils;
using Subasta.Infraestructure.Models;
using Subasta.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Aplication.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, IOptions<AppConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }
        public async Task<string> AddAsync(UsuarioDTO dto)
        {
            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(dto.Contrasenna!, secret);
            // Establecer password DTO
            dto.Contrasenna = passwordEncrypted;
            var objectMapped = _mapper.Map<Usuario>(dto);

            return await _repository.AddAsync(objectMapped);
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
                dto.CantidadSubastas = null;
                dto.CantidadPujas = null;
            }

            return dto;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<UsuarioDTO>>(list);
        }

        public async Task<UsuarioDTO> LoginAsync(string id, string password)
        {
            UsuarioDTO usuarioDTO = null!;

            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(password, secret);

            var @object = await _repository.LoginAsync(id, passwordEncrypted);

            if (@object != null)
            {
                usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
            }

            return usuarioDTO;
        }

        public async Task UpdateAsync(int id, UsuarioDTO dto)
        {
            var usuario = await _repository.FindByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.CorreoElectronico = dto.CorreoElectronico;

            await _repository.UpdateAsync(usuario);
        }
        public async Task ToggleEstadoAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.IdEstado = usuario.IdEstado == 1 ? 2 : 1;

            await _repository.UpdateEstadoAsync(usuario);
        }
    }
}
