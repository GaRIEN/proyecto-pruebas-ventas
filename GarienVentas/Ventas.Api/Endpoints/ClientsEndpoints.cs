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
            app.MapGet("/api/clients/getAll", async ([FromServices] IMediator mediator) =>
            {
                try
                {
                    // 1. Crear el objeto Query con los parámetros capturados
                    var query = new GetAllClientsQuery();

                    // 2. Enviar la consulta y obtener la respuesta completa (incluyendo TotalRows)
                    var response = await mediator.Send(query);

                    // 3. Devolver la respuesta exitosa (200 OK)
                    return Results.Ok(response);
                }
                catch (ValidationException)
                {
                    return Results.BadRequest(new { error = "Parámetros de búsqueda inválidos." });
                }
                catch (Exception)
                {
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("GetAllClients")
            .WithTags("Clients");
        }
    }
}
