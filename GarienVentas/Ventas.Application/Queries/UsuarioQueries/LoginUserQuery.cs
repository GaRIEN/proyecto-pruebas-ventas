
using MediatR;

namespace Ventas.Application.Queries.UsuarioQueries
{
    public record LoginUserQuery(string Usuario, string Password) : IRequest<string>
    {
    }
}
