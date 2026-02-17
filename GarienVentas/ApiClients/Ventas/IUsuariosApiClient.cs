

using ApiClients.Models.Common;
using ApiClients.Models.Responses.ClientsResponses;

namespace ApiClients.Ventas
{
    public  interface IUsuariosApiClient
    {
        Task<ApiResult<string>> GetAllUsuariosAsync(CancellationToken ct = default);
    }
}
