using ApiClients.Base;
using ApiClients.Models.Common;
using ApiClients.Models.Responses.ClientsResponses;

namespace ApiClients.Ventas
{
    public class UsuariosApiClient(HttpClient http) : BaseApiClient(http), IUsuariosApiClient
    {


        public Task<ApiResult<string>> GetAllUsuariosAsync(CancellationToken ct = default)
       => TryGetAsync<string>($"/api/clients/getAll", ct);

    }
}
