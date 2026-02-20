using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Commands.UsuarioCommands;

namespace Ventas.Api.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
        {
            // IMPORTANTE: Usamos MapPost y recibimos el objeto LoginUserQuery directamente
            app.MapPost("/api/auth/login", async (IMediator mediator, [FromBody] UsuarioCommand command) =>
            {

                try
                {
                    var response = await mediator.Send(command);
                    return Results.Ok(response);
                }
                catch (Exception)
                {
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }

            })
            .WithName("LoginUser")
            .WithTags("Auth");
        }
    }
}
