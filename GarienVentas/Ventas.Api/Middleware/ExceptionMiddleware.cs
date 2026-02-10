using System.Net;
using System.Text.Json;

namespace Ventas.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        // 1. Inyectamos IHostEnvironment en el InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext, IHostEnvironment env)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error no controlado.");
                // 2. Se lo pasamos al método que maneja la respuesta
                await HandleExceptionAsync(httpContext, ex, env);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";

            // Determinar el código de estado basado en el tipo de excepción
            var statusCode = exception is KeyNotFoundException ? HttpStatusCode.NotFound : HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                // Solo mostramos el mensaje detallado si NO es un error de servidor o si estamos en Dev
                Message = (statusCode == HttpStatusCode.InternalServerError && !env.IsDevelopment())
                          ? "Ocurrió un error inesperado en el servidor."
                          : exception.Message,
                Detail = env.IsDevelopment() ? exception.StackTrace : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
