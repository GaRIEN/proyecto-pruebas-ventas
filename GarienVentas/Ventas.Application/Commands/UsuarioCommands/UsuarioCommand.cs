
using MediatR;
using Ventas.Application.Responses.UsuarioResponses;

namespace Ventas.Application.Commands.UsuarioCommands
{
    public record UsuarioCommand :IRequest<UsuarioResponse>
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}
