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
            //app.MapGet("/api/clients/getAll", async ([FromServices] IMediator mediator) =>
            //{
            //    try
            //    {
            //        var query = new GetAllClientsQuery();

            //        var response = await mediator.Send(query);

            //        return Results.Ok(response);
            //    }
            //    catch (ValidationException)
            //    {
            //        return Results.BadRequest(new { error = "Parámetros de búsqueda inválidos." });
            //    }
            //    catch (Exception)
            //    {
            //        return Results.StatusCode(StatusCodes.Status500InternalServerError);
            //    }
            //})
            //.WithName("GetAllClients")
            //.WithTags("Clients");


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
