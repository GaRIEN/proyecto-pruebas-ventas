
using AutoMapper;
using Ventas.Application.Commands.UsuarioCommands;
using Ventas.Application.Responses.UsuarioResponses;
using Ventas.Core.Entities.Models;

namespace Ventas.Application.Mappers.usuarioMappers
{
    public  class UsuarioMappingProfile :Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioCommand>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();
        }
    }
}
