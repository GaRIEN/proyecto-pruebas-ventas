using MediatR;
using Ventas.Application.Mappers.ClientsMappers;
using Ventas.Application.Queries.ClientsQueries;
using Ventas.Application.Responses.ClientsResponses;
using Ventas.Core.Repositories;

namespace Ventas.Application.Handlers.ClientsHandlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientsResponse>>
    {
        private readonly IClientRepository _repository;
        public GetAllClientsHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientsResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.GetAllAsync();
            var response = ClientsMapper.Mapper.Map<IEnumerable<ClientsResponse>>(clients);
            return response;
        }
    }
}
