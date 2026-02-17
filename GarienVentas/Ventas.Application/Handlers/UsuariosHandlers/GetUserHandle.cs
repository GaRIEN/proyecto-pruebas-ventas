
using MediatR;
using Ventas.Application.Mappers.usuarioMappers;
using Ventas.Application.Queries.UsuarioQueries;
using Ventas.Core.Repositories;

namespace Ventas.Application.Handlers.UsuariosHandlers
{
    public class GetUserHandle (ILoginUserRepository _repository) : IRequestHandler<LoginUserQuery, string>
    {
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            // El repositorio ya hace la validación y devuelve el string o null
            var token = await _repository.LoginUser(request.Usuario, request.Password);

            // No necesitamos Mapper aquí porque el resultado ya es el string que esperamos
            return token;
        }
    }
}
