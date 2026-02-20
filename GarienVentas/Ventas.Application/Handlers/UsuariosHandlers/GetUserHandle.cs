
using MediatR;
using Ventas.Application.Commands.UsuarioCommands;
using Ventas.Application.Mappers.usuarioMappers;
using Ventas.Application.Responses.UsuarioResponses;
using Ventas.Core.Entities.Models;
using Ventas.Core.Repositories;

namespace Ventas.Application.Handlers.UsuariosHandlers
{
    public class GetUserHandle (ILoginUserRepository _repository) : IRequestHandler<UsuarioCommand, UsuarioResponse>
    {
        public async Task<UsuarioResponse> Handle(UsuarioCommand request, CancellationToken cancellationToken)
        {

            var entity = UsuarioMapper.Mapper.Map<Usuario>(request);
            if (entity is null)
            {
                throw new ApplicationException("not mapped");
            }
            var command = await _repository.LoginUser(entity);
            var response = UsuarioMapper.Mapper.Map<UsuarioResponse>(command);
            return response;
        }
    }
}
