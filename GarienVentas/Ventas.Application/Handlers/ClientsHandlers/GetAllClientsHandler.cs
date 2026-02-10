using MediatR;
using Ventas.Application.Mappers.ClientsMappers;
using Ventas.Application.Queries.ClientsQueries;
using Ventas.Application.Responses.ClientsResponses;
using Ventas.Core.Repositories;
using Ventas.Core.Repositories.Base;

namespace Ventas.Application.Handlers.ClientsHandlers
{
    public class GetAllClientsHandler(IClientRepository _repository) : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientsResponse>>
    {
        public async Task<IEnumerable<ClientsResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.GetAllClients();
            var response = ClientsMapper.Mapper.Map<IEnumerable<ClientsResponse>>(clients);
            return response;
        }
    }
}


