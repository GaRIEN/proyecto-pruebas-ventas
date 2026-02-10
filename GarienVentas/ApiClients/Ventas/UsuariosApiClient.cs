using ApiClients.Base;
using ApiClients.Models.Common;
using ApiClients.Models.Responses.ClientsResponses;

namespace ApiClients.Ventas
{
    public class UsuariosApiClient(HttpClient http) : BaseApiClient(http), IUsuariosApiClient
    {


        public Task<ApiResult<IEnumerable<ClientsResponse>>> GetAllUsuariosAsync(CancellationToken ct = default)
       => TryGetAsync<IEnumerable<ClientsResponse>>($"/api/clients/getAll", ct);

    }
}
