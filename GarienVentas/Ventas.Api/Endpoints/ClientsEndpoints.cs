using MediatR;
using System.ComponentModel.DataAnnotations;
using Ventas.Application.Queries.ClientsQueries;
using Microsoft.AspNetCore.Mvc;

namespace Ventas.Api.Endpoints
{
    public static class ClientsEndpoints
    {
        public static void MapClientsEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/clients/getAll", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllClientsQuery());
                return Results.Ok(response);
            })
            .WithName("GetAllClients")
            .WithTags("Clients");

        }
    }
}
