using ApiClients.Models.Commands.Usuario;
using ApiClients.Models.Common;
using ApiClients.Models.Responses.UsuarioResponses;

namespace ApiClients.Ventas
{
    public  interface IUsuariosApiClient
    {
        Task<ApiResult<UsuarioResponse>> GetLoginUser(UsuarioCommand command ,CancellationToken ct = default);
    }
}
