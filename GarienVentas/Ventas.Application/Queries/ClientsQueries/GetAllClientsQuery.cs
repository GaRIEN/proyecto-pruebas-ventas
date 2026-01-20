
using MediatR;
using Ventas.Application.Responses.ClientsResponses;

namespace Ventas.Application.Queries.ClientsQueries
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientsResponse>>
    {
        public GetAllClientsQuery()
        {
            
        }
    }

}
