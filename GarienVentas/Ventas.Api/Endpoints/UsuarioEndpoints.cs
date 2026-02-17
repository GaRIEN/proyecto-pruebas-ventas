using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Queries.UsuarioQueries;

namespace Ventas.Api.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
        {
            // IMPORTANTE: Usamos MapPost y recibimos el objeto LoginUserQuery directamente
            app.MapPost("/api/auth/login", async (IMediator mediator, [FromBody] LoginUserQuery query) =>
            {
                // Enviamos el objeto 'query' que ya contiene Usuario y Password
                var token = await mediator.Send(query);

                if (string.IsNullOrEmpty(token))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(new { Token = token });
            })
            .WithName("LoginUser")
            .WithTags("Auth");
        }
    }
}
