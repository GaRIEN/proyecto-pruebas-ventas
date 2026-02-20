using ApiClients.Base;
using ApiClients.Models.Commands.Usuario;
using ApiClients.Models.Common;
using ApiClients.Models.Responses.UsuarioResponses;

namespace ApiClients.Ventas
{
    public class UsuariosApiClient(HttpClient http) : BaseApiClient(http), IUsuariosApiClient
    {

        public Task<ApiResult<UsuarioResponse>> GetLoginUser(UsuarioCommand command, CancellationToken ct = default)
         => TryPostAsync<UsuarioCommand, UsuarioResponse>($"/api/auth/login", command, ct);
    }
}
